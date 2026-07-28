using Godot;
using System;

public partial class Raycaster : Node
{
    [Export] 
    public RayCast2D ray; 

    [Export]
    private Node2D body; 

    [Signal]
    public delegate void onRayHitEventHandler(Node2D collider, Vector2 position, Vector2 normal); 

    [Signal]
    public delegate void onRayFinishCastingEventHandler(); 

    public void cast()
    {
        ray.ForceRaycastUpdate(); 

        if (!ray.IsColliding())
        {
            EmitSignal(SignalName.onRayFinishCasting); 
            return;
        }

        Node2D  collider = ray.GetCollider() as Node2D; 
        Vector2 position = ray.GetCollisionPoint(); 
        Vector2 normal   = ray.GetCollisionNormal(); 

        EmitSignal(SignalName.onRayHit, collider, position, normal);
        EmitSignal(SignalName.onRayFinishCasting); 
    }
    
    public void setGlobalTarget(Vector2 target)
    {
        Vector2 relativeVector = target - body.GlobalPosition; 
        setLocalTarget(relativeVector);
    }

    public void setLocalTarget(Vector2 target)
    {
        ray.TargetPosition = target;
    }
}
