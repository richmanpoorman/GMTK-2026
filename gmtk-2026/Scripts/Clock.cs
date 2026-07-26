using Godot;
using System;

public partial class Clock : Control
{
	
	public Expirable expirable; 
	[Export]
	private TextureProgressBar clock;

	public void setExpirable(Expirable expirable)
	{
		this.expirable = expirable;
		clock.MaxValue = expirable.maxTime();
		clock.Value    = expirable.secondsLeft();
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		clock.Value = expirable.secondsLeft();
	}
}
