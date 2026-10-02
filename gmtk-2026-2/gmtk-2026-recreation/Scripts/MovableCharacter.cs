using Godot;
using System;

public partial class MovableCharacter : Node
{

    public enum MovementType
    {
        Sliding, 
        Bouncing,
        Colliding
    }

    [Export]
    private CharacterBody2D body; 

    [Export]
    private float INITIAL_SPEED; 
    [Export]
    private Vector2 INITIAL_DIRECTION = Vector2.Zero;

    [Export] 
    private MovementType movementType = MovementType.Sliding; 

    [Signal]
    public delegate void onRunIntoEventHandler(KinematicCollision2D collision); 

    public float speed; 
    public Vector2 direction; 



    public void moveTowards(Vector2 direction)
    {
        this.direction = direction.LimitLength(1); 
        body.Velocity  = speed * direction; 
    }


    private void moveBodySlide(float deltaTime)
    {
        
        body.MoveAndSlide(); 
        for (int collisionIndex = 0; collisionIndex < body.GetSlideCollisionCount(); collisionIndex++)
        {
            KinematicCollision2D collision = body.GetSlideCollision(collisionIndex); 
            if (collision is not null) EmitSignal(SignalName.onRunInto, collision);
        }
    }

    private void moveBodyBounce(float deltaTime)
    {
        float   stepMagnitude = deltaTime * speed; 
        Vector2 step          = stepMagnitude * direction; 
        KinematicCollision2D collision = body.MoveAndCollide(step); 
        if (collision is null) return; 

        Vector2 bounceVelocity = body.Velocity.Bounce(collision.GetNormal());

        direction = bounceVelocity.LimitLength(1); 
        body.Velocity = bounceVelocity; 

        EmitSignal(SignalName.onRunInto, collision);
    }

    private void moveBodyCollide(float deltaTime)
    {
        float   stepMagnitude = deltaTime * speed; 
        Vector2 step          = stepMagnitude * direction; 
        KinematicCollision2D collision = body.MoveAndCollide(step); 
        if (collision is not null) EmitSignal(SignalName.onRunInto, collision);
    }

    public override void _Ready()
    {
        speed = INITIAL_SPEED; 
        direction = INITIAL_DIRECTION.LimitLength(1); 
        moveTowards(direction); 
    }
    
    public override void _PhysicsProcess(double delta)
    {
        float deltaTime = (float)delta; 

        switch(movementType)
        {
            case MovementType.Sliding: 
                moveBodySlide(deltaTime);
                break; 
            case MovementType.Bouncing: 
                moveBodyBounce(deltaTime); 
                break; 
            default: 
                moveBodyCollide(deltaTime); 
                break; 
        }

    }


}
