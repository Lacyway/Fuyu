using System;
using Fuyu.Common.Hashing;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;
using Fuyu.Backend.BSG.DTO.Profiles;
using Fuyu.Backend.BSG.DTO.Profiles.Info;
using Fuyu.Backend.EFT.DTO.Accounts;

namespace Fuyu.Backend.EFT.Services
{
    public static class ProfileService
    {
        public static string CreateProfile(int accountId)
        {
            var profile = new EftProfile()
            {
                Pmc = new Profile(),
                Savage = new Profile(),
                Suites = [],
                ShouldWipe = true
            };

            // generate new ids
            var pmcId = new MongoId(true).ToString();
            var savageId = new MongoId(pmcId, 1, false).ToString();

            // set profile info
            profile.Pmc._id    = pmcId;
            profile.Pmc.aid    = accountId;

            profile.Savage._id = savageId;
            profile.Savage.aid = accountId;

            // store profile
            EftOrm.SetOrAddProfile(profile);
            WriteToDisk(profile);

            return pmcId;
        }

        public static string WipeProfile(EftAccount account, string side, string headId, string voiceId)
        {
            var profile = EftOrm.GetActiveProfile(account);
            var pmcId = profile.Pmc._id;
            var savageId = profile.Savage._id;

            // create profiles
            var edition = EftOrm.GetWipeProfile(account.Edition);

            profile.Savage = edition[EPlayerSide.Savage].Profile;

            // NOTE: Case-sensitive
            // -- seionmoya, 2024-10-13
            switch (side)
            {
                case "Bear":
                    profile.Pmc = edition[EPlayerSide.Bear].Profile;
                    profile.Suites = edition[EPlayerSide.Bear].Suites;
                    break;

                case "Usec":
                    profile.Pmc = edition[EPlayerSide.Usec].Profile;
                    profile.Suites = edition[EPlayerSide.Usec].Suites;
                    break;
                
                default:
                    throw new Exception("Unsupported faction");
            }        

            // setup savage
            profile.Savage._id = savageId;
            profile.Savage.aid = account.Id;

            // setup pmc
            var voiceTemplate = EftOrm.GetCustomization(voiceId);

            profile.Pmc._id                 = pmcId;
            profile.Pmc.savage              = savageId;
            profile.Pmc.aid                 = account.Id;
            profile.Pmc.Info.Nickname       = account.Username;
            profile.Pmc.Info.LowerNickname  = account.Username.ToLowerInvariant();
            profile.Pmc.Info.Voice          = voiceTemplate._name;
            profile.Pmc.Customization.Head  = headId;

            // wipe done
            profile.ShouldWipe = false;

            // store profile
            EftOrm.SetOrAddProfile(profile);
            WriteToDisk(profile);

            return profile.Pmc._id;
        }

        public static void WriteToDisk(EftProfile profile)
        {
            VFS.WriteTextFile(
                $"./Fuyu/Profiles/EFT/{profile.Pmc._id}.json",
                Json.Stringify(profile));
        }
    }
}