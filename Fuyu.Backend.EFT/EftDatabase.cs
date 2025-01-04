using System.Collections.Generic;
using Fuyu.Backend.BSG.Models.Accounts;
using Fuyu.Backend.BSG.Models.Customization;
using Fuyu.Backend.BSG.Models.Profiles;
using Fuyu.Backend.BSG.Models.Profiles.Info;
using Fuyu.Backend.BSG.Models.Responses;
using Fuyu.Common.Collections;
using Fuyu.Common.IO;
using Fuyu.Common.Serialization;

namespace Fuyu.Backend.EFT
{
    // NOTE: The properties of this class should _NEVER_ be accessed from the
    //       outside. Use EftOrm instead.
    // -- seionmoya, 2024/09/04

    public static class EftDatabase
    {
        internal static readonly ThreadList<EftAccount> Accounts;

        internal static readonly ThreadList<EftProfile> Profiles;

        //                                        sessid  aid
        internal static readonly ThreadDictionary<string, int> Sessions;

        //                                        custid  template 
        internal static readonly ThreadDictionary<string, CustomizationTemplate> Customizations;

        //                                        langid             key     value
        internal static readonly ThreadDictionary<string, Dictionary<string, string>> GlobalLocales;

        //                                        langid  name
        internal static readonly ThreadDictionary<string, string> Languages;

        //                                        langid  locale
        internal static readonly ThreadDictionary<string, MenuLocaleResponse> MenuLocales;

        //                                        edition            side         profile
        internal static readonly ThreadDictionary<string, Dictionary<EPlayerSide, WipeProfile>> WipeProfiles;

        // TODO
        internal static readonly ThreadObject<string> AchievementList;
        internal static readonly ThreadObject<string> AchievementStatistic;
        internal static readonly ThreadObject<string> Globals;
        internal static readonly ThreadObject<string> Handbook;
        internal static readonly ThreadObject<string> HideoutAreas;
        internal static readonly ThreadObject<string> HideoutCustomizationOfferList;
        internal static readonly ThreadObject<string> HideoutProductionRecipes;
        internal static readonly ThreadObject<string> HideoutQteList;
        internal static readonly ThreadObject<string> HideoutSettings;
        internal static readonly ThreadObject<string> Items;
        internal static readonly ThreadObject<string> LocalWeather;
        internal static readonly ThreadObject<string> Locations;
        internal static readonly ThreadObject<string> Prestige;
        internal static readonly ThreadObject<string> Quests;
        internal static readonly ThreadObject<string> Settings;
        internal static readonly ThreadObject<string> Traders;
        internal static readonly ThreadObject<string> Weather;

        static EftDatabase()
        {
            Accounts = new ThreadList<EftAccount>();
            Profiles = new ThreadList<EftProfile>();
            Sessions = new ThreadDictionary<string, int>();

            Customizations = new ThreadDictionary<string, CustomizationTemplate>();
            GlobalLocales = new ThreadDictionary<string, Dictionary<string, string>>();
            Languages = new ThreadDictionary<string, string>();
            MenuLocales = new ThreadDictionary<string, MenuLocaleResponse>();
            WipeProfiles = new ThreadDictionary<string, Dictionary<EPlayerSide, WipeProfile>>();

            // TODO
            AchievementList = new ThreadObject<string>(string.Empty);
            AchievementStatistic = new ThreadObject<string>(string.Empty);
            Globals = new ThreadObject<string>(string.Empty);

            Handbook = new ThreadObject<string>(string.Empty);
            HideoutAreas = new ThreadObject<string>(string.Empty);
            HideoutCustomizationOfferList = new ThreadObject<string>(string.Empty);
            HideoutProductionRecipes = new ThreadObject<string>(string.Empty);
            HideoutQteList = new ThreadObject<string>(string.Empty);
            HideoutSettings = new ThreadObject<string>(string.Empty);
            Items = new ThreadObject<string>(string.Empty);
            LocalWeather = new ThreadObject<string>(string.Empty);
            Locations = new ThreadObject<string>(string.Empty);
            Prestige = new ThreadObject<string>(string.Empty);
            Quests = new ThreadObject<string>(string.Empty);
            Settings = new ThreadObject<string>(string.Empty);
            Traders = new ThreadObject<string>(string.Empty);
            Weather = new ThreadObject<string>(string.Empty);
        }

        // NOTE: load order is VERY important!
        // -- seionmoya, 2024/09/04
        public static void Load()
        {
            // set data source
            Resx.SetSource("eft", typeof(EftDatabase).Assembly);

            // load accounts
            LoadAccounts();
            LoadProfiles();
            LoadSessions();

            // load locales
            LoadLanguages();
            LoadGlobalLocales();
            LoadMenuLocales();

            // load templates
            LoadCustomizations();
            LoadWipeProfiles();

            // TODO
            LoadUnparsed();
        }

        private static void LoadAccounts()
        {
            var path = "./Fuyu/Accounts/EFT/";

            if (!VFS.DirectoryExists(path))
            {
                VFS.CreateDirectory(path);
            }

            var files = VFS.GetFiles(path);

            foreach (var filepath in files)
            {
                var json = VFS.ReadTextFile(filepath);
                var account = Json.Parse<EftAccount>(json);
                EftOrm.SetOrAddAccount(account);

                Terminal.WriteLine($"Loaded EFT account {account.Id}");
            }
        }

