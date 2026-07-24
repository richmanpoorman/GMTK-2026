using Godot;
using System;

public partial class CreatePlayerProjectile : Node2D
{
	
	[Export]
	public PackedScene bulletPrefab; 
	
	
	private ProjectileSpawner spawner;

	private Player player; 
    public override void _Ready()
    {
        spawner = GetNode<ProjectileSpawner>("../ProjectileSpawner"); 
		player  = GetParent<Player>(); 
    }


	// TODO:: MAKE IT SO PLAYER CAN RELEASE ATTACK
	public void onFire(Node2D owner)
	{
		Bullet bullet = bulletPrefab.Instantiate<Bullet>(); 
		bullet.init(player.bulletValues); 
		spawner.spawnProjectile(bullet);
	}
}
