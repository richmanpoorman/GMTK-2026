using Godot;
using System;
public partial class Bullet : Node2D
{
	[Export] 
	public float RANGE = 10f;

	[Export(PropertyHint.Layers2DPhysics)]
	public uint COLLISION_MASK; 

	[Export] 
	private Raycaster raycaster; 

	[Export]
	private Line2D line;
	

	private AttackData data; 

	private void fire() => raycaster.cast(); 

	private void onHit(Node2D collider, Vector2 position, Vector2 normal)
	{
		if (collider is Hurtbox hurtbox) hurtbox.hit(data, position, normal);
		if (collider is null) return; 
		Vector2 localPosition = position - this.GlobalPosition; 
		setLinePoints(localPosition.Length());
	}

	private void finish() => QueueFree();

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

	public void setCollisionMask(uint collisionMask)
	{
		raycaster.ray.CollisionMask = collisionMask;
	}

	public void setAttackData(AttackData data)
	{
		this.data = data; 
	}

    public override void _Ready()
    {
		setCollisionMask(COLLISION_MASK);
        setRange(RANGE);
		fire();
    }

}
