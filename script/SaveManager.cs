using Godot;
using System;

public partial class SaveManager : Node
{
	private string savePath = "res://resource/player_data.tres";
	public PlayerData Player { get; private set; }
	
	public override void _Ready() {
		CreateDefaultPlayer();
	}
	
	private void CreateDefaultPlayer() {
		Player = new PlayerData {
			Health = 100,
			Speed = 300f,
			BulletScene = GD.Load<PackedScene>("res://prefab/bullet.tscn"),
			ShootDelay = 0.5f,
			InvicibilityDuration = 2f
		};
	}
}
