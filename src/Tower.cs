using Godot;

namespace ProjectTD;

public partial class Tower : Node
{
	[Export] private Node2D Base { get; set; }
	[Export] private Node2D Turret { get; set; }
	[Export] private Node2D FirePoint { get; set; }
	
	[Export] private PackedScene BulletScene { get; set; }
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	
	public void LookAt(Vector2 target)
	{
		Turret.LookAt(target);
		Turret.Transform = Turret.Transform.RotatedLocal(Mathf.Pi/2);
	}

	public void Shoot(Node2D target)
	{
		var bullet = BulletScene.Instantiate<Bullet>();
		bullet.GlobalPosition = FirePoint.GlobalPosition;
		bullet.Target = target;
		GetParent().AddChild(bullet);
	}
}