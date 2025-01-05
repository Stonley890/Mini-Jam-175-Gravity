using Godot;
using System;

public partial class Door : StaticBody2D
{
	[Export] bool open;
	private AnimatedSprite2D _animatedSprite2D;
	private uint closedCollisionLayer;
	private uint closedCollisionMask;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		closedCollisionLayer = GetCollisionLayer();
		closedCollisionMask = GetCollisionMask();

		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(open)
		{
			toggle(open);
		}
		else toggle(open);
		GetLever();
		
	}

	public void GetLever()
	{
		 Godot.Collections.Array<Godot.Node> nodes = GetParent().GetChildren();

    		// Loop through all the levers
            for(int i = 0; i < nodes.Count; i++)
            {
                // Get the lever
                Node node = nodes[i];

                // Check if it is a lever scene
                if(node is Lever lever)
                {
                    if(lever.GetEnabled() == true)
					{
						open = true;
					}
					else open = false;
                }
                
            }
	}

	// Toggle door open or closed
	public void toggle(bool state)
	{
		// Get AnimatedSprite2D
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var frame = 0;
		if (state)
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
