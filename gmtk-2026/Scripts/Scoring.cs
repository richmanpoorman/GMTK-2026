using Godot;

public abstract partial class ScoringNode : Node2D, Scoring
{
    public abstract void gainScore(int points);
    public abstract void reset();
    public abstract int score();
}
public interface Scoring
{
    public int score();
    public void  gainScore(int points);
    public void reset();
}

