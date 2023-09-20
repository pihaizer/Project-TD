using Godot;

namespace ProjectTD;

public partial class Player : CharacterBody2D
{
    [Export] public float MoveSpeed { get; set; } = 100f;
    
    [Export] public Node2D ShootPosition { get; set; }
    
    [Export] public PackedScene BulletScene { get; set; }
    
    [Signal]
    public delegate void ShotEventHandler();

    public override void _PhysicsProcess(double delta)
    {
        var direction = new Vector2(
            Input.GetAxis("player_move_left", "player_move_right"),
            Input.GetAxis("player_move_up", "player_move_down"));

        Velocity = direction.Normalized() * MoveSpeed;
        MoveAndSlide();
        
        bool shot = Input.IsActionJustPressed("player_shoot");
        if (shot)
        {
            EmitSignal(nameof(Shot));
        }
    }

    public override void _Process(double delta)
    {
        LookAt(GetGlobalMousePosition());
    }

    private void Shoot()
    {
        var bullet = BulletScene.Instantiate<Bullet>();
        bullet.GlobalPosition = ShootPosition.GlobalPosition;
        bullet.SetTargetPosition(GetGlobalMousePosition());
        bullet.Damage = 10f;
        GetParent().AddChild(bullet);
    }
}