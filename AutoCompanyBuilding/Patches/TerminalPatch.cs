using HarmonyLib;

namespace AutoCompanyBuilding.Patches
{
    [HarmonyPatch(typeof(Terminal))]
    internal class TerminalPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void gatherGroupCreditsValue(ref int ___groupCredits)
        {
            AutoCompanyBuildingBase.groupCredits = ___groupCredits;
        }
    }
}
