using Godot;
using System;

public partial class BulletInitializationData : RefCounted
{
    public Node2D owner; 
    public string[] canHit; 
    public float duration; 
    public float damage; 
    public float speed; 
    public float size; 
}
public partial class BulletSpawnData : RefCounted
{
	public Vector2 position; 
	public float rotation;
}
public partial class Bullet : CharacterBody2D
{
    private Collidable collidable; 
    private Timed timed; 

    private Node2D owner; 
    private float duration = 0.1f;
    private string[] canHit; 
    private float speed; 
    private float damage; 
    private float size; 
    public void init(BulletInitializationData data)
    {
        owner    = data.owner;
        duration = data.duration;
        canHit   = data.canHit; 
        speed    = data.speed; 
        canHit   = data.canHit; 
        damage   = data.damage; 
        size     = data.size; 
    }
    public void setSpawnData(BulletSpawnData data)
    {
        this.Position = data.position; 
        this.Rotation = data.rotation; 
        this.Velocity = new Vector2(speed, 0).Rotated(data.rotation);
        this.Scale = new Vector2(size, size); 
    }
    public override void _Ready()
    {
        collidable = GetNode<Collidable>("Collidable");
        timed      = GetNode<Timed>("Timed");
        timed.setTime(duration);
        collidable.setCollideWith(canHit);
    }

    public override void _PhysicsProcess(double deltaSeconds)
    {
        Vector2 moveStep = this.Velocity * (float)deltaSeconds; 

        KinematicCollision2D collision = MoveAndCollide(moveStep); 

        if (collision == null) return; 
        collidable.collision(collision); 

    }

}
