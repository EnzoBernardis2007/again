using Godot;
using System;
	
public partial class Bullet : CharacterBody2D
{
	[Export] public float Speed = 500f; 
	[Export] public int Damage = 1;
	private Area2D _area;
	public Vector2 Direction = Vector2.Right;
	
	public override void _Ready() {
		_area = GetNode<Area2D>("Area2D");
		_area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}

	public override void _PhysicsProcess(double delta) {
		Velocity = Direction * Speed;
		MoveAndSlide();

		if (!GetViewportRect().HasPoint(GlobalPosition)) {
			QueueFree();
		}
	}
	
	private void OnBodyEntered(Node2D body) {
		if(!body.IsInGroup("enemy")) return;
		if(!body.HasMethod("TakeDamage")) return;
		
		body.Call("TakeDamage", Damage);
		QueueFree();
	}
}
