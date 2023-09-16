using Godot;

namespace ProjectTD;

public partial class HitParticles : Node2D
{
    [Export] public GpuParticles2D Particles { get; set; }

    public override void _Ready()
    {
        Particles.Restart();
        var timer = new Timer();
        AddChild(timer);
        timer.WaitTime = Particles.Lifetime;
        timer.Start();
        timer.Connect("timeout", Callable.From(OnTimeout));
    }

    private void OnTimeout()
    {
        QueueFree();
    }
}