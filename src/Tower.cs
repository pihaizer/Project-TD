using System.Collections.Generic;
using System.Linq;
using Godot;

namespace ProjectTD;

public partial class Tower : Node
{
    [Export] private Area2D RangeArea { get; set; }
    [Export] private CollisionShape2D RangeShape { get; set; }
    [Export] private Node2D Base { get; set; }
    [Export] private Node2D Turret { get; set; }
    [Export] private Node2D FirePoint { get; set; }
    [Export] private float Range { get; set; } = 600f;
    [Export] private float Damage { get; set; } = 10f;
    [Export] private float FireRate { get; set; } = 1f;

    [Export] private PackedScene BulletScene { get; set; }

    private List<Unit> _unitsInRange = new();
    private Unit _target;
    private Timer _cooldownTimer;

    public override void _Ready()
    {
        if (RangeShape.Shape is CircleShape2D circleShape2D)
        {
            circleShape2D.Radius = Range;
        }
        else
        {
            GD.PushWarning("RangeShape is not a CircleShape2D");
        }
        
        RangeArea.AreaEntered += OnAreaEntered;
        RangeArea.AreaExited += OnAreaExited;

        _cooldownTimer = new Timer { WaitTime = 1f / FireRate };
        AddChild(_cooldownTimer);
        _cooldownTimer.OneShot = true;
    }

    private void OnAreaEntered(Area2D area)
    {
        var unit = area.GetParent<Unit>();
        if (unit == null) return;
        unit.Died += () => _unitsInRange.Remove(unit);
        _unitsInRange.Add(unit);
    }

    private void OnAreaExited(Area2D area)
    {
        var unit = area.GetParent<Unit>();
        if (unit == null) return;
        _unitsInRange.Remove(unit);
    }

    public override void _Process(double delta)
    {
        if (_unitsInRange.Count == 0) return;

        _unitsInRange = _unitsInRange
            .OrderByDescending(u => u.Progress).ToList();

        _target = _unitsInRange[0];
        LookAt(_target.GlobalPosition);
        TryShoot();
    }

    public void LookAt(Vector2 target)
    {
        Turret.LookAt(target);
        Turret.Transform = Turret.Transform.RotatedLocal(Mathf.Pi / 2);
    }
    
    private void TryShoot()
    {
        if (_target == null) return;
        if (!_cooldownTimer.IsStopped()) return;
        Shoot(_target);
    }

    public void Shoot(Node2D target)
    {
        var bullet = BulletScene.Instantiate<Bullet>();
        bullet.GlobalPosition = FirePoint.GlobalPosition;
        bullet.Target = target;
        bullet.Damage = Damage;
        GetParent().AddChild(bullet);
        StartShootCooldown();
    }

    private void StartShootCooldown()
    {
        _cooldownTimer.Start();
    }
}