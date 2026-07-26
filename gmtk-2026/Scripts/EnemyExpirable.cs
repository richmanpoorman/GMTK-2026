using Godot;
using System;

public partial class EnemyExpirable : ExpirableNode
{
	[Export]
	public double MAX_TIME, INITIAL_TIME; 
	
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
        setTime(MAX_TIME);
    }
}
