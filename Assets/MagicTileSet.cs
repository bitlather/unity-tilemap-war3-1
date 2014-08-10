using UnityEngine;

public class MagicTileSet {

	const int 
		tile_resolution = 16, // A tile is 16x16 pixels
		tiles_tall = 4, // A magic tile's tileset is 4 tiles tall
		tiles_wide = 8; // A magic tile's tileset is 8 tiles wide

	private Tile[,] tiles;

	public MagicTileSet(Texture2D tileset) {
		this.tiles = new Tile[tiles_wide, tiles_tall];

		if(tileset.width != tile_resolution * tiles_wide){
			Debug.LogError("Magic tileset is not correct width");
		}

		if(tileset.height != tile_resolution * tiles_tall){
			Debug.LogError("Magic tileset is not correct height");
		}

		for(int y = 0; y < tiles_tall; y++){
			for(int x = 0; x < tiles_wide; x++){
				this.tiles[x, y] = new Tile(tileset, x, y, tile_resolution);
			}
		}
	}

	public Tile GetTile(){
		// Always return top left tile for now; eventually take 9 tiles and determine what to use
		return this.tiles[0,3];
	}
}
