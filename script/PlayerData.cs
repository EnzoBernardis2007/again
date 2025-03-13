using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class PlayerData : Resource 
{
	[Export] public int Points { get; set; }
	[Export] public int Health { get; set; }
	[Export] public float Damage { get; set; }
	[Export] public float Speed { get; set; }
	[Export] public PackedScene BulletScene { get; set; }
	[Export] public float ShootDelay { get; set; }
	[Export] public float InvicibilityDuration { get; set; }
	
	public Dictionary<string, AttributesDetails> Attributes { get; set; } = new();
	
	public void InitializeAttributes() {
		Attributes = new Dictionary<string, AttributesDetails> 
		{
			{ "Health", new AttributesDetails { AttributeName = "Health", Gain = 10, Price = 100 } },
			{ "Damage", new AttributesDetails { AttributeName = "Damage", Gain = 0.1f, Price = 100 } },
			{ "Speed", new AttributesDetails { AttributeName = "Spped", Gain = 10, Price = 100 } },
		};
	}
}
