using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyShootable : ShootableNode
{
	private TimeDropper timeDropper; 

	public override void _Ready()
	{
		timeDropper = GetNode<TimeDropper>("../TimeDropper");
	}

    public override void onHitBy(Projectile projectile, Vector2 collisionPosition, Vector2 collisionNormal)
    {
        AttackData data = projectile.data(); 
		float damage = (float)data.GetValueOrDefault("damage", 1); 
		double timeMultiplier = (double)data.GetValueOrDefault("time_multiplier", 1);
		ShooterNode shooter = projectile.shooter() as ShooterNode;

		timeDropper.dropTimeOrb(shooter, damage * timeMultiplier);
    }

}
