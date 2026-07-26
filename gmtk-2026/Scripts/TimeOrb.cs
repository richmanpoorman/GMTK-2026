using Godot;
using System;

public partial class TimeOrb : CharacterBody2D, Collidable, Collider
{

	[Export]
	public Movable movable; 
	[Export]
	public Curve sizeFunction; 
	[Export]
	public float maxSizeRange; 

	private double time; 
	private Node2D target; 
	public void init(Node2D target, double time)
	{
		this.time   = time; 
		this.target = target; 

		float scaleFactor = time > maxSizeRange ? sizeFunction.MaxValue : sizeFunction.Sample((float)time / maxSizeRange);
		this.Scale = new Vector2(scaleFactor, scaleFactor);
		
	}

    public void onCollidedWith(Collider contactingWith)
    {
        return;  
    }

    public void onCollidingOther(Collidable contactedBy)
    {
        if (contactedBy is not Expirable) return; 
		Expirable expirable = contactedBy as Expirable; 

		expirable.addTime(time);

		QueueFree(); 
    }

	
	private void setMovementDirection()
	{
		if (target is null) {
			movable.move(Vector2.Zero);
			return; 
		}

		Vector2 direction = (target.GlobalPosition - this.GlobalPosition).Normalized(); 
		movable.move(direction);
	}

    public override void _PhysicsProcess(double delta)
    {
        setMovementDirection(); 
    }

}
