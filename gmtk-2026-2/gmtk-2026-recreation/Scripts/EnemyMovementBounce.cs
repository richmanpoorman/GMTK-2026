using Godot;
using System;

public partial class EnemyMovementBounce : Node
{
    
    [Export] 
    private CharacterBody2D body; 

    [Export] 
    private Vector2 direction = Vector2.One; 

    private void onCollision()
    {
        
    }
}
