using Godot;
using System;

public partial class Destructable : Node2D
{
	[Signal]
	public delegate void OnDestroyedEventHandler(Node2D destroyedObject);

	public void signalDestroyed(Node2D destroyedObject)
	{
		EmitSignal(SignalName.OnDestroyed, destroyedObject);
	} 
}
