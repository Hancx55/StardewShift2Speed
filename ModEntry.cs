using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using HarmonyLib;
using Microsoft.Xna.Framework.Input;

namespace Shift2Speed
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <paramname="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(this.ModManifest.UniqueID);

            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Farmer), nameof(Farmer.getMovementSpeed))]
        public static class SpeedPatch
        {
            public static void Postfix(Farmer __instance, ref float __result)
            {
                KeyboardState state = Keyboard.GetState();
                bool sprinting = state.IsKeyDown(Keys.LeftShift);

                if (sprinting)
                {
                    __result = 8;
                }
                else
                {
                    __result = 5;
                }
            }
        }
    }
}