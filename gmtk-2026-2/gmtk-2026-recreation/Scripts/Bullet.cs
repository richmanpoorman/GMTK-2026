using Godot;
using System;
public partial class Bullet : Node2D
{
	[Export] 
	public float RANGE = 10f;


	[Export] 
	private Raycaster raycaster; 

	[Export]
	private Line2D line;

	[Signal]
	public delegate void onBulletHitEventHandler(Node2D collider, Vector2 position, Vector2 normal); 
	
	private void fire() => raycaster.cast(); 

	private void onHit(Node2D collider, Vector2 position, Vector2 normal) => EmitSignal(SignalName.onBulletHit, collider, position, normal); 

	private void finish() => QueueFree();

	private void onAnimationFinished(StringName name) {
		if (name == "bullet_fire") finish();
	}

	public void setRange(float range) {
		raycaster.setLocalTarget(range * Vector2.Right);
		setLinePoints(range);
	}

	private void setLinePoints(float range)
	{
		int pointCount = line.GetPointCount(); 
		if (pointCount < 2) return; 

		for (int index = 0; index < pointCount; index++)
		{
			double weight = 1.0 * index / (pointCount - 1); 
			float magnitude = (float)Mathf.Lerp(0, range, weight); 
			Vector2 newPosition = magnitude * Vector2.Right; 
			line.SetPointPosition(index, newPosition); 
		}
	}

    public override void _Ready()
    {
        setRange(RANGE);
    }

}
