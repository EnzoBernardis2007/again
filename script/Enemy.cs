using Godot;
using System;

public partial class Enemy : Entity
{
	public EnemySpawner enemySpawner;
	[Export] public float Speed;
	[Export] public float Damage;
	[Export] public int PointsWorth;
	[Export] public int DificultyLevel;
	public GameManager gameManager;
	private CharacterBody2D _player;
	private Area2D _damageArea;
	private bool _dealDamage = false;
	
	public override void _Ready() {
		enemySpawner = GetNode<EnemySpawner>("../EnemySpawner");
		AddToGroup("enemy");
		_player = GetNode<CharacterBody2D>("../Player");
		_damageArea = GetNode<Area2D>("Area2D");
		_damageArea.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		_damageArea.Connect("body_exited", new Callable(this, nameof(OnBodyExited)));
	}
	
	public override void _PhysicsProcess(double delta) {
		HandleMovement();
		HandleDamage();
	}
	
	private void HandleMovement() {
		if(_player == null) return;
		
		Vector2 direction = (_player.GlobalPosition - GlobalPosition).Normalized();
		
		Velocity = direction * Speed;
		MoveAndSlide();
	}
	
	private void HandleDamage() {
		if (_dealDamage && _player is Player playerScript) {
			playerScript.TakeDamage(Damage);
		}
	}
	
	private void OnBodyExited(Node2D body) {
		if(!body.IsInGroup("player")) return;
		if(!body.HasMethod("TakeDamage")) return;
		
		_dealDamage = false;
	}
	
	private void OnBodyEntered(Node2D body) {
		if(!body.IsInGroup("player")) return;
		if(!body.HasMethod("TakeDamage")) return;
		
		_dealDamage = true;
	}
	
	public override void Die() {
		gameManager.AccumulatedPoints += PointsWorth;
		enemySpawner.totalDificulty -= DificultyLevel;
		base.Die();
	}
}
