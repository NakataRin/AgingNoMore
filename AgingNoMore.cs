using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace AgingNoMore
{
    [BepInPlugin("com.Nakata_Rin.AgingNoMore", "AgingNoMore", "1.0")]
    public class Plugin : BaseUnityPlugin
    {
        private Harmony _harmony;

        private void Awake()
        {
            Logger.LogInfo("I love being yong");

            _harmony = new Harmony("com.Nakata_Rin.AgingNoMore");
            _harmony.PatchAll();
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }
    }

    [HarmonyPatch(typeof(CondOwner), "AddCondAmount")]
    public class NoChargenAgingPatch
    {
        static void Prefix(CondOwner __instance, string strName, ref double fAmount)
        {
            if (strName == "StatAge"
                && fAmount > 0
                && __instance.GetCondAmount("IsInChargen") > 0)
            {
                fAmount = 0;
            }
        }
    }
}