using Godot;
using System;

public partial class Player : CharacterBody2D
{ 
	[Export] int speed = 200;
	public Vector2 velocity;
    Node2D TileDetect;
    TileMapLayer floorTileMap;
    TileMapLayer ceilingTileMap;
    AnimatedSprite2D sprite;
    CollisionShape2D collisionShape;

    bool slide = false;
    bool swapped = false;

    // Plays player animations bassed on velocity
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

    // Detects if the player is on an ice tile
    void Detected()
    {   
        if(floorTileMap.GetCellAtlasCoords((Vector2I)floorTileMap.LocalToMap(TileDetect.GlobalPosition)) == new Vector2I(0,2) && !swapped || ceilingTileMap.GetCellAtlasCoords((Vector2I)ceilingTileMap.LocalToMap(TileDetect.GlobalPosition)) == new Vector2I(0,2) && swapped)
            slide = true;
        else slide = false;
    }

	// Get the input from the player for movement
	private void GetInput()
	{
        if(!slide)
		    velocity = Input.GetVector("ui_left", "ui_right","ui_up","ui_down") * speed;       
	}

	// Called on run
	public override void _Ready()
	{
        floorTileMap = GetParent().GetNode<TileMapLayer>("Floor");
        ceilingTileMap = GetParent().GetNode<TileMapLayer>("Ceiling");
        TileDetect = GetNode<Node2D>("Detect");
        sprite = GetNode<AnimatedSprite2D>("pSprite");
        collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
	}

	// Called every frame.
	public override void _Process(double delta)
	{
		GetInput();
        Detected();
        playAnim();
		MoveAndCollide(velocity * (float)delta);
	}

    // collects player input
    public override void _Input(InputEvent @event)
    {
        // Switches between floor and ceiling tilemaps when player presses space
        if(@event.IsActionPressed("swapGravity"))
        {
            if(swapped)
            {
                floorTileMap.Enabled = true;
                ceilingTileMap.Enabled = false;
            }
            else {
                floorTileMap.Enabled = false;
                ceilingTileMap.Enabled = true;
            }
            swapped = !swapped;
            GD.Print("Swapped Gravity");
        }
        if(@event.IsActionPressed("interact"))
        {
            // Get all levers in the scene
            Godot.Collections.Array<Godot.Node> nodes = GetParent().GetNode("Objects").GetChildren();

    		// Loop through all the levers
            for(int i = 0; i < nodes.Count; i++)
            {
                // Get the lever
                Node node = nodes[i];

                // Check if it is a lever scene
                if(node is Lever lever)
                {
                    // check if player is colliding with the lever
                    if(lever.GetActivationArea().OverlapsBody(this))
                    {
                        lever.Toggle(!lever.GetEnabled());
                    }
                }
                
            }
            
        }
    }
}
