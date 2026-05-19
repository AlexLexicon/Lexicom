using Lexicom.Http.UnitTests.Constructs;
using System.Text.Json;

namespace Lexicom.Http.UnitTests.Tests;

public class HttpQueryStringTests
{
    [Fact]
    public void Parse_Correctly_Parses_Numbers_Strings_And_Json()
    {
        //arrange
        int expectedNumValue = 5;
        string expectedStrValue = "test";
        var expectedJsonValue = new TestJson
        {
            StringValue = "abc",
            IntValue = 123,
        };
        string rawJson = JsonSerializer.Serialize(expectedJsonValue);
        string escapedJson = Uri.EscapeDataString(rawJson);
        string url = $"www.test.com/page?num={expectedNumValue}&str={expectedStrValue}&json={escapedJson}";

        //act
        HttpQueryString queryString = HttpQueryString.Parse(url);

        HttpQueryParameter? num = queryString["num"];
        HttpQueryParameter? str = queryString["str"];
        HttpQueryParameter? json = queryString["json"];

        //assert
        Assert.NotNull(num);
        Assert.NotNull(str);
        Assert.NotNull(json);

        int numValue = int.Parse(num.Value);
        Assert.Equal(expectedNumValue, numValue);

        string strValue = str.Value;
        Assert.Equal(expectedStrValue, strValue);

        TestJson? jsonValue = JsonSerializer.Deserialize<TestJson>(json.Value);
        Assert.NotNull(jsonValue);
        Assert.Equal(expectedJsonValue.StringValue, jsonValue.StringValue);
        Assert.Equal(expectedJsonValue.IntValue, jsonValue.IntValue);
    }

    [Fact]
    public void Parse_Url_Into_Parameters_And_Back()
    {
        //arrange
        string baseUrl = "www.example.com/page";
        string url = $"{baseUrl}?a=1&b=2&c=3";

        //act
        var queryString = HttpQueryString.Parse(url);

        var a = queryString["a"];
        var b = queryString["b"];
        var c = queryString["c"];

        Assert.NotNull(a);
        Assert.NotNull(b);
        Assert.NotNull(c);

        var newQueryString = new HttpQueryString
        {
            a,
            b,
            c,
        };

        string newUrl = newQueryString.ToString(baseUrl);

        Assert.Equal(url, newUrl);
    }

    [Fact]
    public void HttpQueryString_ToString_Format_With_Url()
    {
        //arrange
        string baseUrl = "www.example.com/page";
        string expectedStr = $"{baseUrl}?a=1&b=2&c=3";

        //act
        var queryString = new HttpQueryString
        {
            new HttpQueryParameter("a", 1),
            new HttpQueryParameter("b", 2),
            new HttpQueryParameter("c", 3),
        };

        string str = queryString.ToString(baseUrl);

        Assert.Equal(expectedStr, str);
    }

    [Fact]
    public void HttpQueryString_ToString_Format_With_Url_Without_Parameter()
    {
        //arrange
        string expectedStr = $"www.example.com/page";

        //act
        var queryString = new HttpQueryString
        {
        };

        string str = queryString.ToString(expectedStr);

        Assert.Equal(expectedStr, str);
    }

    [Fact]
    public void HttpQueryString_ToString_Format_Without_Url()
    {
        //arrange
        string expectedString = $"a=1&b=2&c=3";

        //act
        var queryString = new HttpQueryString
        {
            new HttpQueryParameter("a", 1),
            new HttpQueryParameter("b", 2),
            new HttpQueryParameter("c", 3),
        };

        string str = queryString.ToString();

        Assert.Equal(expectedString, str);
    }
}
