using Godot;

namespace ProjectTD;

public partial class Bullet : Node2D
{
    [Export] public float Speed { get; set; } = 100f;
    [Export] public PackedScene HitEffect { get; set; }
    
    public Node2D Target { get; set; }
    public float Damage { get; set; }

    private Vector2 TargetPosition => Target?.GlobalPosition ?? _lastTargetPosition;
    
    private Vector2 _lastTargetPosition;
    
    public void SetTargetPosition(Vector2 targetPosition)
    {
        _lastTargetPosition = targetPosition;
    }

    public override void _Process(double delta)
    {
        var direction = (TargetPosition - GlobalPosition).Normalized();
        GlobalPosition += direction * Speed * (float) delta;
        
        var distance = TargetPosition.DistanceTo(GlobalPosition);
        if (distance < Speed * (float)delta)
        {
            Hit();
            return;
        }

        _lastTargetPosition = TargetPosition;
    }

    private void Hit()
    {
        if (Target is IHittable hittable)
        {
            hittable.Hit(Damage);
        }
        CreateHitEffect(TargetPosition);
        QueueFree();
    }
    
    private void CreateHitEffect(Vector2 position)
    {
        var hitEffect = HitEffect.Instantiate();
        GetParent().AddChild(hitEffect);
        (hitEffect as Node2D).GlobalPosition = position;
    }
}