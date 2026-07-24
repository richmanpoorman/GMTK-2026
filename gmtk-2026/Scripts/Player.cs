using Godot;
using System;

public partial class Player : CharacterBody2D
{

    [ExportGroup("Player Settings")]
    [Export]
    public string[] canCollide = {"enemy"};

    [ExportGroup("Bullet Settings")]
    [Export]
    public string[] canHit = {"enemy"}; 
    [Export]
    public float duration = 1f; 
    [Export]
    public float damage = 10; 
    [Export]
    public float speed = 400; 
    [Export]
    public float size = 1; 

    public BulletInitializationData bulletValues;  

    private Collidable collidable; 
    public override void _Ready()
    {
        collidable = GetNode<Collidable>("Collidable");
        collidable.setCollideWith(canCollide);
        bulletValues = new BulletInitializationData
        {
            canHit = canHit, damage = damage, duration = duration, owner = this, size = size, speed = speed 
        }; 
    }

}
