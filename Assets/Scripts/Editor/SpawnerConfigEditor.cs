using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CustomEditor(typeof(SpawnerConfig))]
public class SpawnerConfigEditor : Editor
{
	private SpawnerConfig Config;
	private List<FieldInfo> Fields = new List<FieldInfo>(0);

	void OnEnable()
	{
		UpdateFields();
	}

	private void DrawAllFields(string arrayName, Action<int, SerializedProperty> initialBlockCallback)
	{
		foreach (var field in Fields)
		{
			var property = serializedObject.FindProperty(field.Name);
			if (property == null)
			{
				continue;
			}

			if (property.isArray && field.Name == arrayName)
			{
				for (int i = 0; i < property.arraySize; i++)
				{
					var arrayElement = property.GetArrayElementAtIndex(i);
					initialBlockCallback?.Invoke(i, arrayElement);
				}
			}
		}
	}

	private void UpdateFields()
	{
		if (typeof(SpawnerConfig) == target.GetType())
		{
			Config = (SpawnerConfig)target;
			if (Config != null)
			{
				var configType = typeof(SpawnerConfig);
				List<Array> fieldsArrays = new List<Array> { configType.GetFields() };
				var derived = configType;
				do
				{
					derived = derived.BaseType;
					if (derived != null)
					{
						fieldsArrays.Add(derived.GetFields());
					}
				} while (derived != null);

				if (fieldsArrays.Count > 0)
				{
					List<FieldInfo> allFields = new List<FieldInfo>();
					List<string> fieldsNames = new List<string>();
					for (int i = fieldsArrays.Count - 1; i >= 0; i--)
					{
						for (int j = 0; j < fieldsArrays[i].Length; j++)
						{
							var field = fieldsArrays[i].GetValue(j) as FieldInfo;
							if (!fieldsNames.Contains(field.Name))
							{
								allFields.Add(field);
								fieldsNames.Add(field.Name);
							}
						}
					}
					Fields = allFields;
				}
			}
		}
	}

	
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		foreach (var field in Fields)
		{
			var prop = serializedObject.FindProperty(field.Name);
			EditorGUILayout.PropertyField(prop, new GUIContent(prop.name));
		}
		
		
		serializedObject.ApplyModifiedProperties();
	}
}
