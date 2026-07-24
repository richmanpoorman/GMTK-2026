using Godot;
using System;
using System.Collections.Generic; 
using System.Linq; 
public partial class Collidable : Node2D
{
	
	private List<string> canCollideWith;

	[Signal]
	public delegate void OnCollisionEventHandler(CollisionObject2D body, KinematicCollision2D collision);

	private CollisionObject2D body; 

	public void setCollideWith(string[] canCollide)
	{
		canCollideWith = new List<string>(canCollide); 
	}

    public override void _Ready()
    {
        body = GetParent<CollisionObject2D>();
    }


	public void collision(KinematicCollision2D collision)
	{
		if (collision == null) return; 
		Node2D collidedWith = collision.GetCollider() as Node2D; 
		bool canCollide = canCollideWith.Any(n => collidedWith.IsInGroup(n)); 
		if (!canCollide) return; 
		EmitSignal(SignalName.OnCollision, body, collision); 
	}

}
