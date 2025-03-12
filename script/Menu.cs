using Godot;
using System;

public partial class Menu : Control
{
	private Button _playButton;
	private Button _shopButton;
	
	public override void _Ready() {
		_playButton = GetNode<Button>("VBoxContainer/HBoxContainer/PlayButton");
		_shopButton = GetNode<Button>("VBoxContainer/HBoxContainer/ShopButton");
		
		_playButton.Pressed += OnPlayButtonPressed;
		_shopButton.Pressed += OnShopButtonPressed;
	}
	
	private void OnPlayButtonPressed() {
		var packedScene = ResourceLoader.Load<PackedScene>("res://scene/test.tscn");
		GetTree().ChangeSceneToPacked(packedScene);
	}
	
	private void OnShopButtonPressed() {
		var packedScene = ResourceLoader.Load<PackedScene>("res://scene/shop.tscn");
		GetTree().ChangeSceneToPacked(packedScene);
	}
}
