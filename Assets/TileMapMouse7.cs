using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap7))] // Needed to capture size of tile
public class TileMapMouse7 : MonoBehaviour {

	TileMap7 tile_map;
	Vector3 current_tile_coord;

	public Transform selection_cube;

	void Start() {
		this.tile_map = GetComponent<TileMap7> ();
	}

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.mainCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;

		// collider is from the object
		if (collider.Raycast (ray, out hitInfo, Mathf.Infinity)) {
			// Get hit point //TODO NEED TO TAKE INTO CONSIDERATION ROTATION AND POSSIBLY transform.worldToLocalMatrix - quill18 said to look it up around 14:30 if we need it 
			int x = Mathf.FloorToInt (hitInfo.point.x / this.tile_map.tile_size);
			int z = Mathf.FloorToInt(hitInfo.point.z / this.tile_map.tile_size);
			//Debug.Log ("Tile #: "+x+","+z);

			this.current_tile_coord.x = x;
			this.current_tile_coord.z = z;

			this.selection_cube.transform.position = current_tile_coord * 1f;
		} else {
			// hide selection cube
		}

		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Left mouse click");
		}
	}
}
