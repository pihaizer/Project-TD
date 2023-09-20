using Godot;

namespace ProjectTD;

public partial class DemoSpawnController : Node
{
    [Export] private Node UnitsContainer { get; set; }
    [Export] private PackedScene UnitScene { get; set; }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            var unit = UnitScene.Instantiate<Unit>();
            UnitsContainer.AddChild(unit);
            unit.Progress = 0;
        }
    }
}