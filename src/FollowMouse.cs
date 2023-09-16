using Godot;

namespace ProjectTD;

public partial class FollowMouse : Node2D
{
    public override void _Process(double delta)
    {
        Transform = new Transform2D(Transform.Rotation, GetGlobalMousePosition());
    }
}