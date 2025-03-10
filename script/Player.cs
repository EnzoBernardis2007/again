using Godot;
using System;

public partial class Player : Entity
{
	[Export] public float Speed;
	[Export] public PackedScene BulletScene;
	[Export] public float ShootDelay;
	
	private bool _canShoot = true;
	private Timer _shootTimer;
	
	public override void _Ready() {
		AddToGroup("player");
		_shootTimer = GetNode<Timer>("ShootTimer");
		_shootTimer.WaitTime = ShootDelay;
		_shootTimer.Timeout += OnShootTimerTimeout;
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
}
