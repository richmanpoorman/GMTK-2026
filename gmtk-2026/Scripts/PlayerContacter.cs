using Godot;
using System;

public partial class PlayerContacter : ContactableNode
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public Dash dash;


    public override void onCollidedWith(Collider contactingWith)
    {
		if (dash.isDashing()) return; // Can't be hit while invincible
        return; // Enemy does contact damage to me here...
    }

    public override void onCollidingOther(Collidable contactedBy)
    {
        return; // If I do something to them here...
    }


}
