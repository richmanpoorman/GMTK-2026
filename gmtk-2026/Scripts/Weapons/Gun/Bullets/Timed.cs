using Godot;
using System;

public partial class Timed : Node2D
{
	[Signal]
	public delegate void OnTimeoutEventHandler(Node2D timedObject); 

	[Export] 
	private double secondsLeft = 60;  
	
	public void changeTime(double changeInSeconds)
	{
		secondsLeft += changeInSeconds; 
	}

	public void setTime(double time)
	{
		secondsLeft = time; 
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		secondsLeft -= delta; 
		if (secondsLeft <= 0) EmitSignal(SignalName.OnTimeout, GetParent());
	}
}
