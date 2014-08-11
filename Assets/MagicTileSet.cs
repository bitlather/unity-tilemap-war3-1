using UnityEngine;

public class MagicTileSet {

	const int 
		tile_resolution = 16, // A tile is 16x16 pixels
		tiles_tall = 4, // A magic tile's tileset is 4 tiles tall
		tiles_wide = 8; // A magic tile's tileset is 8 tiles wide
	const uint 
		TOP_LEFT_BIT = 1,
		TOP_BIT = 2,
		TOP_RIGHT_BIT = 4,
		LEFT_BIT = 8,
		RIGHT_BIT = 16,
		BOTTOM_LEFT_BIT = 32,
		BOTTOM_BIT = 64,
		BOTTOM_RIGHT_BIT = 128;

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

	public Tile GetTile(bool[,] map, uint x, uint z){
		uint
			bits = 0,
			map_height = (uint)map.GetLength(1),
			map_width = (uint)map.GetLength(0);

		// This is left most tile
		if(x == 0){
			bits |= TOP_LEFT_BIT | LEFT_BIT | BOTTOM_LEFT_BIT;
		}
		// This is top most tile
		if(z == map_height){
			bits |= TOP_LEFT_BIT | TOP_BIT | TOP_RIGHT_BIT;
		}
		// This is right most tile
		if(x == map_width){
			bits |= TOP_RIGHT_BIT | RIGHT_BIT | BOTTOM_RIGHT_BIT;
		}
		// This is bottom most tile
		if(z == 0){
			bits |= TOP_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT;
		}

		// Handle top bits for every non top row of map
		if(z+1 < map_height){
			if(x >= 1 && map[x-1, z+1]){
				bits |= TOP_LEFT_BIT;
			}
			if(map[x, z+1]){
				bits |= TOP_BIT;
			}
			if(x+1 < map_width && map[x+1, z+1]){
				bits |= TOP_RIGHT_BIT;
			}
		}

		// Handle bottom bits for every non bottom row of map
		if(z > 0){
			if(x >= 1 && map[x-1, z-1]){
				bits |= BOTTOM_LEFT_BIT;
			}
			if(map[x, z-1]){
				bits |= BOTTOM_BIT;
			}
			if(x+1 < map_width && map[x+1, z-1]){
				bits |= BOTTOM_RIGHT_BIT;
			}
		}

		// Handle side bits
		if(x > 0 && x < map_width){
			if(map[x-1, z]){
				bits |= LEFT_BIT;
			}
			if(map[x+1, z]){
				bits |= RIGHT_BIT;
			}
		}





		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT | TOP_RIGHT_BIT 
			| LEFT_BIT 
			| BOTTOM_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// !!!
			// !!!
			// !!!
			return this.tiles[1,1];	
		}

		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT 
			| LEFT_BIT 
			| BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// !!!
			// !!!
			// !!!
			return this.tiles[1,1];	
		}

		if(bits == 
			( TOP_LEFT_BIT 
			| LEFT_BIT 
			| BOTTOM_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// !!!
			// !!!
			// !!!
			return this.tiles[1,1];	
		}








		// Return appropriate tile
		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT | TOP_RIGHT_BIT 
			| LEFT_BIT | RIGHT_BIT 
			| BOTTOM_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// XXX
			// XXX
			// XXX
			return this.tiles[0,3];	
		}
		if(bits ==
			( TOP_BIT | TOP_RIGHT_BIT 
			| RIGHT_BIT 
			| 0)){
			// -XX
			// --X
			// ---
			return this.tiles[0,2];
		}

		if(bits ==
			( BOTTOM_BIT | BOTTOM_RIGHT_BIT 
			| RIGHT_BIT 
			| 0)){
			// ---
			// X--
			// XX-
			return this.tiles[0,1];
		}

		if(bits ==
			( TOP_BIT | TOP_RIGHT_BIT 
			| RIGHT_BIT
			| BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// XXX
			// ---
			// ---
			return this.tiles[0,0];
		}

		if(bits == 
			( TOP_BIT | TOP_RIGHT_BIT 
			| RIGHT_BIT 
			| BOTTOM_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// XXX
			// ---
			// ---
			return this.tiles[0,0];	
		}

		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT | TOP_RIGHT_BIT 
			| RIGHT_BIT 
			| BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// XXX
			// ---
			// ---
			return this.tiles[0,0];	
		}

		if(bits ==
			( TOP_BIT | TOP_LEFT_BIT 
			| LEFT_BIT 
			| 0)){
			// ---
			// --X
			// -XX
			return this.tiles[1,3];
		}

		if(bits ==
			( TOP_LEFT_BIT | TOP_BIT | TOP_RIGHT_BIT 
			| LEFT_BIT | RIGHT_BIT
			| 0)){
			// --X
			// --X
			// --X
			return this.tiles[1,2];
		}

		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT | TOP_RIGHT_BIT 
			| LEFT_BIT | RIGHT_BIT 
			| BOTTOM_LEFT_BIT)){
			// --X
			// --X
			// --X
			return this.tiles[1,2];	
		}

		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT | TOP_RIGHT_BIT 
			| LEFT_BIT | RIGHT_BIT 
			| BOTTOM_RIGHT_BIT)){
			// --X
			// --X
			// --X
			return this.tiles[1,2];	
		}

		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT | TOP_RIGHT_BIT
			| LEFT_BIT | RIGHT_BIT 
			| BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// XXX
			// XXX
			// -XX
			return this.tiles[1,0];	
		}

		if(bits ==
			( BOTTOM_BIT | BOTTOM_LEFT_BIT 
			| LEFT_BIT 
			| 0)){
			// ---
			// X--
			// XX-
			return this.tiles[2,3];
		}

		if(bits ==
			( 0 
			| LEFT_BIT | RIGHT_BIT
			| BOTTOM_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// X--
			// X--
			// X--
			return this.tiles[2,1];
		}

		if(bits == 
			( TOP_LEFT_BIT
			| LEFT_BIT | RIGHT_BIT 
			| BOTTOM_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// X--
			// X--
			// X--
			return this.tiles[2,1];	
		}

		if(bits == 
			( TOP_RIGHT_BIT 
			| LEFT_BIT | RIGHT_BIT 
			| BOTTOM_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// X--
			// X--
			// X--
			return this.tiles[2,1];	
		}

		if(bits == 
			( TOP_BIT | TOP_RIGHT_BIT
			| LEFT_BIT | RIGHT_BIT 
			| BOTTOM_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// XXX
			// XXX
			// XX-
			return this.tiles[2,0];	
		}

		if(bits ==
			( TOP_LEFT_BIT | TOP_BIT 
			| LEFT_BIT
			| BOTTOM_LEFT_BIT | BOTTOM_BIT)){
			// ---
			// ---
			// XXX
			return this.tiles[3,3];
		}

		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT
			| LEFT_BIT
			| BOTTOM_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// ---
			// ---
			// XXX
			return this.tiles[3,3];	
		}


		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT | TOP_RIGHT_BIT
			| LEFT_BIT
			| BOTTOM_LEFT_BIT | BOTTOM_BIT)){
			// ---
			// ---
			// XXX
			return this.tiles[3,3];	
		}

		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT | TOP_RIGHT_BIT
			| LEFT_BIT | RIGHT_BIT 
			| BOTTOM_LEFT_BIT | BOTTOM_BIT)){
			// -XX
			// XXX
			// XXX
			return this.tiles[3,2];	
		}

		if(bits == 
			( TOP_LEFT_BIT | TOP_BIT
			| LEFT_BIT | RIGHT_BIT 
			| BOTTOM_LEFT_BIT | BOTTOM_BIT | BOTTOM_RIGHT_BIT)){
			// XX-
			// XXX
			// XXX
			return this.tiles[3,1];	
		}


Debug.Log("UNACCOUNTED FOR: "+bits);

		// Always return top left tile for now; eventually take 9 tiles and determine what to use
		return this.tiles[0,3];
	}
}
