using Godot;
using System;

public partial class Movable : Node2D
{
	
	[Export]
	public float speed { get; set; } = 400; 
	[Export]
	public float acceleration = 10f;

	[Export]
	private CharacterBody2D body; 

	private Vector2 direction = Vector2.Zero; 
	private float currentSpeed; 
	private Vector2 targetDirection = Vector2.Zero; 
	public void move(Vector2 direction)
	{
		// EmitSignal(SignalName.OnMove, moved, direction);
		this.direction = direction;
	}

	public void changeSpeedTemporarily(float speed)
	{
		this.currentSpeed = speed;
	}

	public void revertSpeed()
	{
		this.currentSpeed = speed;
	}

	public void changeSpeedPermanently(float speed)
	{
		this.speed = speed; 
		currentSpeed = speed; 
	}

	public override void _Ready()
	{
		revertSpeed();
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 step = direction * currentSpeed * (float)delta; 
		KinematicCollision2D collision = body.MoveAndCollide(step);
		if (collision == null) return; 
		
		GodotObject collidedWith = collision.GetCollider(); 

		// If it does something to an object when being touched, do that
		if (body is Collider collider && collidedWith is Collidable otherCollidable) collider.onCollidingOther(otherCollidable);
		

		// If it does something to an object touching it, do that
		if (body is Collidable collidable && collidedWith is Collider otherCollider) collidable.onCollidedWith(otherCollider);
		
		
	}


}
