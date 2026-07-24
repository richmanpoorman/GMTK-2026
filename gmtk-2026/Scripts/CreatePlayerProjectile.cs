using Godot;
using System;

public partial class CreatePlayerProjectile : Node2D
{
	
	[Export]
	public PackedScene bulletPrefab; 
	
	[Export]
	public float maxChargeTime = 3f;
	
	private ProjectileSpawner spawner;

	private Player player; 
	private float chargeAnimationLength = 1.8f; 
    public override void _Ready()
    {
        spawner = GetNode<ProjectileSpawner>("../ProjectileSpawner"); 
		player  = GetParent<Player>(); 

    }

	public void onCharge(Node2D owner)
	{
		spawner.animator.Set("parameters/charge_time_scale/scale", chargeAnimationLength / maxChargeTime);
		spawner.animator.Set("parameters/transition/transition_request", "charge"); 
	}

	// TODO:: MAKE IT SO PLAYER CAN RELEASE ATTACK
	public void onFire(Node2D owner)
	{
		spawner.animator.Set("parameters/transition/transition_request", "fire");
		Bullet bullet = bulletPrefab.Instantiate<Bullet>(); 
		bullet.init(player.bulletValues); 
		spawner.spawnProjectile(bullet);
	}
}
