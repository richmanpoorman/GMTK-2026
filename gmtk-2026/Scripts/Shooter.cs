global using AttackData = System.Collections.Generic.Dictionary<string, object>;
using Godot;


public interface Shooter
{
    public void onProjectileHit(Projectile projectile, Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal);
    public void onProjectileHit(Projectile projectile, Shootable shootable, Vector2 collisionPosition) { onProjectileHit(projectile, shootable, collisionPosition, Vector2.Zero); }
    public void onProjectileHit(Projectile projectile, Shootable shootable) { onProjectileHit(projectile, shootable, Vector2.Zero); }

    public Bullet createProjectile();
}

public interface Projectile
{
    public void init(Shooter shooter, AttackData data);
    public Shooter shooter(); 
    public AttackData data(); 

    public void onCollideWith(Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal); 
    public void onCollideWith(Shootable shootable, Vector2 collisionPosition) { onCollideWith(shootable, collisionPosition, Vector2.Zero); }
    public void onCollideWith(Shootable shootable) { onCollideWith(shootable, Vector2.Zero); }
}


public abstract partial class Bullet : Node2D, Projectile { 
    public abstract AttackData data();  
    public abstract void init(Shooter shooter, AttackData data);
    public abstract void onCollideWith(Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal);
    public abstract Shooter shooter();
};

public interface Shootable
{
    public void onHitBy(Projectile projectile, Vector2 collisionPosition, Vector2 collisionNormal); 
    public void onHitBy(Projectile projectile, Vector2 collisionPosition) { onHitBy(projectile, collisionPosition, Vector2.Zero); }
    public void onHitBy(Projectile projectile) { onHitBy(projectile, Vector2.Zero); }
}