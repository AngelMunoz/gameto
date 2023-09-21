namespace Gameto.Lib

open Godot

module Say =
    open System.Diagnostics
    let hello name = Debug.WriteLine $"Hello %s{name}"



module PlayerMovement =


    let animateKeyboardMovement (sprite: AnimatedSprite2D option) =
        match sprite with
        | None -> ()
        | Some sprite ->
            if Input.IsActionPressed "move_up" then
                sprite.Play "walking_up"
            elif Input.IsActionPressed "move_down" then
                sprite.Play "walking_down"
            elif Input.IsActionPressed "move_left" then
                sprite.Play "walking_left"
            elif Input.IsActionPressed "move_right" then
                sprite.Play "walking_right"
            else
                sprite.Stop()

    let moveWithKeyboard (player: CharacterBody2D) (speed: float32) : unit =

        let direction = Input.GetVector("move_left", "move_right", "move_up", "move_down")
        player.Velocity <- direction * speed
        player.MoveAndSlide() |> ignore

    let moveWithMouse (player: CharacterBody2D) (target: Vector2) (speed: float32) =
        player.Velocity <- player.Position.DirectionTo(target) * speed

        if player.Position.DistanceTo(target) > 10.f then
            player.MoveAndSlide() |> ignore
