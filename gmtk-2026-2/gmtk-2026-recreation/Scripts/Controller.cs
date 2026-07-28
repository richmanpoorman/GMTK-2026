using Godot;
using System;

public partial class Controller : Node
{
    /*
        Purpose: Reads the I/O from the user and turns it into commands that the rest of the program can understand
        - Note: It is also responsible for things like coyote time and late input
    */

    [Export]
    private Node2D body; 

    [Signal]
    public delegate void onMoveEventHandler(Vector2 input);

    [Signal]
    public delegate void onFireStartEventHandler(); 

    [Signal]
    public delegate void onFireReleaseEventHandler(); 

    [Signal]
    public delegate void onFaceDirectionEventHandler(Vector2 direction); 

    // TODO: ADD DASHING AND MELEE ATTACKS (ONLY NEED FIRING FOR NOW)

    bool isFacingRight = true; 
    private void handleDirection()
    {
        Vector2 input = Input.GetVector("left", "right", "up", "down"); 
        EmitSignal(SignalName.onMove, input);
    }
    private void handleMouse()
    {
        Vector2 unnormalizedDirection = body.GetGlobalMousePosition() - body.GlobalPosition; 
        Vector2 direction = unnormalizedDirection.Normalized();
        EmitSignal(SignalName.onFaceDirection, direction);
    }

    private void handleFiring()
    {
        if (Input.IsActionJustPressed("fire")) EmitSignal(SignalName.onFireStart);
        if (Input.IsActionJustReleased("fire")) EmitSignal(SignalName.onFireRelease);
    }



    public override void _Process(double delta)
    {
        handleDirection(); 
        handleMouse();
        handleFiring();

    }


}
