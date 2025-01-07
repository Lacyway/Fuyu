using System;
using System.Collections.Generic;
using System.Linq;
using Fuyu.Backend.BSG.Models.Accounts;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT.Services
{
    public class AccountService
    {
		// TODO:
		// * account login state tracking
		// -- seionmoya, 2024/09/06

		public static AccountService Instance => instance.Value;
		private static readonly Lazy<AccountService> instance = new(() => new AccountService());

		/// <summary>
		/// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
		/// </summary>
		private AccountService()
		{

		}

		public string LoginAccount(int accountId)
        {
            if (accountId == -1)
            {
                // account doesn't exist
                return string.Empty;
            }

            // find active account session
            var sessions = EftOrm.Instance.GetSessions();

            foreach (var kvp in sessions)
            {
                if (kvp.Value == accountId)
                {
                    // session already exists
                    return kvp.Key;
                }
            }

            // create new account session
            // NOTE: MongoId's are used internally, but EFT's launcher uses
            //       a different ID system (hwid+timestamp hash). Instead of
            //       fully mimicking this, I decided to generate a new MongoId
            //       for each login.
            // -- seionmoya, 2024/09/02
            var sessionId = new MongoId(accountId).ToString();
            EftOrm.Instance.SetOrAddSession(sessionId, accountId);
            return sessionId.ToString();
        }

        private int GetNewAccountId()
        {
            var accounts = EftOrm.Instance.GetAccounts();

            // using linq because sorting otherwise takes up too much code
            var sorted = accounts.OrderBy(account => account.Id).ToArray();

            // find all gap entries
            var found = new List<int>();

            for (var i = 0; i < sorted.Length; ++i)
            {
                if (sorted[i].Id != i)
                {
                    found.Add(sorted[i].Id);
                }
            }

            if (found.Count > 0)
            {
                // use first gap entry
                return found[0];
            }
            else
            {
                // use new entry
                return sorted.Length;
            }
        }

        public int RegisterAccount(string username, string edition)
        {
            var accountId = GetNewAccountId();

            // create profiles
            var pvpId = ProfileService.Instance.CreateProfile(accountId);
            var pveId = ProfileService.Instance.CreateProfile(accountId);

            // create account   
            var account = new EftAccount()
            {
                Id = accountId,
                Edition = edition,
                Username = username,
                PvpId = pvpId,
                PveId = pveId,
                CurrentSession = ESessionMode.Pve
            };

            EftOrm.Instance.SetOrAddAccount(account);
            WriteToDisk(account);

            return accountId;
        }

        public void WriteToDisk(EftAccount account)
        {
            VFS.WriteTextFile(
                $"./Fuyu/Accounts/EFT/{account.Id}.json",
                Json.Stringify(account));
        }
    }
}