using System.Collections;

namespace Lexicom.EntityFramework.Amenities;
//we want to use System.Text.Json
//but it currently is not a feature
//https://github.com/dotnet/runtime/issues/1808 -> https://github.com/dotnet/runtime/issues/63791
//instead we can do the following with newtonsoft
//https://stackoverflow.com/questions/58193757/is-it-possible-to-replicate-the-behaviour-of-jsonobjectattribute
public class Slice<T> : IEnumerable<T>
{
    /*
     * a Slice is used for results from a query operation
     * where you want to know how many items could be returned 
     * but only a slice or subset was returned
     * 
     * example: query users that are active but only take '5'
     * the Slice's 'TotalCount' might be '100' if there are 100 active users 
     * but the Slice.Count() (The 'Values' enumerable count) is only '5'
     */

    /// <exception cref="ArgumentNullException"/>
    public Slice(
        int totalCount, 
        IEnumerable<T> slice)
    {
        ArgumentNullException.ThrowIfNull(slice);

        TotalCount = totalCount;
        Values = slice;
    }

    public int TotalCount { get; }
    public IEnumerable<T> Values { get; }

    public IEnumerator<T> GetEnumerator() => Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
