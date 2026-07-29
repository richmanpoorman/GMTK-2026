using Godot;
using System;

public partial class Expirable : Node
{
    [Export]
    private double START_TIME, MAX_TIME; 

    [Signal]
    public delegate void onExpiredEventHandler();


    public double time {get; set;}
    public double maxTime {get; set;}

    public double decreaseTime(double seconds) 
    {
        time -= seconds; 
        if (time < 0)
        {
            double overflow = -time; 
            time = 0; 
            onExpire();
            return seconds - overflow; 
        }

        return seconds; 
    }

    public double increaseTime(double seconds)
    {
        time += seconds; 
        if (time > maxTime)
        {
            double overflow = maxTime - time; 
            time = maxTime; 
            return seconds - overflow; 
        }
        return seconds; 
    }

    private void onExpire()
    {
        EmitSignal(SignalName.onExpired); 
    }
    public override void _Ready()
    {
        time    = START_TIME; 
        maxTime = MAX_TIME; 
    }

    public override void _Process(double delta)
    {
        decreaseTime(delta); 
    }

}

