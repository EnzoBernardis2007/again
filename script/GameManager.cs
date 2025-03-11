using Godot;
using System;

public partial class GameManager : Node2D
{
	private PackedScene _saveManagerScene;
	private SaveManager _saveManager;
	private PackedScene _playerScene;
	private Player _player;
	public int AccumulatedPoints = 0;
	
	public override void _Ready() {
		_saveManagerScene = GD.Load<PackedScene>("res://prefab/save_manager.tscn");
		_saveManager = _saveManagerScene.Instantiate<SaveManager>();
		AddChild(_saveManager);
		
		_playerScene = GD.Load<PackedScene>("res://prefab/player.tscn");
		_player = _playerScene.Instantiate<Player>();
		_player.playerData = _saveManager.Player;
		
		_player.GlobalPosition = new Vector2(1152.0f / 2f, 648.0f / 2f);
		_player.OnQueueFree += OnPlayerQueueFree;
		
		AddChild(_player);
	}
	
	private void OnPlayerQueueFree() {
		_saveManager.Player.Points += AccumulatedPoints;
		_saveManager.SavePlayer();
	}
}
