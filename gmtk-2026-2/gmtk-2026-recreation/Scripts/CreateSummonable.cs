using Godot;
using System;

// TODO:: Change this into some sort of factory pattern that is a bit more flexible
public partial class CreateSummonable : Node
{
    [Export]
    PackedScene prefab; 

    
    public T createSummon<T>(params Variant[] args) where T : Node2D // , Summonable // <- ADD THIS FOR THE FUTURE
    {
        T node = prefab.Instantiate<T>(); 
        // node.init(args);
        return node; 
    }
}
