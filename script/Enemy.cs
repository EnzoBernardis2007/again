using Godot;
using System;

public partial class Enemy : Entity
{
	[Export] public float Speed;
	[Export] public int Damage;
	private CharacterBody2D _player;
	private Area2D _damageArea;
	
	public override void _Ready() {
		AddToGroup("enemy");
		_player = GetNode<CharacterBody2D>("../Player");
		_damageArea = GetNode<Area2D>("Area2D");
		_damageArea.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}
	
	public override void _PhysicsProcess(double delta) {
		HandleMovement();
	}
	
	private void HandleMovement() {
		if(_player == null) return;
		
		Vector2 direction = (_player.GlobalPosition - GlobalPosition).Normalized();
		
		Velocity = direction * Speed;
		MoveAndSlide();
	}
	
	private void OnBodyEntered(Node2D body) {
		if(!body.IsInGroup("player")) return;
		if(!body.HasMethod("TakeDamage")) return;
		
		body.Call("TakeDamage", Damage);
	}
}
