using Godot;
using System;

public partial class EnemyContacter : ContactableNode
{

    public override void onCollidedWith(Collider contactingWith)
    {
        return; // When something touches me
    }

    public override void onCollidingOther(Collidable contactedBy)
    {
        return; // When I touch something else
    }

}
