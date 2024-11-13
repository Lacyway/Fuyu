using BepInEx;
using Fuyu.Plugin.Arena.Patches;
using Fuyu.Plugin.Arena.Utils;
using Fuyu.Plugin.Common.Reflection;

namespace Fuyu.Plugin.Arena
{
    [BepInPlugin("com.fuyu.plugin.arena", "Fuyu.Plugin.Arena", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private readonly APatch[] _patches;

        public Plugin()
        {
            _patches = new APatch[]
            {
                new BattlEyePatch(),
                new ConsistencyGeneralPatch()
            };
        }

        protected void Awake()
        {
            Logger.LogInfo("[Fuyu.Plugin.Arena] Patching...");

            // NOTE: disable this for packet dumping
            ProtocolUtil.RemoveTransportPrefixes();

            foreach (var patch in _patches)
            {
                patch.Enable();
            }
        }

        protected void OnApplicationQuit()
        {
            Logger.LogInfo("[Fuyu.Plugin.Arena] Unpatching...");

            foreach (var patch in _patches)
            {
                patch.Disable();
            }
        }
    }
}