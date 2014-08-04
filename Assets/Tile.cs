using UnityEngine;

public class Tile {

	private Color[] pixels; // 1d array seems easier, since GetPixels() and SetPixels() uses 1d array

	public Tile(Texture2D tileset, int x, int y, int tile_resolution){
		this.pixels = tileset.GetPixels (
			x * tile_resolution, 
			y * tile_resolution, 
			tile_resolution, 
			tile_resolution);
	}

	public Color[] Pixels(){
		return this.pixels;
	}

}
