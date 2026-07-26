using Godot;
using System;

public partial class Dash : Node2D
{
	[Export]
	public CharacterBody2D body; 
	[Export]
	public PlayerController controller; 
	[Export]
	public Movable movable;
	[Export]
	private Afterimage afterimage; 
	[Export]
	public float COOLDOWN = 1f;
	[Export]
	public float COYOTE_TIME = 0.05f;

	[Export]
	public float distance = 50f;
	[Export]
	public float duration = 0.1f;

	private ulong lastTimeStep = 0; 
	private bool _isDashing = false;
	public bool isDashing()
	{
		return _isDashing; 
	}

	private void dash(Vector2 direction)
	{
		afterimage.leaveAfterImage();
		_isDashing = true; 
		float speed = distance / duration; 
		movable.changeSpeedTemporarily(speed);
		movable.move(direction);
		controller.pauseInput(); 
		GetTree().CreateTimer(duration).Timeout += endDash; 
	}

	private void endDash()
	{
		ulong currentTimeStep = Time.GetTicksMsec(); 
		_isDashing = false; 
		lastTimeStep = currentTimeStep; 
		controller.resumeInput();
		movable.revertSpeed();
	}

    public void onDash(Vector2 direction)
	{
		ulong milisecondsPassed = Time.GetTicksMsec() - lastTimeStep; 
		if (milisecondsPassed <= COOLDOWN * 1000) {
			float secondsLeft = (COOLDOWN * 1000 - milisecondsPassed) / 1000f;
			if (secondsLeft <= COYOTE_TIME) GetTree().CreateTimer(secondsLeft).Timeout += () => onDash(direction);  // Try again after the timer
			return;
		} 
		GD.Print("Dashed");
		dash(direction);
	}

}
