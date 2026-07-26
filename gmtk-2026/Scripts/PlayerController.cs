using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerController : Node2D
{
	[Signal]
	public delegate void OnPlayerChargeGunEventHandler(); 

	[Signal]
	public delegate void OnPlayerFireGunEventHandler(); 

	[Signal]
	public delegate void OnPlayerChargeMeleeEventHandler();

	[Signal]
	public delegate void OnPlayerSwingMeleeEventHandler();

	
	[Signal]
	public delegate void OnPlayerDashEventHandler(Vector2 direction); 

	[Signal]
	public delegate void OnPlayerMoveEventHandler(Vector2 direction); 

	private Movable movable; 
	private Player player; 
	private ProjectileSpawner gun; 
	private bool canInput = true; 

	private record DelayedEvent(StringName signal, Variant[] args);
	private Queue<DelayedEvent> eventQueue = new Queue<DelayedEvent>(); 

	public void pauseInput()
	{
		canInput = false;
	}
	
	public void resumeInput()
	{
		canInput = true; 
	}

	private void addToEventQueue(StringName signal, params Variant[] args)
	{
		DelayedEvent delayedEvent = new DelayedEvent(signal, args);
		eventQueue.Enqueue(delayedEvent);
	}

	private void executeEventQueue()
	{
		while (eventQueue.Count > 0)
		{
			DelayedEvent delayedEvent = eventQueue.Dequeue(); 
			EmitSignal(delayedEvent.signal, delayedEvent.args);
		}
	}

	private void move()
	{
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down"); 
		addToEventQueue(SignalName.OnPlayerMove, inputDirection);
		// movable.move(inputDirection);
	}

	private void checkGun()
	{
		if (Input.IsActionJustPressed("shoot")) 
			addToEventQueue(SignalName.OnPlayerChargeGun);
		
		if (Input.IsActionJustReleased("shoot"))
			addToEventQueue(SignalName.OnPlayerFireGun);
	}

	private void checkDash()
	{
		if (!Input.IsActionJustPressed("dash")) return; 

		Vector2 inputDirection  = Input.GetVector("left", "right", "up", "down"); 
		if (inputDirection == Vector2.Zero) return; // No dash if you aren't moving
		Vector2 direction       = inputDirection.Normalized();

		addToEventQueue(SignalName.OnPlayerDash, direction);
	}

	private void aimGun()
	{
		gun.pointAt(GetGlobalMousePosition());
	}

    public override void _Ready()
    {
		player  = GetParent<Player>();
        movable = GetNode<Movable>("../Movable");
		gun     = GetNode<ProjectileSpawner>("../ProjectileSpawner");
    }

	

	public override void _Process(double delta)
	{
		move(); 
		
		checkGun(); 
		aimGun(); 
		checkDash();
		
		if (canInput) executeEventQueue();
	}
}
