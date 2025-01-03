using Godot;
using System;

public partial class Player : CharacterBody2D
{ 
	[Export] int speed = 200;
	Vector2 velocity;

	// Get the input from the player
	private void GetInput()
	{
		velocity = Input.GetVector("ui_left", "ui_right","ui_up","ui_down") * speed;
		Velocity = velocity;
	}

	// Called on run
	public override void _Ready()
	{
	}

	// Called every frame.
	public override void _Process(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
}
