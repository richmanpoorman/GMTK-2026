using Godot;
using System;

public partial class PlayerScore : ScoringNode
{
	private int _score = 0; 
    public override void gainScore(int points)
    {
        _score += points; 
    }

    public override void reset()
    {
        _score = 0;
    }

    public override int score()
    {
        return _score;
    }

}
