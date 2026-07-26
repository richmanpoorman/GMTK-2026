using Godot;
using System;

public partial class ParticleDeleteOnFinish : GpuParticles2D
{

    public override void _Ready()
    {
        Emitting = true; 
    }

	public void onFinish()
	{
		QueueFree(); 
	}
}
