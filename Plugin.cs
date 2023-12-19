using BepInEx;
using HarmonyLib;

namespace TreysHealthText
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static BepInEx.Logging.ManualLogSource PluginLogger;
        private void Awake()
        {
            PluginLogger = Logger;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            // Initialize Harmony
            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            // Log that patching process has been initiated
            Logger.LogInfo("Harmony patches applied.");
        }
    }
}
