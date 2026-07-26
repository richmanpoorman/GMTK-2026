using Godot;
using System;

public partial class TimeDropper : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	PackedScene timeOrbPrefab; 
	[Export]
	public float distance = 60f; 
	[Export]
	public Curve divergenceSampler;
	[Export]
	public float maxDivergence = 10f; 
	[Export]
	public Curve curlSampler; 
	[Export]
	public float maxCurl = 10f;
	
	private Random random = new Random(); 
	private Expirable expirable;

    public override void _Ready()
    {
        expirable = GetParent<Expirable>();
    }

	public void dropTimeOrb(Node2D target, double time)
	{
		double timeDropped = expirable.removeTime(time);
		float randomAngle = random.NextSingle() * 2 * (float)Math.PI; 
		Vector2 unitDirection = Vector2.Right.Rotated(randomAngle);
		Vector2 offset = unitDirection * distance;

		TimeOrb timeOrb = timeOrbPrefab.Instantiate<TimeOrb>(); 
		timeOrb.init(target, timeDropped);
		timeOrb.Position = this.GlobalPosition + offset; 

		float repulsion = divergenceSampler.Sample(random.NextSingle()) * maxDivergence;
		int direction = random.Next(1) == 0 ? -1 : 1;  
		float curvature = curlSampler.Sample(random.NextSingle()) * maxCurl * direction; 
		
		timeOrb.Velocity = unitDirection * repulsion;
		timeOrb.Velocity = unitDirection.Orthogonal() * curvature;

		GetTree().CurrentScene.AddChild(timeOrb);
	}
}
