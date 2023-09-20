using System;
using Godot;

namespace ProjectTD;

public partial class CameraController : Camera2D
{
    [Export] public float Speed { get; set; } = 100f;
    [Export] public Node2D Target { get; set; }

    public override void _Ready()
    {
        // GD.PrintS(GetMovementLimits(), GetViewportRect().Position, GetViewportRect().Size);
    }

    public override void _Process(double delta)
    {
        // var input = new Vector2(
        //     Input.GetActionStrength("CameraRight") - Input.GetActionStrength("CameraLeft"),
        //     Input.GetActionStrength("CameraDown") - Input.GetActionStrength("CameraUp")
        // );

        // GlobalTranslate(input.Normalized() * Speed * (float) delta);
        GlobalPosition = Target.GlobalPosition;
        // var limits = GetMovementLimits();
        // if (!limits.HasPoint(GlobalPosition))
        // {
        //     GlobalPosition = GetClosestPoint(GlobalPosition, limits);
        // }


        // GlobalPosition += input.Normalized() * Speed * (float) delta;
    }

    // private Rect2 GetMovementLimits()
    // {
    //     return new Rect2(
    //         new Vector2((LimitRight - LimitLeft) / 2, (LimitBottom - LimitTop) / 2),
    //         new Vector2(LimitRight - LimitLeft - GetViewportRect().Size.X,
    //             LimitBottom - LimitTop - GetViewportRect().Size.Y));
    // }
    //
    // private Vector2 GetClosestPoint(Vector2 point, Rect2 rect)
    // {
    //     var x = Mathf.Clamp(point.X, rect.Position.X - rect.Size.X / 2, rect.Position.X + rect.Size.X / 2);
    //     var y = Mathf.Clamp(point.Y, rect.Position.Y - rect.Size.Y / 2, rect.Position.Y + rect.Size.Y / 2);
    //     return new Vector2(x, y);
    // }
}