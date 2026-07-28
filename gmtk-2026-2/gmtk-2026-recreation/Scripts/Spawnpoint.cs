using Godot;
using System;

[Tool]
public partial class Spawnpoint : Node2D
{
	/* 
		Purpose : Puts the instance into the scene with the same global position, rotation, and scale (attaching to the scene root)
		Note    : Only responsible for putting the Node into the world without a parent; 
				  is NOT responsible for making and setting up the spawnable; that should be done by another node/factory
	*/

	[Signal]
	public delegate void onSpawnEventHandler(Node2D instance); 

	[Signal]
	public delegate void onPrespawnEventHandler(Node2D instance); 

	public void callAndSpawn<T>(T instance, Action<T> prespawn = null, Action<T> postspawn = null) where T : Node2D
	{
		if (prespawn is not null) {
			prespawn(instance); 
			EmitSignal(SignalName.onPrespawn, instance); 
		}

		spawn<T>(instance);

		if (postspawn is not null) {
			postspawn(instance); 
			EmitSignal(SignalName.onPrespawn, instance); 
		}
	}

	public void spawn<T>(T instance) where T : Node2D
	{
		if (instance is null)
		{
			GD.PrintErr("Try to spawn null at " + this.ToString());
			return; 
		}

		instance.GlobalPosition = this.GlobalPosition; 
		instance.GlobalRotation = this.GlobalRotation; 
		instance.GlobalScale    = this.GlobalScale;

		GetTree().CurrentScene.AddChild(instance); 

		EmitSignal(SignalName.onSpawn, instance); 
	}
}
