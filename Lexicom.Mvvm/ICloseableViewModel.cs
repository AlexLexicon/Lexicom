using System.Windows.Input;

namespace Lexicom.Mvvm;
public interface ICloseableViewModel
{
    ICommand? CloseCommand { set; }
}
