using Godot;

namespace ProjectTD;

public partial class Unit : PathFollow2D
{
    [Export] public float Speed { get; set; } = 100f;

    public override void _Process(double delta)
    {
        Progress += Speed * (float) delta;
    }
}