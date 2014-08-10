using UnityEngine;
using System.Collections;

// https://www.youtube.com/watch?v=tH7MI2SpMZg
// Unity 3d: TileMaps - Part 7 - Tile/Sprite Atlases

// Note that we created a file, TileMapInspector7, in /Assets/Editor. This file must exist in the Editor folder to work.
// Note our green tile cursor is child of an empty object (TileSelectionIndicator) to make math easier. Note the position of cube inside of empty object.
// Towards end of video 5, around 27:30, he recommends creating a MouseManager object

/* NOTES
 * - Monodevelop: When your cursor is on a variable, if you press F2, you can refactor variable name.
 * - [ExecuteInEditMode] makes the script run while in the editor, so you can see the tilemap.
 * - Console warning:
 *       Cleaning up leaked objects in scene since no game object, component or manager is referencing them
 *       Mesh  has been leaked 3 times.
 *   is OK, according to quill18, because that object will just be garbage collected. We might be able to destroy it somehow if we don't want it. (Part 4 @~ 9:30
 * - Part 7: In /Textures, click civ_map_textures7.png and change type to advanced, then check Read/Write enabled in order to gain access to GetPixels()
 */
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap7 : MonoBehaviour {

	public int 
		size_x = 100,  // Number of tiles on the x axis
		size_z = 50;
	public float
		tile_size = 1.0f; // Size in unity units

	public Texture2D terrain_tiles; // drag the texture onto the object in unity editor
	public int tile_resolution = 16; // 16x16 pixel tiles
	public int num_tiles = 4; // four tiles in tilemap

	Color[][] ChopUpTiles(){
		int num_tiles_per_row = this.terrain_tiles.width / this.tile_resolution;
		int num_rows = this.terrain_tiles.height / this.tile_resolution;

		Color[][] tiles = new Color[num_tiles_per_row*num_rows][];

		for (int y=0; y<num_rows; y++) {
			for (int x=0; x<num_tiles_per_row; x++) {
				tiles [y * num_tiles_per_row + x] = terrain_tiles.GetPixels (x * tile_resolution, y * tile_resolution, tile_resolution, tile_resolution); // hugely inefficient
			}
		}
		return tiles;
	}

	// Use this for initialization
	void Start () {
		BuildMesh();
	}

	public MagicTileSet magic_tile_set;

	MagicTileSet LoadMagicTile(){
		Texture2D texture = (Texture2D)Resources.Load("war3-grass");

		if(texture == null){
			Debug.LogError("NULL TEXTURE AT RESOURCES.LOAD()");
		}

		return new MagicTileSet(texture);
	}
	public void BuildMesh(){
		MagicTileSet magic_tile_set = LoadMagicTile();
		TileMeshData tile_mesh_data = new TileMeshData(magic_tile_set);
		tile_mesh_data.BuildMesh(GetComponent<MeshFilter> (),
			GetComponent<MeshRenderer> (), //this one doesnt seem to be used
			GetComponent<MeshCollider> ());
	}
}
