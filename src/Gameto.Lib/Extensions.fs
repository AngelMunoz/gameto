namespace Gameto.Lib.Extensions

open System.Runtime.CompilerServices
open Godot

[<Extension>]
type NullableExtensions =

    [<Extension>]
    static member ToOption<'TValue when 'TValue: null>(value: 'TValue) =
        match value with
        | null -> None
        | _ -> Some value

    [<Extension>]
    static member ToVOption<'TValue when 'TValue: null>(value: 'TValue) =
        match value with
        | null -> ValueNone
        | _ -> ValueSome value

[<Extension>]
type InputExtensions =

    [<Extension>]
    static member IsMovingWithKeyboard(event: InputEvent) =
        event.IsActionPressed "move_up"
        || event.IsActionPressed "move_down"
        || event.IsActionPressed "move_left"
        || event.IsActionPressed "move_right"
