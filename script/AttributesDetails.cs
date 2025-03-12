using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class AttributesDetails : Resource
{
	[Export] public string AttributeName { get; set; }
	[Export] public float Gain { get; set; }
	[Export] public int Price { get; set; }    
}
