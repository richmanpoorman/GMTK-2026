using Godot;
using System;

public partial class Player : CharacterBody2D 
{
	[Export]
    private MovableCharacter movableCharacter; 

    [Export]
    private Gun gun;

    [Export]
    private PlayerBulletMaker bulletMaker; 

    [Export]
    private DamageCalculator damageCalculator; 

    [Export]
    private double damage = 1f;


    private AttackData attackData; 

    private void onMove(Vector2 direction) => movableCharacter.moveTowards(direction);
    
    private void onLookAtDirection(Vector2 direction) => gun.setDirection(direction); 

    private void onFire()
    {
        Bullet bullet = bulletMaker.createBullet(attackData); 
        gun.fire<Bullet>(bullet);
    }

    private void onHit(AttackData attackData, Vector2 position, Vector2 normal) => damageCalculator.takeDamage(attackData);

    private void onTimeout()
    {
        GD.Print("I have no more time..."); 
    }

    public override void _Ready()
    {
        attackData = bulletMaker.playerAttackData(damage);
    }

}
