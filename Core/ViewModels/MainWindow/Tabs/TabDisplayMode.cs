using System;
namespace Core.ViewModels.MainWindow
{
    public enum TabDisplayMode
    {
        Error,
        ReplaceFocused,
        CreateNewAndFocus,
        CreateNewInBackground,
        TryToFocusInAlreadyOpened,
        SidePane, // TODO: in the Presenter find the focused pane and open a tab there, then focus
        Preview
    }
}
