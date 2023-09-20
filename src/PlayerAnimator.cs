using Godot;

namespace ProjectTD;

public partial class PlayerAnimator : Node
{
    [Export] private Sprite2D IdleSprite { get; set; }
    [Export] private Sprite2D ShootSprite { get; set; }

    private Player _player;
    private Timer _shootToIdleTimer;

    public override void _Ready()
    {
        _player = GetParent() as Player;
        if (_player == null)
        {
            GD.PrintErr("PlayerAnimator must be a child of Player");
            QueueFree();
            return;
        }
        
        _player.Shot += OnShoot;
        
        _shootToIdleTimer = new Timer();
        AddChild(_shootToIdleTimer);
        _shootToIdleTimer.Timeout += OnShootToIdleTimerTimeout;
        
        OnShootToIdleTimerTimeout();
    }
    
    private void OnShoot()
    {
        IdleSprite.Visible = false;
        ShootSprite.Visible = true;
        _shootToIdleTimer.Start(0.1f);
    }
    
    private void OnShootToIdleTimerTimeout()
    {
        IdleSprite.Visible = true;
        ShootSprite.Visible = false;
    }
}