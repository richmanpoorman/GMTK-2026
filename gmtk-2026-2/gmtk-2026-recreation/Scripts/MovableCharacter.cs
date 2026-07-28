using Godot;
using System;

public partial class MovableCharacter : Node
{

    [Export]
    private CharacterBody2D body; 

    [Export]
    private float INITIAL_SPEED; 

    [Signal]
    public delegate void onRunIntoEventHandler(KinematicCollision2D collision); 

    public float speed; 
    public Vector2 direction; 



    public void moveTowards(Vector2 direction)
    {
        this.direction = direction; 
    }


    private void moveBody(float deltaTime)
    {
        float   stepMagnitude = deltaTime * speed; 
        Vector2 step          = stepMagnitude * direction; 
        KinematicCollision2D collision = body.MoveAndCollide(step); 
        
        if (collision is not null) EmitSignal(SignalName.onRunInto, collision);
    }
        public override void _Ready()
    {
        speed = INITIAL_SPEED; 
        direction = Vector2.Zero; 
    }
    
    public override void _PhysicsProcess(double delta)
    {
        moveBody((float)delta); 
    }


}
