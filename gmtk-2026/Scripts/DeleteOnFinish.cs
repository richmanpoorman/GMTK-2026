using Godot;
using System;

public partial class DeleteOnFinish : GpuParticles2D
{
	public void onFinish()
	{
		QueueFree(); 
	}
}
