using HarmonyLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using JumpKing.Mods;
using JumpKing.PauseMenu;
using HidePauseMenu.Menu;

namespace HidePauseMenu;
[JumpKingMod("JeFi.HidePauseMenu")]
public static class HidePauseMenu
{
    const string IDENTIFIER = "JeFi.HidePauseMenu";
    const string HARMONY_IDENTIFIER = "JeFi.HidePauseMenu.Harmony";
    const string SETTINGS_FILE = "JeFi.HidePauseMenu.Preferences.xml";

    public static string AssemblyPath { get; private set; }
    public static Preferences Prefs { get; private set; }

    [BeforeLevelLoad]
    public static void BeforeLevelLoad()
    {
        AssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
#if DEBUG
        Debugger.Launch();
        Debug.WriteLine("------");
        Harmony.DEBUG = true;
        Environment.SetEnvironmentVariable("HARMONY_LOG_FILE", $@"{AssemblyPath}\harmony.log.txt");
#endif
        try {
            Prefs = XmlSerializerHelper.Deserialize<Preferences>($@"{AssemblyPath}\{SETTINGS_FILE}");
        } catch (Exception e) {
            Debug.WriteLine($"[ERROR] [{IDENTIFIER}] {e.Message}");
            Prefs = new Preferences();
        }
        Prefs.PropertyChanged += SaveSettingsOnFile;

        Harmony harmony = new Harmony(HARMONY_IDENTIFIER);

        try {
            new Patching.PauseManager(harmony);
        } catch (Exception e) {
            Debug.WriteLine(e.ToString());
        }

#if DEBUG
        Environment.SetEnvironmentVariable("HARMONY_LOG_FILE", null);
#endif
    }


    #region Menu Items

    [MainMenuItemSetting]
    [PauseMenuItemSetting]
    public static ToggleHidePauseMenu ToggleHidePauseMenu(object factory, GuiFormat format)
    {
        return new ToggleHidePauseMenu();
    }

    #endregion

    private static void SaveSettingsOnFile(object sender, System.ComponentModel.PropertyChangedEventArgs args)
    {
        try
        {
            XmlSerializerHelper.Serialize($@"{AssemblyPath}\{SETTINGS_FILE}", Prefs);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"[ERROR] [{IDENTIFIER}] {e.Message}");
        }
    }
}
