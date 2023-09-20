using Godot;

namespace ProjectTD;

public partial class UnitHealthUI : Control
{
    [Export] private Unit Unit { get; set; }
    [Export] private TextureRect HealthBarTextureRect { get; set; }
    
    private float _healthBarWidth;

    public override void _Ready()
    {
        Unit.HealthChanged += OnHealthChanged;
        _healthBarWidth = HealthBarTextureRect.Size.X;
    }

    private void OnHealthChanged(float health)
    {
        HealthBarTextureRect.Size = new Vector2(_healthBarWidth * health / Unit.MaxHealth, HealthBarTextureRect.Size.Y);
    }
}