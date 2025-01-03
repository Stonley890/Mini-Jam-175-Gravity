using Godot;
using System;

public partial class Player : CharacterBody2D
{ 
	[Export] int speed = 200;
	Vector2 velocity;
    Node2D TileDetect;
    TileMapLayer tileMap;
    AnimatedSprite2D sprite;
    bool slide = false;

    void playAnim()
    {
        if(velocity.X != 0  && velocity.Y == 0)
        {
            sprite.Play("Run");
            sprite.FlipH = velocity.X > 0;
        }
        else if(velocity.Y < 0)
            sprite.Play("Back Run");
        else if(velocity.Y > 0)
            sprite.Play("Run");
        else sprite.Play("Idle");
    }

    void Detected()
    {   
        if(tileMap.GetCellAtlasCoords((Vector2I)tileMap.LocalToMap(TileDetect.GlobalPosition)) == new  Vector2I(0,2))
            slide = true;
        else slide = false;
    }

	// Get the input from the player
	private void GetInput()
	{
        if(!slide)
		    velocity = Input.GetVector("ui_left", "ui_right","ui_up","ui_down") * speed;
	}

	// Called on run
	public override void _Ready()
	{
        tileMap = GetParent().GetNode<TileMapLayer>("tiles");
        TileDetect = GetNode<Node2D>("Detect");
        sprite = GetNode<AnimatedSprite2D>("pSprite");
	}

	// Called every frame.
	public override void _Process(double delta)
	{
		GetInput();
        Detected();
        playAnim();
		MoveAndCollide(velocity * (float)delta);
	}
}
