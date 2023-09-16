using System.Collections.Generic;
using System.Linq;
using Godot;

namespace ProjectTD;

[GlobalClass]
public partial class TowerFollowMouse : Node
{
    [Export] private Node TowersRoot { get; set; }
    [Export] private Node2D TowersTarget { get; set; }

    private List<Tower> _towers;

    public override void _Ready()
    {
        _towers = TowersRoot.GetChildren().Cast<Tower>().ToList();
    }

    public override void _Process(double delta)
    {
        if (_towers == null) return;
        
        foreach (var tower in _towers)
        {
            tower.LookAt(TowersTarget.GlobalPosition);
        }

        
        if (Input.IsActionJustPressed("Shoot"))
        {
            foreach (var tower in _towers)
            {
                tower.Shoot(TowersTarget);
            }
        }
    }
}