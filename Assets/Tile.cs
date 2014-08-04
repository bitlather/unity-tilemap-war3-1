using UnityEngine;

public class Tile {

	private Color[] pixels; // 1d array seems easier, since GetPixels() and SetPixels() uses 1d array

	public Tile(Color color){
		// 16 should not be implied! It should be a constant somewhere!
		this.pixels = new Color[16*16];
		for(int i=0; i<16*16; i++){
			this.pixels[i] = color;
		}
	}

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
