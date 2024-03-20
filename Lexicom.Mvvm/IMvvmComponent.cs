using System.ComponentModel;

namespace Lexicom.Mvvm;
public interface IMvvmComponent<TViewModel> where TViewModel : INotifyPropertyChanged
{
    TViewModel ViewModel { get; }
    Task InvokeStateChange();
}
