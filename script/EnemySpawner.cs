using Godot;
using System;

public partial class EnemySpawner : StaticBody2D
{
	private PackedScene[] _enemyScenes;
	public string EnemyFolderPath = "res://prefab/enemies"; 
	[Export] public GameManager gameManager;
	private Node2D[] _spawners = new Node2D[4];
	private Random random = new Random();
	private Timer _spawnTimer;
	[Export] public float SpawnDelay;
	private bool _canSpawn = false;
	public int totalDificulty = 0;
	private int _maxDificulty = 10;

	public override void _Ready() {
		LoadEnemyScenes();
		
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
		if(totalDificulty > _maxDificulty) return;
		
		var enemyScene = _enemyScenes[random.Next(0, _enemyScenes.Length)];
		var enemy = enemyScene.Instantiate<Enemy>();
		enemy.GlobalPosition = _spawners[position].GlobalPosition;
		enemy.gameManager = gameManager;
		totalDificulty += enemy.DificultyLevel;
		GetParent().AddChild(enemy);
	}
	
	private void LoadEnemyScenes() {
		using var dir = DirAccess.Open(EnemyFolderPath);

		dir.ListDirBegin();
		var files = new System.Collections.Generic.List<string>();
		while (true) {
			var file = dir.GetNext();
			if (string.IsNullOrEmpty(file))
				break;
			if (!file.EndsWith(".tscn"))
				continue;
			files.Add(file);
		}
		dir.ListDirEnd();

		_enemyScenes = new PackedScene[files.Count];
		for (int i = 0; i < files.Count; i++) {
			var scenePath = $"{EnemyFolderPath}/{files[i]}";
			_enemyScenes[i] = GD.Load<PackedScene>(scenePath);
		}
	}
}
