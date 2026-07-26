using Godot;
using System;

public partial class ClockHand : Control
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	private Godot.Range progressBar; 
	[Export]
	private Control clockhand;
    public override void _Ready()
    {
        clockhand.PivotOffset = 0.5f * Size; 
    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		double percentage =  progressBar.Value / progressBar.MaxValue; 
		double angle      = 2 * Math.PI * percentage; 
		clockhand.Rotation = (float)angle; 

	}
}
