using Godot;

public abstract partial class ContactableNode : Node2D, Collidable, Collider
{
    public abstract void onCollidedWith(Collider contactingWith); 
    public abstract void onCollidingOther(Collidable contactedBy);
}

public interface Collidable // Can be touched
{
    void onCollidedWith(Collider contactingWith); 
}

public interface Collider // Can touch other things
{
    void onCollidingOther(Collidable contactedBy); 
}