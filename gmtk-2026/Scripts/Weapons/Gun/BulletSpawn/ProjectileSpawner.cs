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
	
	private float[] stageLengths; 

    public override void _Ready()
    {
        bulletSpawnPoint.Position = new Vector2(SPAWNPOINT_RADIUS, 0); 
		animator = bulletSpawnPoint.GetNode<AnimationTree>("AnimationTree");
		float stage1length = animator.GetAnimation("stage_1").Length; 
		float stage2length = animator.GetAnimation("stage_2").Length; 
		float stage3length = animator.GetAnimation("stage_3").Length; 
		
		stageLengths = new float[] {stage1length, stage2length, stage3length}; 
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

	public void spawnProjectile(ProjectileNode projectile)
	{

		projectile.Position = bulletSpawnPoint.GlobalPosition; 
		projectile.Rotation = bulletSpawnPoint.GlobalRotation;
		GetTree().CurrentScene.AddChild(projectile);
	}

	public void chargeAnimation(float[] stageDurations)
	{
		for (int stage = 1; stage <= 3; stage++)
		{
			int stageIndex = stage - 1;
			string timescalePath = "parameters/stage_" + stage +"_time_scale/scale"; 
			float scaleFactor = stageDurations[stageIndex] / stageLengths[stageIndex];
			animator.Set(timescalePath, scaleFactor);
		}
		animator.Set("parameters/transition/transition_request", "charge"); 
	} 

	public void fireAnimation()
	{
		animator.Set("parameters/transition/transition_request", "fire");
		animator.Set("parameters/charge_state/transition_request", "stage_1");
	}
}
