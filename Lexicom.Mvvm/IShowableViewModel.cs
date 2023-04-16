using System.Windows.Input;

namespace Lexicom.Mvvm;
public interface IShowableViewModel
{
    ICommand? ShowCommand { set; }
}
