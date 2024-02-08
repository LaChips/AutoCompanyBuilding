using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
