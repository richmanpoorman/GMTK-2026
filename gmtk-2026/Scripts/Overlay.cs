using Godot;
using System;

public partial class Overlay : Control
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public Player player;
	[Export]
	public Clock clock; 
	[Export]
	public Label timeLabel, scoreLabel; 
	public override void _Ready()
	{
		clock.setExpirable(player);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
