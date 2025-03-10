using Godot;
using System;

public partial class Entity : CharacterBody2D
{
	[Export] public int Health;
	
	public void TakeDamage(int damage) {
		Health = Math.Max(Health - damage, 0);
		
		if(Health <= 0) {
			Die();
		}
	}
	
	private void Die() {
		QueueFree();
	}
}