        private static void LoadProfiles()
        {
            var path = "./Fuyu/Profiles/EFT/";

            if (!VFS.DirectoryExists(path))
            {
                VFS.CreateDirectory(path);
            }

            var files = VFS.GetFiles(path);

            foreach (var filepath in files)
            {
                var json = VFS.ReadTextFile(filepath);
                var profile = Json.Parse<EftProfile>(json);
                EftOrm.SetOrAddProfile(profile);

                Terminal.WriteLine($"Loaded EFT profile {profile.Pmc._id}");
            }
        }

        private static void LoadSessions()
        {
            // intentionally empty
            // sessions are created when users are logged in successfully
            // -- seionmoya, 2024/09/06
        }

        private static void LoadCustomizations()
        {
            var json = Resx.GetText("eft", "database.client.customization.json");
            var response = Json.Parse<ResponseBody<Dictionary<string, CustomizationTemplate>>>(json);

            foreach (var kvp in response.data)
            {
                EftOrm.SetOrAddCustomization(kvp.Key, kvp.Value);
            }
        }

        private static void LoadLanguages()
        {
            var json = Resx.GetText("eft", $"database.locales.client.languages.json");
            var response = Json.Parse<ResponseBody<Dictionary<string, string>>>(json);

            foreach (var kvp in response.data)
            {
                EftOrm.SetOrAddLanguage(kvp.Key, kvp.Value);
            }
        }

        private static void LoadGlobalLocales()
        {
            var languages = EftOrm.GetLanguages();

            foreach (var languageId in languages.Keys)
            {
                var json = Resx.GetText("eft", $"database.locales.client.locale-{languageId}.json");
                var response = Json.Parse<ResponseBody<Dictionary<string, string>>>(json);

                EftOrm.SetOrAddGlobalLocale(languageId, response.data);
            }
        }

        private static void LoadMenuLocales()
        {
            var languages = EftOrm.GetLanguages();

            foreach (var languageId in languages.Keys)
            {
                var json = Resx.GetText("eft", $"database.locales.client.menu.locale-{languageId}.json");
                var response = Json.Parse<ResponseBody<MenuLocaleResponse>>(json);

                EftOrm.SetOrAddMenuLocale(languageId, response.data);
            }
        }

        private static void LoadWipeProfiles()
        {
            // profile
            var bearJson = Resx.GetText("eft", "database.profiles.player.unheard-bear.json");
            var usecJson = Resx.GetText("eft", "database.profiles.player.unheard-usec.json");
            var savageJson = Resx.GetText("eft", "database.profiles.player.savage.json");

            // customization
            var customizationStorageJson = Resx.GetText("eft", "database.profiles.client.customization.storage.json");

            EftOrm.SetOrAddWipeProfile("unheard", new Dictionary<EPlayerSide, WipeProfile>()
            {
                {
                    EPlayerSide.Bear,
                    new WipeProfile()
                    {
                        Profile = Json.Parse<Profile>(bearJson),
                        Customization = Json.Parse<ResponseBody<CustomizationStorageEntry[]>>(customizationStorageJson).data
                    }
                },
                {
                    EPlayerSide.Usec,
                    new WipeProfile()
                    {
                        Profile = Json.Parse<Profile>(usecJson),
                        Customization = Json.Parse<ResponseBody<CustomizationStorageEntry[]>>(customizationStorageJson).data
                    }
                },
                {
                    EPlayerSide.Savage,
                    new WipeProfile()
                    {
                        Profile = Json.Parse<Profile>(savageJson),
                        Customization = []
                    }
                }
            });
        }

        // TODO
        private static void LoadUnparsed()
        {
            AchievementList.Set(Resx.GetText("eft", "database.client.achievement.list.json"));
            AchievementStatistic.Set(Resx.GetText("eft", "database.client.achievement.statistic.json"));
            Globals.Set(Resx.GetText("eft", "database.client.globals.json"));
            Handbook.Set(Resx.GetText("eft", "database.client.handbook.templates.json"));
            HideoutAreas.Set(Resx.GetText("eft", "database.client.hideout.areas.json"));
            HideoutCustomizationOfferList.Set(Resx.GetText("eft", "database.client.hideout.customization.offer.list.json"));
            HideoutProductionRecipes.Set(Resx.GetText("eft", "database.client.hideout.production.recipes.json"));
            HideoutQteList.Set(Resx.GetText("eft", "database.client.hideout.qte.list.json"));
            HideoutSettings.Set(Resx.GetText("eft", "database.client.hideout.settings.json"));
            Items.Set(Resx.GetText("eft", "database.client.items.json"));
            LocalWeather.Set(Resx.GetText("eft", "database.client.localGame.weather.json"));
            Locations.Set(Resx.GetText("eft", "database.client.locations.json"));
            Prestige.Set(Resx.GetText("eft", "database.client.prestige.list.json"));
            Quests.Set(Resx.GetText("eft", "database.client.quest.list.json"));
            Settings.Set(Resx.GetText("eft", "database.client.settings.json"));
            Traders.Set(Resx.GetText("eft", "database.client.trading.api.traderSettings.json"));
            Weather.Set(Resx.GetText("eft", "database.client.weather.json"));
        }
    }
}