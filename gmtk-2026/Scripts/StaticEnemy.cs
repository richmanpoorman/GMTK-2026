using Godot;
using System;

public partial class StaticEnemy : StaticBody2D, Shootable, Expirable, Collidable, Collider
{
	[Export]
	public ShootableNode shootable; 

	[Export]
	public ExpirableNode timer; 

	[Export]
	public ContactableNode contacter;

    public double addTime(double secondsAdded)
    {
        return timer.addTime(secondsAdded);
    }

    public double maxTime()
    {
        return timer.maxTime();
    }

    public void onCollidedWith(Collider contactingWith)
    {
        ((Collidable)contacter).onCollidedWith(contactingWith);
    }

    public void onCollidingOther(Collidable contactedBy)
    {
        ((Collider)contacter).onCollidingOther(contactedBy);
    }

    public void onExpire()
    {
        timer.onExpire();
    }


    public void onHitBy(Projectile projectile, Vector2 collisionPosition, Vector2 collisionNormal)
    {
        shootable.onHitBy(projectile, collisionPosition, collisionNormal);
    }

    public double removeTime(double secondsRemoved)
    {
        return timer.removeTime(secondsRemoved);
    }

    public double secondsLeft()
    {
        return timer.secondsLeft();
    }

    public double setTime(double seconds)
    {
        return timer.setTime(seconds);
    }

    public double updateMaxTime(double seconds)
    {
        return timer.updateMaxTime(seconds);
    }
}
