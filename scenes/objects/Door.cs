using Godot;
using System;

public partial class Door : StaticBody2D
{
	[Export] bool open = false;
	private AnimatedSprite2D _animatedSprite2D;
	private uint closedCollisionLayer;
	private uint closedCollisionMask;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		closedCollisionLayer = GetCollisionLayer();
		closedCollisionMask = GetCollisionMask();

		if(open)
		{
			toggle(open);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Toggle door open or closed
	public void toggle(bool state)
	{
		open = state;
		// Get AnimatedSprite2D
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var frame = 0;
		if (open)
		{
			// An open door should no longer act like a wall
			SetCollisionLayer(0);
			// Likewise, it shouldn't need to check for interactions with Players or Boxes
			SetCollisionMask(0);
			frame = 1;
		}
		else
		{
			SetCollisionLayer(closedCollisionLayer);
			SetCollisionMask(closedCollisionMask);
		}

		// Set sprite
		_animatedSprite2D.SetFrame(frame);
		
	}
}
