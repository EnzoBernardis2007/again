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
			try {
				Player = ResourceLoader.Load<PlayerData>(savePath);
				
				GD.Print("Player data loaded successfully:");
				GD.Print("Health: " + Player.Health);
				GD.Print("Speed: " + Player.Speed);
				GD.Print("BulletScene: " + Player.BulletScene);
				GD.Print("ShootDelay: " + Player.ShootDelay);
				GD.Print("InvicibilityDuration: " + Player.InvicibilityDuration);
			} catch {
				GD.Print("error");
			}
		}
	}
	
	private void SavePlayer() {
		ResourceSaver.Save(Player, savePath);
	}
	
	private void CreateDefaultPlayer() {
		Player = new PlayerData {
			Points = 0,
			Health = 100,
			Damage = 10,
			Speed = 300f,
			BulletScene = GD.Load<PackedScene>("res://prefab/bullet.tscn"),
			ShootDelay = 0.5f,
			InvicibilityDuration = 2f
		};
	}
}
