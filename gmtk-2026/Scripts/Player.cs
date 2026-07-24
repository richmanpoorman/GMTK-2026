using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public string[] canCollide = {"enemy"};

    [Export]
    public BulletInitializationData bulletValues;  

    private Collidable collidable; 
    public override void _Ready()
    {
        collidable = GetNode<Collidable>("Collidable");
        collidable.setCollideWith(canCollide);
    }

}
