using Godot;
using System;

public partial class Menu : Control
{
	private Button _playButton;
	
	public override void _Ready() {
		_playButton = GetNode<Button>("VBoxContainer/Button");
		
		_playButton.Pressed += OnButtonPressed;
	}
	
	private void OnButtonPressed() {
		var packedScene = ResourceLoader.Load<PackedScene>("res://scene/test.tscn");
		GetTree().ChangeSceneToPacked(packedScene);
	}
}
