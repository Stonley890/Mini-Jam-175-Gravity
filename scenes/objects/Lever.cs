using Godot;
using System;

public partial class Lever : RigidBody2D
{

	[Export] boolean enabled = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Toggle lever on and off
	public void toggle(bool state)
	{
		enabled = state;
		// Get AnimatedSprite2D
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (enabled)
		{
			_animatedSprite2D.frame = 1;
		}
		else
		{
			_animatedSprite2D.frame = 0;
		}
	}
}
