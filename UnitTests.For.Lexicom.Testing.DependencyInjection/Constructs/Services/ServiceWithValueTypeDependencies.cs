namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithValueTypeDependencies
{
    public ServiceWithValueTypeDependencies(
        int intValueType,
        char charValueType)
    {
        IntValueType = intValueType;
        CharValueType = charValueType;
    }

    public int IntValueType { get; }
    public char CharValueType { get; }
}
