namespace Gameto.Lib

module Say =
    open System.Diagnostics
    let hello name = Debug.WriteLine $"Hello %s{name}"
