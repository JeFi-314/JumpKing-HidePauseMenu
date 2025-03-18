using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HidePauseMenu;
public class Preferences : INotifyPropertyChanged
{
    private bool _hidePauseMenu = false;
    public bool isHidePauseMenu
    {
        get => _hidePauseMenu;
        set
        {
            _hidePauseMenu = value;
            OnPropertyChanged();
        }
    }

    #region INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}