using Godot;
using System;

// TODO:: Change this into some sort of factory pattern that is a bit more flexible
public partial class PlayerBulletMaker : Node
{
    [Export]
    PackedScene bullet; 
    

    public AttackData playerAttackData(double damage)
    {
        return new AttackData()
            .setSource(GetParent())
            .setDamage(damage); 
    }

    public Bullet createBullet(AttackData data) // , Summonable // <- ADD THIS FOR THE FUTURE
    {
        Bullet node = bullet.Instantiate<Bullet>(); 
        node.setAttackData(data);  
        // node.init(args);
        return node; 
    }
}
