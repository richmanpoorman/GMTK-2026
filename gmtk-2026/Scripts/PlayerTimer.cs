using Godot;
using System;

public partial class PlayerTimer : ExpirableNode
{
	[Export]
	public double MAX_TIME, INITIAL_TIME; 
	[Export]
	public ScoringNode scorer; 
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		init(INITIAL_TIME, MAX_TIME);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		removeTime(delta);
	}

    public override void onExpire()
    {
        return; // Switch to game over
    }

    public override double addTime(double secondsAdded)
    {
		scorer.gainScore((int)secondsAdded);
        return base.addTime(secondsAdded);
    }

}
