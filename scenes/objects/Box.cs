using Godot;
using System;

public partial class Box : StaticBody2D
{
	Vector2 velocity;
	float Maxspeed = 250;

	// Allows player to interact with the box through the use of an area2d
	public void PlayerEnter(Player p)
	{
		velocity += p.velocity.Normalized() * Maxspeed;	
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	
		velocity = velocity.MoveToward(Vector2.Zero, 10);
		MoveAndCollide(velocity * (float)delta);
	}
}
