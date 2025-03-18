using JumpKing.PauseMenu.BT.Actions;

namespace HidePauseMenu.Menu;
public class ToggleHidePauseMenu : ITextToggle
{
    public ToggleHidePauseMenu() : base(HidePauseMenu.Prefs.isHidePauseMenu) {}

    protected override string GetName() => "Hide Pause Menu";

    protected override void OnToggle()
    {
        HidePauseMenu.Prefs.isHidePauseMenu = toggle;
    }
}
