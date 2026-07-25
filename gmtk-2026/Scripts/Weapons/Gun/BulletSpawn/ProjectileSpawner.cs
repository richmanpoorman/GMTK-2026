using Godot;
using System;



public partial class ProjectileSpawner : Node2D
{

	[Export]
	public Node2D bulletAnchorPoint; 
	[Export]
	public Node2D bulletSpawnPoint; 

	[Export] 
	public float SPAWNPOINT_RADIUS = 50; 

	public AnimationTree animator;
	
	private Shooter shooter; 

    public override void _Ready()
    {
        bulletSpawnPoint.Position = new Vector2(SPAWNPOINT_RADIUS, 0); 
		animator = bulletSpawnPoint.GetNode<AnimationTree>("AnimationTree");
		if (GetParent<Node2D>() is Shooter _shooter) shooter = _shooter;  
		else Assert.Failed("The parent of a projectile spawner is NOT a shooter"); 
    }



	/* Make the weapon face the correct direction */ 
	public void pointAt(Vector2 globalPosition)
	{
		Vector2 direction = GlobalPosition.DirectionTo(globalPosition); 
		setAngle(direction); 
	}
	public void setAngle(Vector2 directionVector)
	{
		bulletAnchorPoint.Rotation = directionVector.Angle(); 
	}

	public Bullet spawnProjectile()
	{

		Bullet bullet = shooter.createProjectile(); 
		bullet.Position = bulletSpawnPoint.GlobalPosition; 
		bullet.Rotation = bulletSpawnPoint.GlobalRotation;
		GetTree().CurrentScene.AddChild(bullet);
		return bullet; 
	}

}
