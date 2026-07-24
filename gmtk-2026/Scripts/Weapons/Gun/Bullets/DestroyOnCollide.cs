using Godot;
using System;

public partial class DestroyOnCollide : Node2D
{
	private Destructable destructable; 

	public override void _Ready()
	{
		destructable = GetNode<Destructable>("../Destructable");
	}
	private void onCollision(Node2D body, KinematicCollision2D collision)
	{
		destructable.signalDestroyed(body);
		
		body.QueueFree();
	}
}
