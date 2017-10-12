using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScriptableObjectCraator : EditorWindow {

	private static ScriptableObjectCraator instanceWindow;

	private MonoScript target;

	private string commonPath = "Assets/Resources/";
	private string inputPath = "";

	[MenuItem("JohnnyGameStudio/ScriptableObjectCraator")]
	private static void Open()
	{
		if (instanceWindow == null)
		{
			var window = CreateInstance<ScriptableObjectCraator>();
			window.Show();
			instanceWindow = window;
		}
	}

	void OnGUI()
	{
		try
		{
			target = (MonoScript)EditorGUILayout.ObjectField("Select Script", target, typeof(MonoScript), false,GUILayout.Width(350));

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("output path ->", GUILayout.Width(100));
			EditorGUILayout.LabelField(commonPath, GUILayout.Width(180));
			inputPath = EditorGUILayout.TextField(inputPath, GUILayout.Width(200));
			EditorGUILayout.EndHorizontal();
			
			if (GUILayout.Button("Create Asset", GUILayout.Width(350),GUILayout.Height(50)))
			{
				//MonoScript指定だからスクリプトファイルしかないはず
				//TextやObjectファイルは・・・エラー吐くからいいよね
				var obj = CreateInstance(target.name);
				string tmp = inputPath.Contains(".asset") ? "" :".asset" ;
				string path = commonPath + inputPath + tmp;
				AssetDatabase.CreateAsset(obj,path);
				AssetDatabase.Refresh();
			}
		}
		catch (System.Exception e)
		{
			Debug.LogError(e); 
		}
	}
}
