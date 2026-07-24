using Godot;
using System;

public partial class DestroyOnTimeout : Node2D
{
	private Destructable destructable; 

	public override void _Ready()
	{
		destructable = GetNode<Destructable>("../Destructable");
	}
	void onTimeout(Node2D timedOutObject)
	{
		destructable.signalDestroyed(timedOutObject);
		timedOutObject.QueueFree();
	}
}
