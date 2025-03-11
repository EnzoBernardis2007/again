using Godot;
using System;

public partial class EnemySpawner : StaticBody2D
{
	[Export] public PackedScene EnemyScene;
	[Export] public GameManager gameManager;
	private Node2D[] _spawners = new Node2D[4];
	private Random random = new Random();
	private Timer _spawnTimer;
	[Export] public float SpawnDelay;
	private bool _canSpawn = false;

	public override void _Ready() {
		_spawners[0] = GetNode<Node2D>("TopSpawner");
		_spawners[1] = GetNode<Node2D>("BottomSpawner");
		_spawners[2] = GetNode<Node2D>("LeftSpawner");
		_spawners[3] = GetNode<Node2D>("RightSpawner");

		_spawnTimer = GetNode<Timer>("Timer");
		_spawnTimer.WaitTime = SpawnDelay;
		_spawnTimer.Timeout += OnSpawnTimerTimeout;
		_spawnTimer.Start();
	}

	public override void _PhysicsProcess(double delta) {
		if (_canSpawn) {
			InstantiateEnemy(random.Next(0, _spawners.Length));
			_canSpawn = false;
			_spawnTimer.Start();
		}
	}

	private void OnSpawnTimerTimeout() {
		_canSpawn = true; 
	}

	private void InstantiateEnemy(int position) {
		var enemy = EnemyScene.Instantiate<Enemy>();
		enemy.GlobalPosition = _spawners[position].GlobalPosition;
		enemy.gameManager = gameManager;
		GetParent().AddChild(enemy);
	}
}
