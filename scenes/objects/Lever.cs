using Godot;
using System;

public partial class Lever : StaticBody2D
{

	// whether the lever is enabled. controlled via toggle(bool state)
	[Export] bool enabled = false;
	// the control group of the lever. all levers in the same control group should
	// share the state of the lever.
	[Export] int controlGroup = 0;

	[Signal]
	public delegate void LeverStateChangedEventHandler(bool state, int controlGroup);

	private AnimatedSprite2D _animatedSprite2D;
	
	private Area2D activationArea;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (GetNode("ActivationArea") is Area2D area2D)
		{
			activationArea = area2D;
		}
		if(enabled)
		{
			Toggle(true)
		}
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Toggle lever on and off
	public void Toggle(bool state)
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
		
		EmitSignal(SignalName.LeverStateChanged, enabled, controlGroup);
	}

	// Get the state of the lever
	public bool GetEnabled()
	{
		return enabled;
	}

	// TODO: I'm not actually sure if this will work across multiple levers in a stage.
	// This may need more work.
	private void OnLeverStateChanged(bool state, int controlGroup)
	{
		if (this.controlGroup == controlGroup)
		{
			this.enabled = state;
		}
	}

	public Area2D GetActivationArea()
	{
		return activationArea;
	}
}
