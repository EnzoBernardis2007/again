using Godot;
using System;

public partial class GameManager : Node2D
{
	private PackedScene _saveManagerScene;
	private SaveManager _saveManager;
	private PackedScene _playerScene;
	private PackedScene _menuScene;
	private Player _player;
	public int AccumulatedPoints = 0;
	
	public override void _Ready() {
		_saveManagerScene = GD.Load<PackedScene>("res://prefab/save_manager.tscn");
		_saveManager = _saveManagerScene.Instantiate<SaveManager>();
		AddChild(_saveManager);
		
		_playerScene = GD.Load<PackedScene>("res://prefab/player.tscn");
		_player = _playerScene.Instantiate<Player>();
		_player.playerData = _saveManager.Player;
		
		_player.GlobalPosition = new Vector2(1152.0f / 2f, 648.0f / 2f);
		_player.OnQueueFree += OnPlayerQueueFree;
		
		AddChild(_player);
		
		_menuScene = GD.Load<PackedScene>("res://scene/menu.tscn");
	}
	
	private void OnPlayerQueueFree()
{
	// Adiciona pontos acumulados ao jogador e salva
	_saveManager.Player.Points += AccumulatedPoints;
	_saveManager.SavePlayer();

	// Obtém o container dos botões
	HBoxContainer container = GetNodeOrNull<HBoxContainer>("UI/HBoxContainer/Spacer/DieMenu/HBoxContainer");
	if (container == null) 
	{
		GD.PrintErr("Container não encontrado.");
		return;
	}

	// Obtém o botão de menu
	Button menuButton = container.GetNodeOrNull<Button>("MenuButton");
	if (menuButton != null)
	{
		menuButton.Pressed += OnMenuButtonPressed;
	}
	else
	{
		GD.PrintErr("Botão de menu não encontrado.");
	}

	// Obtém o botão de tentar novamente
	Button againButton = container.GetNodeOrNull<Button>("AgainButton");
	if (againButton != null)
	{
		againButton.Pressed += OnAgainButtonPressed;
	}
	else
	{
		GD.PrintErr("Botão de tentar novamente não encontrado.");
	}

	// Torna o menu de morte visível
	ColorRect dieMenu = GetNodeOrNull<ColorRect>("UI/HBoxContainer/Spacer/DieMenu");
	if (dieMenu != null)
	{
		dieMenu.Visible = true;
	}
	else
	{
		GD.PrintErr("Menu de morte não encontrado.");
	}
}

	private void OnMenuButtonPressed()
	{
		// Carrega a cena do menu
		if (_menuScene != null)
		{
			GetTree().ChangeSceneToPacked(_menuScene);
		}
		else
		{
			GD.PrintErr("Cena do menu não definida.");
		}
	}

	private void OnAgainButtonPressed()
	{
		// Recarrega a cena atual
		string currentScenePath = GetTree().CurrentScene.SceneFilePath;
		if (!string.IsNullOrEmpty(currentScenePath))
		{
			GetTree().ChangeSceneToFile(currentScenePath);
		}
		else
		{
			GD.PrintErr("Falha ao obter o caminho da cena atual.");
		}
	}
}
