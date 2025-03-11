using Godot;
using System;

[GlobalClass]
public partial class PlayerData : Resource
{
	public int Health;
	public float Speed;
	public PackedScene BulletScene;
	public float ShootDelay;
	public float InvicibilityDuration;
}
