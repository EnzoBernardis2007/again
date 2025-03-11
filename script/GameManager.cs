using Godot;
using System;

public partial class GameManager : Node2D
{
	private PackedScene _saveManagerScene;
	private PackedScene _playerScene;
	public int AccumulatedPoints = 0;
	
	public override void _Ready() {
		_saveManagerScene = GD.Load<PackedScene>("res://prefab/save_manager.tscn");
		var saveManager = _saveManagerScene.Instantiate<SaveManager>();
		AddChild(saveManager);
		
		_playerScene = GD.Load<PackedScene>("res://prefab/player.tscn");
		var player = _playerScene.Instantiate<Player>();
		player.playerData = saveManager.Player;
		
		player.GlobalPosition = new Vector2(1152.0f / 2f, 648.0f / 2f);
		
		AddChild(player);
	}
}
