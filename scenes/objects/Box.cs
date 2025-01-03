using Godot;
using System;

public partial class Box : StaticBody2D
{
	Vector2 velocity;
<<<<<<< HEAD
	float Maxspeed = 250;
=======
	float Maxspeed = 50;
>>>>>>> d5eacad520e86e7a4a161d458ea117bac929aab0

	// Allows player to interact with the box through the use of an area2d
	public void PlayerEnter(Player p)
	{
<<<<<<< HEAD
		velocity += p.velocity.Normalized() * Maxspeed;	
=======
		velocity += p.velocity;	
>>>>>>> d5eacad520e86e7a4a161d458ea117bac929aab0
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
