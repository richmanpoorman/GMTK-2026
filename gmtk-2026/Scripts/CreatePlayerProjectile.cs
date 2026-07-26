using Godot;
using System;
using System.Collections.Generic;

public partial class CreatePlayerProjectile : ShooterNode
{
		
	
	[Export] 
    public PackedScene bullet; 
    [Export]
    public float DAMAGE = 10; 
    [Export]
    public float RANGE = 400;
	[Export]
	public double[] TIME_GAIN_MULTIPLIERS = {1.5, 1.5, 1.5, 1.5};

	private ProjectileSpawner spawner;

	private Player player; 

	private AnimationTree animator; 
	
	[Export]
	public float[] STAGE_TIMES = {0.6f, 0.6f, 0.6f};
	
	[Export]
	public float[] STAGE_DAMAGE_MULTIPLIERS = {1.0f, 1.5f, 2f, 4f}; 
	[Export]
	public float[] STAGE_RANGE_MULTIPLIERS = {.5f, .75f, 1f, 2f}; 


	[Export]
	public float[] WIDTH_MULTIPLIERS = {1f, 1.5f, 2f, 4f};

	[Export]
	public float SHOOT_COST = 1f;

	private float[] stageLengths; 

	private ulong chargeTimeStartMsec; 
	private enum ChargeStage
	{
		STAGE_1 = 0, 
		STAGE_2 = 1, 
		STAGE_3 = 2, 
		FULLY_CHARGED = 3  
	}
	
	private ChargeStage currentStage = ChargeStage.STAGE_1;  

    public override void _Ready()
    {
        spawner = GetNode<ProjectileSpawner>("../ProjectileSpawner"); 
		player  = GetParent<Player>(); 
		
    }

	public void onCharge()
	{
		
		spawner.chargeAnimation(STAGE_TIMES);
		chargeTimeStartMsec = Time.GetTicksMsec(); 
	}

	// TODO:: MAKE IT SO PLAYER CAN RELEASE ATTACK
	public void onFire()
	{
		spawner.fireAnimation(); 

		ulong chargeTimeEndMsec = Time.GetTicksMsec(); 
		currentStage = determineStage(chargeTimeStartMsec, chargeTimeEndMsec); 
		ProjectileNode projectile = createProjectile(); 
		spawner.spawnProjectile(projectile);
	}

    public override ProjectileNode createProjectile()
    {
		float dmgMultiplier = damageMultiplier(currentStage);
		float distanceMultiplier = rangeMultiplier(currentStage);
		double timeRewardFactor = timeGainMultipler(currentStage);
		float  widthMultipler  = projectileWidthMultipler(currentStage);
        AttackData data = new AttackData
        {
            {"damage", DAMAGE * dmgMultiplier}, 
            {"range" , RANGE * distanceMultiplier}, 
			{"time_multiplier", timeRewardFactor},
			{"width_scale", widthMultipler}
        }; 

		// It takes time to fire a bullet
		player.removeTime(SHOOT_COST);
        
		ProjectileNode shootable = bullet.Instantiate<ProjectileNode>(); 
        shootable.init(this, data); 
        return shootable; 
    }

    public override void onProjectileHit(Projectile projectile, Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal)
    {
        // if (!(shootable is Enemy)) return; 
		// Enemy enemy = shootable as Enemy; 
		// AttackData data = projectile.data(); 
		
		// double damage         = (float)data.GetValueOrDefault("damage", DAMAGE); 
		// double gainMultiplier = (float)data.GetValueOrDefault("time_multiplier", 1); 
		// double timeGained     = enemy.removeTime(damage) * gainMultiplier; 
		// player.addTime(timeGained);
    }

	private ChargeStage determineStage(ulong startMsec, ulong endMsec)
	{
		float timePassed  = (endMsec - startMsec) / 1000f; 
		ChargeStage stage = ChargeStage.STAGE_1; 
		float stageTimes = 0; 

		for (int stageIndex = 0; stageIndex < 3; stageIndex++)
		{
			stageTimes += STAGE_TIMES[stageIndex]; 
			if (timePassed < stageTimes) return stage; 
			stage = (ChargeStage)(stageIndex + 1); 
		}
		return stage; 
	}

	private float damageMultiplier(ChargeStage stage)
	{
		return STAGE_DAMAGE_MULTIPLIERS[(int)stage];
	} 
	
	private float rangeMultiplier(ChargeStage stage)
	{
		return STAGE_RANGE_MULTIPLIERS[(int)stage];
	} 
	private double timeGainMultipler(ChargeStage stage)
	{
		return TIME_GAIN_MULTIPLIERS[(int)stage];
	}

	private float projectileWidthMultipler(ChargeStage stage)
	{
		return WIDTH_MULTIPLIERS[(int)stage];
	}
}
