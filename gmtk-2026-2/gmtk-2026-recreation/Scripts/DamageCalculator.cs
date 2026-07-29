using Godot;
using System;

public partial class DamageCalculator : Node
{
    [Export]
    private Expirable expirable; 

    public void takeDamage(AttackData data)
    {
        double damage = data.damage; 
        expirable.decreaseTime(damage);
    }
}
