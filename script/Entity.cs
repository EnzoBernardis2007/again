using Godot;
using System;

public partial class Entity : CharacterBody2D
{
	[Export] public float Health;
	
	public virtual void TakeDamage(float damage) {
		Health = Math.Max(Health - damage, 0);
		
		if(Health <= 0) {
			Die();
		}
	}
	
	public virtual void Die() {
		QueueFree();
	}
}
