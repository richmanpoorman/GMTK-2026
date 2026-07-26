using Godot;
using System;

public partial class Player : CharacterBody2D, Shooter, Expirable, Collidable, Collider, Scoring
{

    
    [Export]
    public ExpirableNode timer;

    [Export]
    public ShooterNode shooter; 

    [Export]
    public ContactableNode contacter; 

    [Export]
    public ScoringNode scorer; 


    public double addTime(double secondsAdded)
    {
        return ((Expirable)timer).addTime(secondsAdded);
    }

    public void onExpire()
    {
        ((Expirable)timer).onExpire();
    }


    public double removeTime(double secondsRemoved)
    {
        return ((Expirable)timer).removeTime(secondsRemoved);
    }

    public double secondsLeft()
    {
        return ((Expirable)timer).secondsLeft();
    }

    public double setTime(double seconds)
    {
        return ((Expirable)timer).setTime(seconds);
    }

    public double updateMaxTime(double seconds)
    {
        return ((Expirable)timer).updateMaxTime(seconds);
    }

    public double maxTime()
    {
        return ((Expirable)timer).maxTime();
    }

    public void onProjectileHit(Projectile projectile, Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal)
    {
        ((Shooter)shooter).onProjectileHit(projectile, shootable, collisionPosition, collisionNormal);
    }

    public ProjectileNode createProjectile()
    {
        return ((Shooter)shooter).createProjectile();
    }

    public void onCollidedWith(Collider contactingWith)
    {
        ((Collidable)contacter).onCollidedWith(contactingWith);
    }

    public void onCollidingOther(Collidable contactedBy)
    {
        ((Collider)contacter).onCollidingOther(contactedBy);
    }

    public int score()
    {
        return ((Scoring)scorer).score();
    }

    public void gainScore(int points)
    {
        ((Scoring)scorer).gainScore(points);
    }

    public void reset()
    {
        ((Scoring)scorer).reset();
    }

}
