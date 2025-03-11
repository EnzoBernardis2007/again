using Godot;
using System;

[GlobalClass]
public partial class PlayerData : Resource 
{
	[Export] public int Points { get; set; }
	[Export] public int Health { get; set; }
	[Export] public int Damage { get; set; }
	[Export] public float Speed { get; set; }
	[Export] public PackedScene BulletScene { get; set; }
	[Export] public float ShootDelay { get; set; }
	[Export] public float InvicibilityDuration { get; set; }
}
