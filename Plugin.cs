using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace TreysHealthText
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static BepInEx.Logging.ManualLogSource PluginLogger;
        internal static ConfigEntry<string> HpTextPosition;
        internal static ConfigEntry<bool> UnderlineTopLine;
        internal static ConfigEntry<string> HPLabelName;
        internal static ConfigEntry<int> XOffset;
        internal static ConfigEntry<int> YOffset;

        private void Awake()
        {
            PluginLogger = Logger;

            // Define the configuration options
            HpTextPosition = Config.Bind("General",
                                         "HpTextPosition",
                                         "Above",
                                         "Choose to display the 'HP' text above or below the number. Options: Above, Below");
            UnderlineTopLine = Config.Bind("General",
                                           "UnderlineTopLine",
                                           false,
                                           "Set to true to underline the top line of the health text. Options: true, false");
            HPLabelName = Config.Bind("General",
                                      "HPLabelName",
                                      "HP",
                                      "Set to whatever label you want for the display, leave blank if you don't want a label.");
            XOffset = Config.Bind("Position",
                                  "XOffset",
                                  0,
                                  "Horizontal offset for the health text position.");
            YOffset = Config.Bind("Position",
                                  "YOffset",
                                  0,
                                  "Vertical offset for the health text position.");

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            // Initialize Harmony
            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            Logger.LogInfo("Harmony patches applied.");
        }
    }
}
