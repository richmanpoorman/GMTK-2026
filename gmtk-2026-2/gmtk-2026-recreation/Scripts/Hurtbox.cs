using Godot;
using System;

[Tool]
public partial class Hurtbox : Area2D
{
	[Export]
	private CollisionShape2D collider; 
	[Export]
	private Shape2D hurtboxShape
	{
		get => _hurtboxShape; 
		set 
		{
			setShape(value);
		}
	}
	[Export] 
	private ShapeDrawer shapeDrawer; 

	[Signal]
	public delegate void onHitEventHandler(AttackData data, Vector2 position, Vector2 normal); // Add information about what got hit

	private Shape2D _hurtboxShape; 
	private void setShape(Shape2D shape)
	{
		_hurtboxShape = shape; 
		if (collider is null) return; 
		collider.Shape = shape; 
		if (shapeDrawer is null) return; 
		shapeDrawer.shape = shape; 
	}

    public override void _Ready()
    {
        setShape(hurtboxShape);
    }


	public void hit(AttackData data, Vector2 position, Vector2 normal)
	{
		GD.Print($"Hurt at {position}");
		EmitSignal(SignalName.onHit, data, position, normal); 
	}
}
