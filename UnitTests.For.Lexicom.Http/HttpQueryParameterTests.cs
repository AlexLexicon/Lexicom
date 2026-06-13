using Lexicom.Http;
using Lexicom.Http.Exceptions;
using Lexicom.Http.UnitTests.Constructs;
using System.Globalization;
using System.Text.Json;

namespace UnitTests.For.Lexicom.Http;

public class HttpQueryParameterTests
{
    private (string expectedName, string expectedValueString, string expectedToString) ArrangeExpectedValues<T>(string name, T expectedValue)
    {
        string expectedName = name;
        string expectedValueString;
        if (expectedValue is DateTimeOffset dateTimeOffset)
        {
            expectedValueString = dateTimeOffset.ToString("o", CultureInfo.InvariantCulture);
        }
        else if (expectedValue is DateTime dateTime)
        {
            expectedValueString = dateTime.ToString("o", CultureInfo.InvariantCulture);
        }
        else
        {
            expectedValueString = expectedValue!.ToString()!;
        }
        string expectedToString = $"{expectedName}={Uri.EscapeDataString(expectedValueString)}";

        return (expectedName, expectedValueString, expectedToString);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("a")]
    [InlineData("ab")]
    [InlineData("abc")]
    [InlineData("this is a test.")]
    [InlineData("`~!@#$%^&*()-_=+[{]}\\|;:'\",<.>/?")]
    public void Successfully_Create_HttpQueryParameter_With_String_Value(string expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("string", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());
    }

