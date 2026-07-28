using Godot;
using System;

[Tool]
public partial class Gun : Node2D
{
	[Export]
	private Spawnpoint spawnpoint
	{
		get => _spawnpoint; 
		set
		{
			_spawnpoint = value; 
			setInitialRadius(radius);
		}
	}

	[Export] 
	private Node2D visuals
	{
		get => _visuals; 
		set
		{
			_visuals = value; 
			setInitialRadius(radius);
		}
	}

	[Export] 
	public float radius
	{
		get => _radius; 
		set 
		{
			_radius = value; 
			setInitialRadius(_radius);
		}
	}

	private Spawnpoint _spawnpoint; 
	private Node2D _visuals; 
	private float _radius = 0.0f; 

	private void setInitialRadius(float radius)
	{
		Vector2 newPosition = radius * Vector2.Right;
		if (spawnpoint is not null) spawnpoint.Position = newPosition; 
		if (visuals is not null) visuals.Position = newPosition;
	}

	public void setDirection(Vector2 direction)
	{
		setAngle(direction.Angle());
	}

	public void setAngle(float radians)
	{
		this.Rotation = radians; 
	}

	public void fire<T>(T bullet) where T : Node2D 
	{
		spawnpoint.spawn<T>(bullet);
	}
}
