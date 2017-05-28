using UnityEngine;
using UnityEditor;

public class DataGenerator
{
	[MenuItem("Data/Make Some Data")]
	static void Init()
	{
		MyData data = ScriptableObject.CreateInstance<MyData>();
		data.text = "Hello World!";
		AssetDatabase.CreateAsset(data, "Assets/data.asset");
	}
}