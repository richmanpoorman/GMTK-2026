using Godot;
using System;

public partial class Afterimage : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public float INITIAL_TRANSPARENCY = 0.5f;

	[Export]
	public float DURATION = 0.1f;
	[Export]
	private Node2D visualsRoot; 
	public void setVisualNode(Node2D visuals)
	{
		visualsRoot = visuals;
	}
	public void leaveAfterImage()
	{
		Node2D afterimage = visualsRoot.Duplicate() as Node2D; 
		afterimage.GlobalPosition = this.GlobalPosition;
		
		GetTree().CurrentScene.AddChild(afterimage);

		Tween tween = CreateTween(); 
		Callable freeAfterFinish = Callable.From(afterimage.QueueFree); 

		tween.TweenProperty(afterimage, "modulate:a", 0f, DURATION);
		tween.TweenCallback(freeAfterFinish);

	}
}
