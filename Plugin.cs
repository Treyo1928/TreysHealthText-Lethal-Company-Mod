using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace TreysHealthText
{
    [BepInPlugin("TreysHealthText", "TreysHealthText", "1.3.3")]
    public class Plugin : BaseUnityPlugin
    {
        internal static BepInEx.Logging.ManualLogSource PluginLogger;
        internal static ConfigEntry<bool> HideSystemsOnline;
        internal static ConfigEntry<string> HpTextPosition;
        internal static ConfigEntry<bool> UnderlineTopLine;
        internal static ConfigEntry<string> HPLabelName;
        internal static ConfigEntry<string> TextColor;
        internal static ConfigEntry<int> XOffset;
        internal static ConfigEntry<int> YOffset;
        internal static ConfigEntry<bool> RainbowText;
        internal static ConfigEntry<float> RainbowSpeed;

        private void Awake()
        {
            PluginLogger = Logger;

            // Define the configuration options
            HideSystemsOnline = Config.Bind("General",
                                         "HideSystemsOnline",
                                         false,
                                         "Does Systems Online text pop up when joining lobbies? (Bug turned into a feature :D)");
            HpTextPosition = Config.Bind("General",
                                         "HpTextPosition",
                                         "Right",
                                         "Choose to display the 'HP' text above, below, left, or right of the number. Options: Above, Below, Left, Right");
            UnderlineTopLine = Config.Bind("General",
                                           "UnderlineTopLine",
                                           false,
                                           "Set to true to underline the top line of the health text. Options: true, false");
            HPLabelName = Config.Bind("General",
                                      "HPLabelName",
                                      "HP",
                                      "Set to whatever label you want for the display, leave blank if you don't want a label.");
            TextColor = Config.Bind("General",
                                    "TextColor",
                                    "243.015, 100.47, 0",
                                    "Color of the text in RGB format (e.g., '255,0,0' for red).");
            XOffset = Config.Bind("Position",
                                  "XOffset",
                                  0,
                                  "Horizontal offset for the health text position.");
            YOffset = Config.Bind("Position",
                                  "YOffset",
                                  0,
                                  "Vertical offset for the health text position.");
            RainbowText = Config.Bind("Funny",
                                      "RainbowText",
                                      false,
                                      "Set the true if you want a laggy rainbow nightmare. Options: true, false");
            RainbowSpeed = Config.Bind("Funny",
                                       "RainbowSpeed",
                                       0.3f,
                                       "Changes speed at which the rainbow color cycles :D (Input as a float)");

            Logger.LogInfo("Plugin TreysHealthText is loaded!");

            // Initialize Harmony
            Harmony harmony = new Harmony("TreysHealthText");
            harmony.PatchAll();

            Logger.LogInfo("Harmony patches applied.");
        }
    }
}