using Godot;
using System;
using System.Collections.Generic;

public partial class Shop : Control
{
	private PackedScene _menuScene;
	private PackedScene _buyAttributeScene;
	
	private string saveManagerPath = "res://script/SaveManager.cs";
	private SaveManager _saveManager;
	
	private Label _points;
	private Button _goBack;
	
	private string _basicPath = "ColorRect/VBoxContainer/";
	
	public override void _Ready() {
		_menuScene = ResourceLoader.Load<PackedScene>("res://scene/menu.tscn");
		_buyAttributeScene = ResourceLoader.Load<PackedScene>("res://prefab/buy_attribute.tscn");
		
		var script = GD.Load<CSharpScript>(saveManagerPath);
		var instance = script.New();
		
		_saveManager = instance.As<SaveManager>();
		_saveManager.LoadPlayer();
		_saveManager.Player.InitializeAttributes();
		
		SetupShop();
		SetupBuyButton();
	}
	
	private void SetupShop() {
		_points = GetNode<Label>("Points");
		_points.Text = $"TOTAL POINTS: {_saveManager.Player.Points}";
		
		_goBack = GetNode<Button>("GoBackButton");
		_goBack.Pressed += () => GetTree().ChangeSceneToPacked(_menuScene);
	}
	
	private void SetupBuyButton() {
		foreach (var attributeEntry in _saveManager.Player.Attributes) {
			string attributeName = attributeEntry.Key;
			AttributesDetails details = attributeEntry.Value;
			
			HBoxContainer container = (HBoxContainer)_buyAttributeScene.Instantiate();
			container.GetNode<Label>("Label").Text = $"{attributeName}: +{details.Gain}";
			container.GetNode<Button>("Button").Text = $"BUY FOR ${details.Price}";
			container.GetNode<Button>("Button").Pressed += () => OnUpgradeButtonPressed(attributeName, details);
			
			GetNode<VBoxContainer>(_basicPath).AddChild(container);
		}
	}
	
	private void OnUpgradeButtonPressed(string attributeName, AttributesDetails details) {
		if(_saveManager.Player.Points < details.Price) {
			GD.Print("nao tem dinheiro");
			return;
		}
		
		switch (attributeName)
		{
			case "Health":
				_saveManager.Player.Health += (int)details.Gain;
				break;
			case "Damage":
				_saveManager.Player.Damage += (int)details.Gain;
				break;
			case "Speed":
				_saveManager.Player.Speed += details.Gain;
				break;
			default:
				GD.PrintErr($"Atributo desconhecido: {attributeName}");
				return;
		}
		
		_saveManager.Player.Points -= details.Price;
		_saveManager.SavePlayer();
		_points.Text = $"TOTAL POINTS: {_saveManager.Player.Points}";
	}
}
