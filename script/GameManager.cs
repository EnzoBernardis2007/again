using Godot;
using System;

public partial class GameManager : Node2D
{
	private PackedScene _saveManagerScene;
	private PackedScene _playerScene;
	
	public override void _Ready() {
		_saveManagerScene = GD.Load<PackedScene>("res://prefab/save_manager.tscn");
		var saveManager = _saveManagerScene.Instantiate<SaveManager>();
		AddChild(saveManager);
		
		_playerScene = GD.Load<PackedScene>("res://prefab/player.tscn");
		var player = _playerScene.Instantiate<Player>();
		player.playerData = saveManager.Player;
		AddChild(player);
	}
}
