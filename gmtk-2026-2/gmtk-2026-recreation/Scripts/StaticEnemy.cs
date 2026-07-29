using Godot;
using System;

public partial class StaticEnemy : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	private DamageCalculator damageCalculator; 

	[Export] 
	private Expirable expirable; 
	private void onHit(AttackData attack, Vector2 position, Vector2 normal)
	{
		damageCalculator.takeDamage(attack); 
		GD.Print($"Took attack {attack} at {position} with normal {normal}");
	}

	private void onTimeout()
	{
		GD.Print($"Timed out!");
		expirable.time = expirable.maxTime; 
	}
}
