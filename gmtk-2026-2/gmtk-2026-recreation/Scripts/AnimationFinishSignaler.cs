using Godot;
using System;

public partial class AnimationFinishSignaler : Node
{
    
    [Export]
    public StringName animation; 

    [Export]
    private AnimationTree animationTree; 

    [Signal]
    public delegate void onAnimationFinishedEventHandler();

    private AnimationPlayer animationPlayer; 

    private void signalAnimationFinish(StringName animationName)
    {
        GD.Print($"Animation finished: {animationName}");
        if (animationName == animation) EmitSignal(SignalName.onAnimationFinished);
    }

    public override void _Ready()
    {
        animationPlayer = animationTree.GetNode<AnimationPlayer>(animationTree.AnimPlayer);
        animationPlayer.AnimationFinished += signalAnimationFinish; 
    }

    public override void _ExitTree()
    {
        animationPlayer.AnimationFinished -= signalAnimationFinish; 
    }


}
