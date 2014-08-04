using UnityEngine;

public class TileMeshData {

	private int[,] elevation_map;
	private bool[,] tiles_grass;
	private Tile[,] tiles_rendered;
	private int 
		tiles_x = 30,
		tiles_z = 30;

	public TileMeshData(MagicTileSet magic_tile_set){
		LoadHardcodedElevationMap();
		LoadHardcodedGrass();
		RenderTiles(magic_tile_set);
	}

	private void LoadHardcodedGrass(){
		this.tiles_grass = new bool[this.tiles_x, this.tiles_z];

		for(int z=0; z < this.tiles_z; z++){
			for(int x=0; x < this.tiles_x; x++){
				this.tiles_grass[x,z] = false;
			}
		}

		for(int z=0; z < 2; z++){
			for(int x=0; x < 2; x++){
				this.tiles_grass[x,z] = true;
			}
		}

	}

	private void LoadHardcodedElevationMap(){
		this.elevation_map = new int[this.tiles_x, this.tiles_z];

		for(int z=0; z < this.tiles_z; z++){
			for(int x=0; x < this.tiles_x; x++){
				this.elevation_map[x,z] = 0;
			}
		}

		for(int z=0; z < 5; z++){
			for(int x=0; x < this.tiles_x; x++){
				this.elevation_map[x,z] = 3;
			}
		}
	}

	private void RenderTiles(MagicTileSet magic_tile_set){
		this.tiles_rendered = new Tile[this.tiles_x, this.tiles_z];
		Tile blank = new Tile(Color.red);

		for(int z=0; z < this.tiles_z; z++){
			for(int x=0; x < this.tiles_x; x++){
				if(this.tiles_grass[x,z]){
					this.tiles_rendered[x,z] = magic_tile_set.GetTile();
				} else {
					this.tiles_rendered[x,z] = blank;
				}
			}
		}
	}

}
