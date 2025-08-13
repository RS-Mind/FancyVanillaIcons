using BepInEx;
using HarmonyLib;
using UnboundLib.GameModes;
using Jotunn.Utils;
using UnityEngine;
using System.Collections;
using UnboundLib;
using FancyCardBar;

namespace FancyVanillaIcons
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.rsmind.rounds.fancycardbar", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class FancyVanillaIcons : BaseUnityPlugin
    {
        private const string ModId = "com.rsmind.rounds.fancyvanillaicons";
        private const string ModName = "Fancy Vanilla Icons";
        public const string Version = "1.0.3";
        public const string ModInitials = "FVI";
        public static FancyVanillaIcons instance { get; private set; }

        void Awake()
        {
            Unbound.RegisterClientSideMod(ModId);
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }


        void Start()
        {
            instance = this;


            assets = AssetUtils.LoadAssetBundleFromResources("fancyvanillaicons", typeof(FancyVanillaIcons).Assembly);

            if (assets == null)
            {
                UnityEngine.Debug.Log("Failed to load Fancy Vanilla Icons asset bundle");
            }

            foreach (CardInfo cardInfo in CardChoice.instance.cards)
            {
                FancyIcon icon = cardInfo.gameObject.AddComponent<FancyIcon>();
                icon.fancyIcon = assets.LoadAsset<GameObject>("I_" + cardInfo.cardName.ToLower());
            }
        }

        public static bool Debug = false;
        internal static AssetBundle assets;
    }
}