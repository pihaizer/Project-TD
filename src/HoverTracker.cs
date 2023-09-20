using System;
using System.Collections.Generic;
using Godot;

namespace ProjectTD;

public partial class HoverTracker : Area2D
{
    private readonly List<Unit> UnitsInRange = new();
    public static Unit? Target { get; private set; }

    public static event Action<Unit?> TargetChanged;
    
    public static HoverTracker? Singleton { get; private set; }
    
    public override void _Ready()
    {
        if (Singleton != null && Singleton != this)
        {
            GD.PushWarning("There can only be one HoverTracker");
            QueueFree();
            return;
        }
        
        Singleton = this;
        
        AreaEntered += OnAreaEntered;
        AreaExited += OnAreaExited;
        
        // DEBUG
        TargetChanged += unit => GD.PrintS("Target changed to", unit?.Name);
    }
    
    public override void _Process(double delta)
    {
        base._Process(delta);
        GlobalPosition = GetGlobalMousePosition();
    }

    private void OnAreaEntered(Area2D area)
    {
        var unit = area.GetParent<Unit>();
        if (unit == null) return;
        AddUnit(unit);
    }
    
    private void OnAreaExited(Area2D area)
    {
        var unit = area.GetParent<Unit>();
        if (unit == null) return;
        RemoveUnit(unit);
    }
    
    private void AddUnit(Unit unit)
    {
        unit.Died += () => UnitsInRange.Remove(unit);
        UnitsInRange.Add(unit);
        SortUnits();
        SetTarget(UnitsInRange[0]);
    }
    
    private void RemoveUnit(Unit unit)
    {
        UnitsInRange.Remove(unit);
        SortUnits();
        SetTarget(UnitsInRange.Count > 0 ? UnitsInRange[0] : null);
    }

    private void SortUnits()
    {
        if (UnitsInRange.Count == 0) return;
        UnitsInRange.Sort((a, b) =>
        {
            float aDistance = a.GlobalPosition.DistanceSquaredTo(GlobalPosition);
            float bDistance = b.GlobalPosition.DistanceSquaredTo(GlobalPosition);
            return aDistance.CompareTo(bDistance);
        });
    }

    private void SetTarget(Unit? unit)
    {
        if (Target == unit) return;
        Target = unit;
        TargetChanged?.Invoke(Target);
    }
}