using AutoCompanyBuilding.Patches;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace AutoCompanyBuilding
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class AutoCompanyBuildingBase : BaseUnityPlugin
    {
        private const string                    PLUGIN_GUID = "La_Chips.AutoCompanyBuilding";
        private const string                    PLUGIN_NAME = "Auto Company Building";
        private const string                    PLUGIN_VERSION = "1.2.1.0";

        private readonly Harmony                harmony = new Harmony(PLUGIN_GUID);

        private static AutoCompanyBuildingBase  Instance;

        public static ManualLogSource           logSource;
        public static int                       groupCredits;
        public static bool                      hasRerouted = false;

        private void Awake()
        {
            logSource = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);

            if (Instance == null)
            {
                Instance = this;
                logSource.LogInfo("Creating static instance");
            }

            logSource.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");

            harmony.PatchAll(typeof(AutoCompanyBuildingBase));
            harmony.PatchAll(typeof(TerminalPatch));
            harmony.PatchAll(typeof(StartOfRoundPatch));
        }
    }
}
