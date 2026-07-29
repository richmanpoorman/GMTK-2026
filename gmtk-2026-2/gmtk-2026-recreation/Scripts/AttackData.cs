using Godot;

public partial class AttackData : RefCounted
{
    public double damage = 1; 
    public Node   attacker = null; 

    public AttackData() { }
    public AttackData setDamage(double damage)
    {
        this.damage = damage; 
        return this; 
    }

    public AttackData setSource<T>(T attacker) where T : Node
    {
        this.attacker = attacker; 
        return this; 
    }

    public AttackData setSource(Node node) => this.setSource<Node>(node); 
}