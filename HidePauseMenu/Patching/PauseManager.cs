using HarmonyLib;
using JumpKing.Controller;
using System;
using System.Reflection;

namespace HidePauseMenu.Patching;
// Disable pause menu drawing if pause key is down
public class PauseManager
{
    public PauseManager (Harmony harmony)
    {
        Type type = AccessTools.TypeByName("JumpKing.PauseMenu.PauseManager");
        MethodInfo Draw = AccessTools.Method(type, "Draw");
        harmony.Patch(
            Draw,
            prefix: new HarmonyMethod(AccessTools.Method(typeof(PauseManager), nameof(preDraw)))
        );
    }

    private static bool preDraw() 
    {
        if (HidePauseMenu.Prefs.isHidePauseMenu && ControllerManager.instance.GetPadState().pause)
        {
            return false;
        }
        return true;
    }
}