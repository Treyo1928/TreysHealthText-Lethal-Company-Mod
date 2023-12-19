using UnityEngine;
using TMPro;
using HarmonyLib;

namespace HealthText
{
    [HarmonyPatch(typeof(RoundManager), "LoadNewLevel")]
    public static class RoundManager_LoadNewLevel_Patch
    {
        static void Postfix(RoundManager __instance)
        {
            /*
            // Find the HUDManager instance
            HUDManager hudManager = GameObject.FindObjectOfType<HUDManager>();
            if (hudManager == null || hudManager.weightCounter == null)
            {
                Plugin.PluginLogger.LogError("Could not find the HUD Manager or weightCounter");
                return;
            }

            // Use the weightCounter's parent as the parent for the new text object
            Transform parentTransform = hudManager.weightCounter.transform.parent;

            // Create a new GameObject for the TextMeshPro element
            GameObject textObj = new GameObject("PlayerHealthText");
            textObj.transform.SetParent(parentTransform, false);

            // Add and configure the TextMeshPro component
            TextMeshProUGUI textMeshPro = textObj.AddComponent<TextMeshProUGUI>();

            // Clone properties from weightCounter
            TextMeshProUGUI referenceText = hudManager.weightCounter;
            textMeshPro.font = referenceText.font;
            textMeshPro.fontSize = referenceText.fontSize;
            textMeshPro.alignment = referenceText.alignment;
            textMeshPro.enableAutoSizing = referenceText.enableAutoSizing;
            textMeshPro.fontSizeMin = referenceText.fontSizeMin;
            textMeshPro.fontSizeMax = referenceText.fontSizeMax;

            // Initialize with a default health value
            int currentHealth = 100; // Placeholder, replace with actual health retrieval

            // Set the text with HP underlined on the first line and health value on the second line
            textMeshPro.text = $"<u>HP</u>\n{currentHealth}";
            textMeshPro.color = Color.red; // Set text color to red

            // Adjust position relative to the new parent
            RectTransform rectTransform = textObj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(2, 4); // Adjust position as needed

            Plugin.PluginLogger.LogInfo("Health Text Asset Created.");
            */
            return;
        }
    }
}
