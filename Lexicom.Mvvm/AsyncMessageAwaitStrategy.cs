namespace Lexicom.Mvvm;

public enum AsyncMessageAwaitStrategy
{
    //iterates each message recipient one at a time and awaits each before calling the next one.
    ForeachAwait,
    //awaits all message recipient calls at once, this will mean messages may be 'received' in any order
    ForeachWhenAll,
}
