namespace Lexicom.Validation;
public interface IRuleSetTransfromer<TProperty, TInProperty>
{
    string ErrorMessageTypeName { get; }

    bool TryTransform(TInProperty inProperty, out TProperty property);
}
