using UnityEngine;
using HarmonyLib;
using GameNetcodeStuff;

namespace TreysHealthText
{
    [HarmonyPatch(typeof(HUDManager), "Awake")]
    public static class HUDManager_Awake_Patch
    {
        static void Postfix(HUDManager __instance)
        {
            // Access the PlayerControllerB instance
            PlayerControllerB playerController = GameObject.FindObjectOfType<PlayerControllerB>();
            if (playerController != null)
            {
                // Update the health UI with the player's current health
                // Assuming 'health' is a publicly accessible property of PlayerControllerB
                int playerHealth = playerController.health;
                HUDManager.Instance.UpdateHealthUI(playerHealth, true);
                Plugin.PluginLogger.LogInfo("UpdatedHealth");
            }
            else
            {
                Plugin.PluginLogger.LogError("PlayerControllerB not found.");
            }
        }
    }
}
