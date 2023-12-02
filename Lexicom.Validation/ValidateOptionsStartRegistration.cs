namespace Lexicom.Validation.Options;
public class ValidateOptionsStartRegistration
{
    public ValidateOptionsStartRegistration(
        Type optionsType, 
        string optionsName)
    {
        OptionsType = optionsType;
        OptionsName = optionsName;
    }

    public Type OptionsType { get; }
    public string OptionsName { get; }
}
