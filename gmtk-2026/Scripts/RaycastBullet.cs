using Godot;
using System;
using System.Collections.Generic;

public partial class RaycastBullet : ProjectileNode
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
	public PackedScene missParticlePrefab;

	[Export]
	public float DEFAULT_DISTANCE = 100f;

	private Shooter _shooter; 
	private AttackData _data;
	public void setDistance(float distance)
	{
		raycast.TargetPosition = Vector2.Right * distance; 
	}

	private float width; 

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
		
		width = (float)data.GetValueOrDefault("width_scale", 1f); 
		scaleProjectileWidth(width);

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
		raycast.ForceRaycastUpdate(); // Needed, since fires outside of physics loop

		// Set the lines to the max length if not collding
		if (!raycast.IsColliding()) {
			Vector2 target = raycast.TargetPosition; 
			setTarget(target);
			fireAnimation();
			missAnimation(target);
			GD.Print("Miss"); 
			return; 
		} 

		Vector2 position  = raycast.GetCollisionPoint(); 
		Vector2 normal    = raycast.GetCollisionNormal(); 
		Node2D  collision = raycast.GetCollider() as Node2D;

		GD.Print("Hit at: " + position);

		if (collision is Shootable shootable) {
			_shooter.onProjectileHit(this, shootable, position, normal);
			shootable.onHitBy(this); 
			onCollideWith(shootable, position, normal);
		}

		Vector2 hitTarget = (position - this.GlobalPosition); 
		// Visual animation
		setTarget(Vector2.Right * hitTarget.Length());
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
		particle.Scale    = new Vector2(width, width);
		GetTree().CurrentScene.AddChild(particle);
	}

	private void missAnimation(Vector2 relativeTarget)
	{
		return; 
		Node2D particle = missParticlePrefab.Instantiate<Node2D>(); 
		particle.Position = relativeTarget;
		particle.Scale    = new Vector2(width, width);
		AddChild(particle); 
	}

	private void scaleProjectileWidth(float width)
	{
		Vector2 scale   = new Vector2(1, width);
		innerLine.Scale = scale; 
		outerLine.Scale = scale;
	}

    
}
