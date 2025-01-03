using Godot;
using System;

public partial class Player : CharacterBody2D
{ 
	[Export] int speed = 200;
	Vector2 velocity;
    RayCast2D TileDetect;
    TileMapLayer tileMap;
    bool slide = false;

    public void Detected()
    {   
        if(tileMap.GetCellAtlasCoords((Vector2I)tileMap.LocalToMap(Position)) == new  Vector2I(0,2))
        {
            GD.Print("Ice Detected");
            slide = true;
        }
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
        TileDetect = GetNode<RayCast2D>("TileDetect");
	}

	// Called every frame.
	public override void _Process(double delta)
	{
		GetInput();
        Detected();
		MoveAndCollide(velocity * (float)delta);
	}
}
