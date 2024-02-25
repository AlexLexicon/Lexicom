using System.ComponentModel;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly;
public interface IView<TViewModel> where TViewModel : INotifyPropertyChanged
{
    TViewModel ViewModel { get; }
    Task InvokeStateChange();
}
