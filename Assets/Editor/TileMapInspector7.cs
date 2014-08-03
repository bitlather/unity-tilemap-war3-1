// This class lets the tile map update in the unity editor when you change public variables
using UnityEditor; // <-- Important!
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TileMap7))] // <-- Makes it appear in unity editor when you click objects that have script TileMap4
public class TileMapInspector7 : Editor {

	public override void OnInspectorGUI(){
		//base.OnInspectorGUI (); <-- seems be the same as DrawDefaultInspector();
		DrawDefaultInspector ();

		if (GUILayout.Button ("Regenerate")) {
			TileMap7 tileMap = (TileMap7)target; // <-- target just exists :-)
			tileMap.BuildMesh();
		}

	}
}

/* We can create sliders in the editor, too, like this:
public class TileMapInspector4 : Editor {
	
	float value = 0.5f;
	
	public override void OnInspectorGUI(){
		//base.OnInspectorGUI (); <-- seems be the same as DrawDefaultInspector();
		DrawDefaultInspector ();
		
		EditorGUILayout.BeginVertical ();
		this.value = EditorGUILayout.Slider (this.value, 0, 2.0f);
		EditorGUILayout.EndVertical ();
		
		if (GUILayout.Button ("Regenerate")) {
			TileMap4 tileMap = (TileMap4)target; // <-- target just exists :-)
			tileMap.BuildMesh();
		}
		
	}
}
*/