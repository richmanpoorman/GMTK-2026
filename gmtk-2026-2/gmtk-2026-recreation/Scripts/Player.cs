using Godot;
using System;

public partial class Player : CharacterBody2D 
{
	[Export]
    private MovableCharacter movableCharacter; 

    [Export]
    private Gun gun;

    [Export]
    private CreateSummonable bulletMaker; 


    private void onMove(Vector2 direction) => movableCharacter.moveTowards(direction);
    
    private void onLookAtDirection(Vector2 direction) => gun.setDirection(direction); 

    private void onFire()
    {
        Node2D bullet = bulletMaker.createSummon<Node2D>(); 
        gun.fire<Node2D>(bullet);
    }

    
}
