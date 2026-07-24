using Godot;
using System;

public partial class Movable : Node2D
{
	[Signal] 
	public delegate void OnMoveEventHandler(CharacterBody2D moved, Vector2 direction); 

	
	
	[Export]
	public float speed { get; set; } = 400; 

	private CharacterBody2D body; 

	private Collidable collidable; 

	private Vector2 direction = Vector2.Zero; 
	public void move(Vector2 direction)
	{
		// EmitSignal(SignalName.OnMove, moved, direction);
		this.direction = direction;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		body = GetParent<CharacterBody2D>(); 
		collidable = GetNode<Collidable>("../Collidable");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 step = direction * speed * (float)delta; 
		KinematicCollision2D collision = body.MoveAndCollide(step);
		if (collision != null) collidable.collision(collision);
	}


}
