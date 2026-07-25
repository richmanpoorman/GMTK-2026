using Godot;
using System;

public partial class Player : CharacterBody2D, Shooter
{

    [ExportGroup("Bullet Settings")]
    [Export] 
    public PackedScene bullet; 
    [Export]
    public float damage = 10; 
    [Export]
    public float range = 400; 


    public Bullet createProjectile()
    {
        AttackData data = new AttackData
        {
            {"damage", damage}, 
            {"range" , range }
        }; 

        Bullet shootable = bullet.Instantiate<Bullet>(); 
        shootable.init(this, data); 
        return shootable; 
    }

    public void onProjectileHit(Projectile projectile, Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal)
    {
        throw new NotImplementedException();
    }



}
