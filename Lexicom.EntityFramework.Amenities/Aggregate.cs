using System.Collections;

namespace Lexicom.EntityFramework.Amenities;
//we want to use System.Text.Json
//but it currently is not a feature
//https://github.com/dotnet/runtime/issues/1808 -> https://github.com/dotnet/runtime/issues/63791
//instead we can do the following with newtonsoft
//https://stackoverflow.com/questions/58193757/is-it-possible-to-replicate-the-behaviour-of-jsonobjectattribute
public class Aggregate<T> : IEnumerable<T>
{
    /*
     * an Aggregate is used for results from a query operation
     * where you want to know how many items could be returned 
     * but only a slice or subset was returned
     * 
     * example: query users that are active but only take '5'
     * the Aggregate's TotalCount might be '100' 
     * but the Slice.Count() is only '5' since thats all that was taken
     */

    /// <exception cref="ArgumentNullException"/>
    public Aggregate(
        int totalCount, 
        IEnumerable<T> slice)
    {
        ArgumentNullException.ThrowIfNull(slice);

        TotalCount = totalCount;
        Slice = slice;
    }

    public int TotalCount { get; }
    public IEnumerable<T> Slice { get; }

    public IEnumerator<T> GetEnumerator() => Slice.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
