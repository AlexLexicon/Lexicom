using System.Windows.Input;

namespace Lexicom.Mvvm;
public interface IHideableViewModel
{
    ICommand? HideCommand { set; }
}
