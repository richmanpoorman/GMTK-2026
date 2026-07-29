using Godot;
using System;

public partial class SetAnimatorStart : Node
{
    [Export]
    StringName initialAnimation;

    [Export]
    private AnimationTree animator; 
    
    public override void _Ready()
    {
        animator.Set("parameters/transition/transition_request", initialAnimation);
    }

}
