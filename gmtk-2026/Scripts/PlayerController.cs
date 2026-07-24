using Godot;
using System;

public partial class PlayerController : Node2D
{
	[Signal]
	public delegate void OnPlayerChargeGunEventHandler(Player player); 

	[Signal]
	public delegate void OnPlayerFireGunEventHandler(Player player); 

	[Signal]
	public delegate void OnPlayerChargeMeleeEventHandler(Player player);

	[Signal]
	public delegate void OnPlayerSwingMeleeEventHandler(Player player);

	
	private Movable movable; 
	private Player player; 
	private ProjectileSpawner gun; 

	private void move()
	{
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down"); 

		movable.move(inputDirection);
	}

	private void checkGun()
	{
		if (Input.IsActionPressed("shoot")) 
			EmitSignal(SignalName.OnPlayerChargeGun, player);
		
		if (Input.IsActionJustReleased("shoot"))
			EmitSignal(SignalName.OnPlayerFireGun, player);
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
	}
}
