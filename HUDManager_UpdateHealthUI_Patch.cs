using UnityEngine;
using TMPro;
using HarmonyLib;

namespace TreysHealthText
{
    [HarmonyPatch(typeof(HUDManager), "UpdateHealthUI")]
    public static class HUDManager_UpdateHealthUI_Patch
    {
        static void Postfix(HUDManager __instance, int health)
        {
            // Find or create the TextMeshPro object
            TextMeshProUGUI healthText = GameObject.Find("PlayerHealthText")?.GetComponent<TextMeshProUGUI>();
            if (healthText == null)
            {
                // Use the weightCounter's parent as the parent for the new text object
                Transform parentTransform = __instance.weightCounter.transform.parent;

                // Create a new GameObject for the TextMeshPro element
                GameObject textObj = new GameObject("PlayerHealthText");
                textObj.transform.SetParent(parentTransform, false);

                // Add and configure the TextMeshPro component
                healthText = textObj.AddComponent<TextMeshProUGUI>();

                // Clone properties from weightCounter
                TextMeshProUGUI referenceText = __instance.weightCounter;
                healthText.font = referenceText.font;
                healthText.fontSize = referenceText.fontSize;
                healthText.color = referenceText.color;
                healthText.alignment = TextAlignmentOptions.Center; // Center alignment
                healthText.enableAutoSizing = referenceText.enableAutoSizing;
                healthText.fontSizeMin = referenceText.fontSizeMin;
                healthText.fontSizeMax = referenceText.fontSizeMax;

                // Adjust position relative to the new parent
                RectTransform rectTransform = textObj.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(-82, 25); // Adjust position as needed

                Plugin.PluginLogger.LogInfo("Health Text Asset Created.");
            }

            // Update the text to show the current health
            healthText.text = $"HP\n{health}";

            // Destroy the text object if the player's health is 0 or less
            if (health <= 0)
            {
                GameObject.Destroy(healthText.gameObject);
            }
        }
    }
}
