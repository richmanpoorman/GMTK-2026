using Godot;
using System;
using System.Collections.Generic;

public partial class RaycastBullet : Bullet
{
	[Export]
	public RayCast2D raycast; 

	[Export]
	public Line2D innerLine, outerLine; 

	[Export]
	public AnimationTree animator; 

	[Export]
	public PackedScene hitParticlePrefab;

	[Export]
	public float DEFAULT_DISTANCE = 100f;

	private Shooter _shooter; 
	private AttackData _data;
	public void setDistance(float distance)
	{
		raycast.TargetPosition = Vector2.Right * distance; 
	}

    public override void _Ready()
    {
        onFire();
    }

	public void onAnimationFinish(string animationName)
	{
		if (animationName == "fire") QueueFree();
	}
	
    public override void init(Shooter shooter, AttackData data)
    {
        _shooter = shooter; 
		_data    = data; 
		float distance = (float)data.GetValueOrDefault("range", DEFAULT_DISTANCE); 
		setDistance(distance);
    }

    public override Shooter shooter()
    {
        return _shooter; 
    }

    public override AttackData data()
    {
        return _data; 
    }

    public override void onCollideWith(Shootable shootable, Vector2 collisionPosition, Vector2 collisionNormal)
    {
        hitAnimation(collisionPosition, collisionNormal);
    }

	public void onFire()
	{
		// Set the lines to the max length if not collding
		if (!raycast.IsColliding()) {
			Vector2 target = raycast.TargetPosition; 
			setTarget(target);
			fireAnimation();
			return; 
		} 

		Vector2 position  = raycast.GetCollisionPoint(); 
		Vector2 normal    = raycast.GetCollisionNormal(); 
		Node2D  collision = raycast.GetCollider() as Node2D;

		if (collision is Shootable shootable) {
			_shooter.onProjectileHit(this, shootable, position, normal);
			shootable.onHitBy(this); 
			onCollideWith(shootable, position, normal);
		}

		// Visual animation
		setTarget(position - this.GlobalPosition);
		fireAnimation();
		hitAnimation(position, normal);
	}

	private void setTarget(Vector2 target)
	{
		innerLine.SetPointPosition(1, target);
		outerLine.SetPointPosition(1, target);
	}
	private void fireAnimation()
	{
		animator.Set("parameters/transition/transition_request", "fire");
	}

	private void hitAnimation(Vector2 position, Vector2 direction)
	{
		Node2D particle = hitParticlePrefab.Instantiate<Node2D>(); 
		particle.Rotation = direction.Angle(); 
		particle.Position = position; 

		GetTree().CurrentScene.AddChild(particle);
	}


    
}
