using Godot;
using System;



public partial class ProjectileSpawner : Node2D
{

	[Export]
	public Node2D bulletAnchorPoint; 
	[Export]
	public Node2D bulletSpawnPoint; 

	[Export] 
	public float spawnpointRadius = 50; 

	public AnimationTree animator;

    public override void _Ready()
    {
        bulletSpawnPoint.Position = new Vector2(spawnpointRadius, 0); 
		animator = bulletSpawnPoint.GetNode<AnimationTree>("AnimationTree");
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

	public void spawnProjectile(Bullet bullet)
	{
		bullet.setSpawnData(new BulletSpawnData {
			position = bulletSpawnPoint.GlobalPosition, 
			rotation = bulletAnchorPoint.Rotation
		});
		GetTree().CurrentScene.AddChild(bullet);
	}

}