    [Fact]
    public void Successfully_Create_HttpQueryParameter_With_Object_Value()
    {
        //arrange
        object expectedValue = new ObjectWithToString();
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("object", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"StringValue\":\"abc\"}")]
    [InlineData("{\"IntValue\":123}")]
    [InlineData("{\"StringValue\":\"abc\",\"IntValue\":123}")]
    public void Successfully_Create_HttpQueryParameter_With_JsonString_Value(string expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("jsonString", expectedValue);
        TestJson? expectedJson = JsonSerializer.Deserialize<TestJson>(expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        TestJson? json = JsonSerializer.Deserialize<TestJson>(parameter.Value);
        Assert.NotNull(json);
        Assert.Equivalent(expectedJson, json);
    }

    [Theory]
    [InlineData("[]")]
    [InlineData("[{}]")]
    [InlineData("[{},{}]")]
    [InlineData("[{\"StringValue\":\"abc\",\"IntValue\":123}]")]
    [InlineData("[{\"StringValue\":\"abc\",\"IntValue\":123},{\"StringValue\":\"def\",\"IntValue\":456}]")]
    public void Successfully_Create_HttpQueryParameter_With_JsonListString_Value(string expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("jsonListString", expectedValue);
        List<TestJson>? expectedJsonList = JsonSerializer.Deserialize<List<TestJson>>(expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        List<TestJson>? jsonList = JsonSerializer.Deserialize<List<TestJson>>(parameter.Value);
        Assert.NotNull(jsonList);
        Assert.Equivalent(expectedJsonList, jsonList);
    }

    [Theory]
    [InlineData(short.MaxValue)]
    [InlineData(100)]
    [InlineData(10)]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-100)]
    [InlineData(short.MinValue)]
    public void Successfully_Create_HttpQueryParameter_With_Short_Value(short expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("short", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        short value = short.Parse(parameter.Value);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData(int.MaxValue)]
    [InlineData(100)]
    [InlineData(10)]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-100)]
    [InlineData(int.MinValue)]
    public void Successfully_Create_HttpQueryParameter_With_Int_Value(int expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("int", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        int value = int.Parse(parameter.Value);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData(long.MaxValue)]
    [InlineData(100)]
    [InlineData(10)]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-100)]
    [InlineData(long.MinValue)]
    public void Successfully_Create_HttpQueryParameter_With_Long_Value(long expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("long", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        long value = long.Parse(parameter.Value);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData(float.MaxValue)]
    [InlineData(100.001)]
    [InlineData(100)]
    [InlineData(10.01)]
    [InlineData(10)]
    [InlineData(1.1)]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-1.1)]
    [InlineData(-10)]
    [InlineData(-10.01)]
    [InlineData(-100)]
    [InlineData(-100.001)]
    [InlineData(float.MinValue)]
    public void Successfully_Create_HttpQueryParameter_With_Float_Value(float expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("float", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        float value = float.Parse(parameter.Value);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData(double.MaxValue)]
    [InlineData(100.001)]
    [InlineData(100)]
    [InlineData(10.01)]
    [InlineData(10)]
    [InlineData(1.1)]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-1.1)]
    [InlineData(-10)]
    [InlineData(-10.01)]
    [InlineData(-100)]
    [InlineData(-100.001)]
    [InlineData(double.MinValue)]
    public void Successfully_Create_HttpQueryParameter_With_Double_Value(double expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("double", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        double value = double.Parse(parameter.Value);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData(100.001)]
    [InlineData(100)]
    [InlineData(10.01)]
    [InlineData(10)]
    [InlineData(1.1)]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-1.1)]
    [InlineData(-10)]
    [InlineData(-10.01)]
    [InlineData(-100)]
    [InlineData(-100.001)]
    public void Successfully_Create_HttpQueryParameter_With_Decimal_Value(decimal expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("decimal", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        decimal value = decimal.Parse(parameter.Value);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData(uint.MaxValue)]
    [InlineData(100)]
    [InlineData(10)]
    [InlineData(1)]
    [InlineData(uint.MinValue)]
    public void Successfully_Create_HttpQueryParameter_With_UInt_Value(uint expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("uint", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        uint value = uint.Parse(parameter.Value);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData(ulong.MaxValue)]
    [InlineData(100)]
    [InlineData(10)]
    [InlineData(1)]
    [InlineData(ulong.MinValue)]
    public void Successfully_Create_HttpQueryParameter_With_ULong_Value(ulong expectedValue)
    {
        //arrange
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("ulong", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        ulong value = ulong.Parse(parameter.Value);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("e43a4865-db3b-4ad1-a63f-c2cc1ff54aee")]
    public void Successfully_Create_HttpQueryParameter_With_Guid_Value(string expectedValueData)
    {
        //arrange
        Guid expectedValue = Guid.Parse(expectedValueData);
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("guid", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        Guid value = Guid.Parse(parameter.Value);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData("2026/05/19 14:31:23")]
    public void Successfully_Create_HttpQueryParameter_With_DateTime_Value(string expectedValueData)
    {
        //arrange
        DateTime expectedValue = DateTime.Parse(expectedValueData);
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("datetime", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        DateTime value = DateTime.Parse(parameter.Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData("2026/05/19 14:31:23")]
    public void Successfully_Create_HttpQueryParameter_With_DateTimeOffset_Value(string expectedValueData)
    {
        //arrange
        DateTimeOffset expectedValue = DateTimeOffset.Parse(expectedValueData);
        (string expectedName, string expectedValueString, string expectedToString) = ArrangeExpectedValues("datetimeoffset", expectedValue);

        //act
        var parameter = new HttpQueryParameter(expectedName, expectedValue);

        //assert
        Assert.Equal(expectedName, parameter.Name);
        Assert.Equal(expectedValueString, parameter.Value);
        Assert.Equal(expectedToString, parameter.ToString());

        DateTimeOffset value = DateTimeOffset.Parse(parameter.Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("&")]
    [InlineData("?")]
    [InlineData("/")]
    [InlineData("=")]
    [InlineData("+")]
    [InlineData("%")]
    [InlineData("te&st")]
    [InlineData("te?st")]
    [InlineData("te/st")]
    [InlineData("te=st")]
    [InlineData("te+st")]
    [InlineData("te%st")]
    [InlineData("te st")]
    public void Fail_To_Create_HttpQueryParameter_With_Invalid_Name(string invalidName)
    {
        //assert
        Assert.Throws<InvalidHttpQueryParameterNameException>(() =>
        {
            //act
            _ = new HttpQueryParameter(invalidName, "value");
        });
    }

    [Fact]
    public void Fail_To_Create_HttpQueryParameter_With_Null_Name()
    {
        //assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //act
            _ = new HttpQueryParameter(null!, "test");
        });
    }

    [Fact]
    public void Fail_To_Create_HttpQueryParameter_With_Null_Value()
    {
        //assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //act
            _ = new HttpQueryParameter("test", null!);
        });
    }
}
