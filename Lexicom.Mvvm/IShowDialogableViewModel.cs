using System.Windows.Input;

namespace Lexicom.Mvvm;
public interface IShowDialogableViewModel
{
    ICommand? ShowDialogCommand { set; }
}
