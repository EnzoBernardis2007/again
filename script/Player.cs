using Godot;
using System;

public partial class Player : Entity
{
	[Export] public float Speed;
	[Export] public PackedScene BulletScene;
	[Export] public float ShootDelay;
	
	private bool _canShoot = true;
	private Timer _shootTimer;
	
	[Export] public float InvicibilityDuration;
	private Timer _invicibilityTimer;
	public bool _isInvicible = false;
	
	private Control ui;
	private TextureProgressBar healthBar;
	
	public override void _Ready() {
		AddToGroup("player");
		SetShootTimer();
		SetInvicibilityTimer();
		
		ui = GetNode<Control>("../UI");
		healthBar = ui.GetNode<TextureProgressBar>("HBoxContainer/ColorRect/TextureProgressBar");
		healthBar.MaxValue = Health;
		UpdateHealthBar();
	}
	
	private void SetShootTimer() {
		_shootTimer = GetNode<Timer>("ShootTimer");
		_shootTimer.WaitTime = ShootDelay;
		_shootTimer.Timeout += OnShootTimerTimeout;
	}
	
	private void SetInvicibilityTimer() {
		_invicibilityTimer = GetNode<Timer>("InvicibilityTimer");
		_invicibilityTimer.WaitTime = InvicibilityDuration;
		_invicibilityTimer.Timeout += OnInvicibilityTimerTimeout;
	}
	
	private void UpdateHealthBar() {
		healthBar.Value = Health;
	}
	
	public override void _PhysicsProcess(double delta) {
		HandleMovement();
		MoveAndSlide();
		HandleShooting();
	}
	
	private void HandleMovement() {
		Vector2 direction = Vector2.Zero;
		
		if(Input.IsActionPressed("move_up")) direction.Y -= 1;
		if(Input.IsActionPressed("move_down")) direction.Y += 1;
		if(Input.IsActionPressed("move_right")) direction.X += 1;
		if(Input.IsActionPressed("move_left")) direction.X -= 1;
		
		direction = direction.Normalized();
		Velocity = direction * Speed;
	}
	
	private void HandleShooting() {
		if (!_canShoot) return;
		
		if(Input.IsActionPressed("shoot_up")) Shoot(Vector2.Up);
		else if(Input.IsActionPressed("shoot_down")) Shoot(Vector2.Down);
		else if(Input.IsActionPressed("shoot_right")) Shoot(Vector2.Right);
		else if(Input.IsActionPressed("shoot_left")) Shoot(Vector2.Left);
	}
	
	private void Shoot(Vector2 direction) {
		var bullet = BulletScene.Instantiate<Bullet>();
		bullet.Direction = direction;
		
		float offset = 30f;
		bullet.GlobalPosition = GlobalPosition + (direction * offset);
		GetParent().AddChild(bullet);
		
		_canShoot = false;
		_shootTimer.Start();
	}
	
	private void OnShootTimerTimeout() {
		_canShoot = true;
	}
	
	private void OnInvicibilityTimerTimeout() {
		_isInvicible = false;
	}
	
	public override void TakeDamage(int damage) {
		if(_isInvicible) return;
		base.TakeDamage(damage);
		_isInvicible = true;
		_invicibilityTimer.Start();
		UpdateHealthBar();
	}
}
