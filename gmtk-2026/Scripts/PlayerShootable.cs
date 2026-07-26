using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerShootable : ShootableNode
{
	[Export]
	public Dash dash;
	[Export]
	public TimeDropper timeDropper;

    public override void onHitBy(Projectile projectile, Vector2 collisionPosition, Vector2 collisionNormal)
    {
        if (dash.isDashing()) return; // Can't be hit while invincible

		AttackData data = projectile.data(); 
		double damage = (double)data.GetValueOrDefault("damage", 1); 
		ShooterNode shooter = projectile.shooter() as ShooterNode;

		timeDropper.dropTimeOrb(shooter, damage);
    }

}
