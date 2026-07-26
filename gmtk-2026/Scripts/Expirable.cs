using Godot;

public abstract partial class ExpirableNode : Node2D, Expirable
{

    public abstract void onExpire();
    private double _maxTime, _time; 
    public void init(double startTime, double maxTime)
    {
        updateMaxTime(maxTime); 
        setTime(startTime);
    }

    public virtual double maxTime()
    {
        return _maxTime; 
    }

    public virtual double secondsLeft()
    {
        return _time; 
    }

    public virtual double removeTime(double secondsRemoved)
    {
        double secondsLeft = _time; 
        _time -= secondsRemoved;
        if (secondsLeft > secondsRemoved)
        {
            onExpire(); 
            return secondsLeft; 
        }
		return secondsRemoved; 
    }

    public virtual double addTime(double secondsAdded)
    {
		_time += secondsAdded; 
        if (_maxTime < 0)
		{
			return secondsAdded; 
		}

		if (_time > _maxTime)
		{
			double difference = _time - _maxTime; 
			_time = _maxTime; 
			return secondsAdded - difference; 
		}
		
		return secondsAdded;
    }

    public virtual double setTime(double seconds)
    {
        if (_maxTime < 0)
		{
			_time = seconds; 
			return seconds; 
		}

        if (seconds > _maxTime)
        {
            _time = _maxTime; 
            return _maxTime; 
        }

		_time = seconds; 
		return seconds; 
    }

    public virtual double updateMaxTime(double seconds)
    {
        _maxTime = seconds; 
		return seconds; 
    }
}
public interface Expirable
{
    public void init(double startTime, double maxTime)
    {
        updateMaxTime(maxTime);
        setTime(startTime);
    }

    public double setTime(double seconds); 
    public double updateMaxTime(double seconds); 
    public double maxTime(); // less than 0 if there is no max time
    public double secondsLeft(); 

    public double removeTime(double secondsRemoved); // returns the amount of time actually removed, as opposed to the theoretical amount of time removed

    public double addTime(double secondsAdded); // Returns the amount of time actually added, as opposed to the theoretical amount of time added

    public void onExpire(); // Callback for what happens when the timer runs out
}