// Disable general file validation, does NOT disable client bundles validation

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Fuyu.Plugin.Arena.Reflection;
using Fuyu.Plugin.Common.Reflection;

namespace Fuyu.Plugin.Arena.Patches
{
    public class ConsistencyGeneralPatch : APatch
    {
        private static readonly MethodInfo _mi;

        static ConsistencyGeneralPatch()
        {
            var name = "RunFilesChecking";
            var flags = PatchHelper.AnyInstanceFlags;
            var type = PatchHelper.Types.Single(t => t.GetMethod(name, flags) != null);

            _mi = type.GetMethod(name);
        }

        public ConsistencyGeneralPatch() : base("com.fuyu.plugin.arena.consistencygeneral", EPatchType.Prefix)
        {
        }

        protected override MethodBase GetOriginalMethod()
        {
            return _mi;
        }

        protected static bool Patch(ref Task __result)
        {
            __result = Task.CompletedTask;

            // Don't run original code
            return false;
        }
    }
}