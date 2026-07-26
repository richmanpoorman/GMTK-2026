global using AttackData = System.Collections.Generic.Dictionary<string, object>;
using Godot;

public abstract partial class ShooterNode : Node2D, Shooter
{
    public abstract ProjectileNode createProjectile();
    public abstract void onProjectileHit(Projectile projectile, Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal);
    public virtual void onProjectileHit(Projectile projectile, Shootable shootable, Vector2 collisionPosition) { onProjectileHit(projectile, shootable, collisionPosition, Vector2.Zero); }
    public virtual void onProjectileHit(Projectile projectile, Shootable shootable) { onProjectileHit(projectile, shootable, Vector2.Zero); }
}
public interface Shooter
{
    public void onProjectileHit(Projectile projectile, Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal);
    public void onProjectileHit(Projectile projectile, Shootable shootable, Vector2 collisionPosition) { onProjectileHit(projectile, shootable, collisionPosition, Vector2.Zero); }
    public virtual void onProjectileHit(Projectile projectile, Shootable shootable) { onProjectileHit(projectile, shootable, Vector2.Zero); }

    public ProjectileNode createProjectile();
}

public abstract partial class ProjectileNode : Node2D, Projectile { 
    public abstract void init(Shooter shooter, AttackData data);
    public abstract AttackData data();  
    public abstract Shooter shooter();
    public abstract void onCollideWith(Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal);
    public virtual void onCollideWith(Shootable shootable, Vector2 collisionPosition) { onCollideWith(shootable, collisionPosition, Vector2.Zero); }
    public virtual void onCollideWith(Shootable shootable) { onCollideWith(shootable, Vector2.Zero); }
};
public interface Projectile
{
    public void init(Shooter shooter, AttackData data);
    public Shooter shooter(); 
    public AttackData data(); 

    public void onCollideWith(Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal); 
    public void onCollideWith(Shootable shootable, Vector2 collisionPosition) { onCollideWith(shootable, collisionPosition, Vector2.Zero); }
    public virtual void onCollideWith(Shootable shootable) { onCollideWith(shootable, Vector2.Zero); }
}




public abstract partial class ShootableNode : Node2D, Shootable
{
    public abstract void onHitBy(Projectile projectile, Vector2 collisionPosition, Vector2 collisionNormal);
    public virtual void onHitBy(Projectile projectile, Vector2 collisionPosition) { onHitBy(projectile, collisionPosition, Vector2.Zero); }
    public virtual void onHitBy(Projectile projectile) { onHitBy(projectile, Vector2.Zero); }
}
public interface Shootable
{
    public void onHitBy(Projectile projectile, Vector2 collisionPosition, Vector2 collisionNormal); 
    public void onHitBy(Projectile projectile, Vector2 collisionPosition) { onHitBy(projectile, collisionPosition, Vector2.Zero); }
    public virtual void onHitBy(Projectile projectile) { onHitBy(projectile, Vector2.Zero); }
}