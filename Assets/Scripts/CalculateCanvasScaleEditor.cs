using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CalculateCanvasScale))]

public class CalculateCanvasScaleEditor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		CalculateCanvasScale t = (CalculateCanvasScale)target;
		GUILayout.Label("Расчет масштаба холста:", EditorStyles.boldLabel);
		if (GUILayout.Button("Calculate")) t.Calculate();
	}
}
