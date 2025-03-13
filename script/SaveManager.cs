using Godot;
using System;

public partial class SaveManager : Node
{
	public string savePath = "res://resource/player_data.tres";
	public PlayerData Player { get; set; }
	
	public override void _Ready() {
		LoadPlayer();
	}
	
	public void LoadPlayer() {
		if(!ResourceLoader.Exists(savePath)) {
			GD.Print("creating");
			CreateDefaultPlayer();
			SavePlayer();
		} else {
			Player = ResourceLoader.Load<PlayerData>(savePath);
		}
	}
	
	public void SavePlayer() {
		ResourceSaver.Save(Player, savePath);
	}
	
	private void CreateDefaultPlayer() {
		Player = new PlayerData {
			Points = 0,
			Health = 100,
			Damage = 1f,
			Speed = 300f,
			BulletScene = GD.Load<PackedScene>("res://prefab/bullet.tscn"),
			ShootDelay = 0.5f,
			InvicibilityDuration = 2f
		};
	}
}
