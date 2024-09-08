using HarmonyLib;
using UnityEngine;
using TMPro;

namespace TreysHealthText
{
    [HarmonyPatch(typeof(HUDManager), "Update")]
    public static class HUDManager_Update_Patch
    {
        private static float hue = 0f;

        static void Postfix(HUDManager __instance)
        {
            if (!Plugin.RainbowText.Value) return;

            TextMeshProUGUI healthText = HUDManager_UpdateHealthUI_Patch.healthText;
            if (healthText == null)
            {
                Plugin.PluginLogger.LogWarning("healthText is null in Update patch");
                return;
            }

            hue = (hue + Time.deltaTime * Plugin.RainbowSpeed.Value) % 1f;
            Color rainbow = Color.HSVToRGB(hue, 1f, 1f);
            healthText.color = rainbow;

            Plugin.PluginLogger.LogInfo($"Updated color: {rainbow}");
        }
    }
}