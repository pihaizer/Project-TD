using Godot;

namespace ProjectTD;

public partial class Unit : PathFollow2D, IHittable
{
    [Export] public float Speed { get; set; } = 100f;
    [Export] public float MaxHealth { get; set; } = 100f;
    
    [Export] public float Health { get; set; }

    [Signal]
    public delegate void HealthChangedEventHandler(float health);

    [Signal]
    public delegate void DiedEventHandler();

    public override void _Ready()
    {
        Health = MaxHealth;
    }

    public override void _Process(double delta)
    {
        Progress += Speed * (float)delta;
    }

    public void Hit(float damage)
    {
        Health -= damage;
        EmitSignal(nameof(HealthChanged), Health);
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GD.PrintS(Name, "died");
        EmitSignal(nameof(Died));
        QueueFree();
    }
}