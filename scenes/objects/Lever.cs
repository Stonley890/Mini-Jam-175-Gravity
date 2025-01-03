using Godot;
using System;

public partial class Lever : RigidBody2D
{

	[Export] bool enabled = false;
	private AnimatedSprite2D _animatedSprite2D;

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
		var frame = 0;
		if (enabled)
		{
			frame = 1;
		}
		_animatedSprite2D.SetFrame(frame);
		
	}
}
