using UnityEngine;

public class TileMeshData {

	private float[,] elevation_map;
	private bool[,] tiles_grass;
	private Tile[,] tiles_rendered;
	private int 
		tiles_x = 30,
		tiles_z = 30,
		tile_resolution = 16; // one tile is 16 pixels; should be stored elsewhere
	public float
		tile_size = 1.0f; // Size in unity units

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

		// Bottom left patch
		for(int z=0; z < 4; z++){
			for(int x=0; x < 4; x++){
				this.tiles_grass[x,z] = true;
			}
		}

		// 1x1 square
		for(int z=2; z < 4; z++){
			for(int x=6; x < 8; x++){
				this.tiles_grass[x,z] = true;
			}
		}

		// 2x2 square
		for(int z=6;z<10;z++){
			for(int x=2;x<6;x++){
				this.tiles_grass[x,z] = true;	
			}
		}

		// 4x4 square with whole in middle
		for(int z=12; z<20;z++){
			for(int x = 2; x<4; x++){
				this.tiles_grass[x,z] = true;	
			}
			for(int x = 8; x<10; x++){
				this.tiles_grass[x,z] = true;	
			}
		}
		for(int x=2; x<10; x++){
			for(int z = 12; z<14; z++){
				this.tiles_grass[x,z] = true;	
			}
			for(int z = 18; z<20; z++){
				this.tiles_grass[x,z] = true;	
			}
		}

		// Plus sign
		for(int z=6;z<10;z++){
			for(int x=12;x<20;x++){
				this.tiles_grass[x,z] = true;	
			}
		}
		for(int z=4;z<12;z++){
			for(int x=14;x<18;x++){
				this.tiles_grass[x,z] = true;	
			}
		}

		// X
		for(int z=14;z<16;z++){
			for(int x=12;x<14;x++){
				this.tiles_grass[x,z] = true;	
			}
		}
		for(int z=18;z<20;z++){
			for(int x=12;x<14;x++){
				this.tiles_grass[x,z] = true;	
			}
		}
		for(int z=14;z<16;z++){
			for(int x=16;x<18;x++){
				this.tiles_grass[x,z] = true;	
			}
		}
		for(int z=18;z<20;z++){
			for(int x=16;x<18;x++){
				this.tiles_grass[x,z] = true;	
			}
		}
		for(int z=16;z<18;z++){
			for(int x=14;x<16;x++){
				this.tiles_grass[x,z] = true;	
			}
		}

	}

	private void LoadHardcodedElevationMap(){
		this.elevation_map = new float[this.tiles_x + 1, this.tiles_z + 1]; // +1 BECAUSE VERTICES!!!

		for(int z=0; z < this.tiles_z; z++){
			for(int x=0; x < this.tiles_x; x++){
				this.elevation_map[x,z] = 0;
			}
		}

		for(int z=0; z < 5; z++){
			for(int x=0; x < this.tiles_x; x++){
				this.elevation_map[x,z] = 2f;
			}
		}

		for(int z=5; z < 6; z++){
			for(int x=0; x < this.tiles_x; x++){
				this.elevation_map[x,z] = 0.5f;
			}
		}
	}

	private void RenderTiles(MagicTileSet magic_tile_set){
		this.tiles_rendered = new Tile[this.tiles_x, this.tiles_z];
		Tile blank = new Tile(Color.red);

		for(uint z=0; z < this.tiles_z; z++){
			for(uint x=0; x < this.tiles_x; x++){
				if(this.tiles_grass[x,z]){
					this.tiles_rendered[x,z] = magic_tile_set.GetTile(this.tiles_grass, x, z);
				} else {
					this.tiles_rendered[x,z] = blank;
				}
			}
		}
	}

	public void BuildMesh(MeshFilter mesh_filter, MeshRenderer mesh_renderer, MeshCollider mesh_collider){
		/* Map
		 * 0----1----2----3
		 * |\   |\   |\   |
		 * | \  | \  | \  |
		 * |  \ |  \ |  \ |
		 * |   \|   \|   \|
		 * 4----5----6----7
		 * |\   |\   |\   |
		 * | \  | \  | \  |
		 * |  \ |  \ |  \ |
		 * |   \|   \|   \|
		 * 8----9---10---11
		 */
		int num_tiles = this.tiles_x * this.tiles_z;
		int num_triangles = num_tiles * 2;

		int vsize_x = this.tiles_x + 1; // Number of horizontal vertices. To draw one tile wide, you need two vertices - so we add one.
		int vsize_z = this.tiles_z + 1;
		int num_vertices = vsize_x * vsize_z;

		// Generate the mesh data
		Vector3[] vertices = new Vector3[num_vertices];
		Vector3[] normals = new Vector3[num_vertices];   // one per vertex
		Vector2[] uv = new Vector2[num_vertices];        // one per vertex; value from 0 to 1. Represents where in texture to apply (just one texture for entire map right now). 0 = left most, 1 = right most.

		int[] triangles = new int[num_triangles * 3];     // 2 triangles * 3		 vertices

		// Populate vertices, normals, and uv
		int x, z, index;
		for (z = 0; z < vsize_z; z++) {
			for (x = 0; x < vsize_x; x++) {
				index = z * vsize_x + x;

				vertices[index] = new Vector3( 
					x * this.tile_size,
					this.elevation_map[x, z], //Random.Range (-1f, 1f),// <-- range creates a neat choppy water effect
					z * this.tile_size);
				normals[index] = Vector3.up;
				// NOTE! In part 6, we changed from vsize_x to tiles_x and vsize_z to size_z, because in final loop, if z=100, then vsize_z = 101.
				uv[index] = new Vector2((float)x / this.tiles_x, (float)z / this.tiles_z);
			}
		}

		// Populate triangles
		for (z = 0; z < this.tiles_z; z++) {
			for (x = 0; x < this.tiles_x; x++) {
				/* First tile
				 * -----------------------------
				 * First triangle
				 * triangles[0] = 0;
				 * triangles[1] = vsize_x + 0;
				 * triangles[2] = vsize_x + 1;
				 * 
				 * Second triangle
				 * triangles[3] = 0;
				 * triangles[4] = vsize_x + 1;
				 * triangles[5] = 1;
				 */
				int square_index = z * this.tiles_x + x;
				int triangle_offset = square_index * 6;

				// First triangle
				triangles[triangle_offset + 0] = z * vsize_x + x + 0;
				triangles[triangle_offset + 1] = z * vsize_x + x + vsize_x + 0;
				triangles[triangle_offset + 2] = z * vsize_x + x + vsize_x + 1;

				// Second triangle
				triangles[triangle_offset + 3] = z * vsize_x + x + 0;
				triangles[triangle_offset + 4] = z * vsize_x + x + vsize_x + 1;
				triangles[triangle_offset + 5] = z * vsize_x + x + 1;
			}
		}

		// Create a new mesh and populate with the data
		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		
		mesh_filter.mesh = mesh;
		mesh_collider.sharedMesh = mesh;

		BuildTexture(mesh_renderer);
	}

	private void BuildTexture(MeshRenderer mesh_renderer){
		int 
			texture_width = this.tiles_x * this.tile_resolution,
			texture_height = this.tiles_z * this.tile_resolution;
		Texture2D texture = new Texture2D(texture_width, texture_height);

		for(int z=0; z<this.tiles_z; z++){
			for(int x=0; x<this.tiles_x; x++){
				Color[] p = this.tiles_rendered[z,x].Pixels ();
				texture.SetPixels(x*tile_resolution, z*tile_resolution, tile_resolution, tile_resolution, p);
			}
		}

		// Do not blend between pixels
		// Normally, you probably don't want to do this
		texture.filterMode = FilterMode.Point;
		// In Part 7 we temporarily change this to bilinear so it's less harshly pixelated
		//    texture.filterMode = FilterMode.Bilinear;

		texture.wrapMode = TextureWrapMode.Clamp;

		// Apply pixel changes to texture
		texture.Apply();

		// Put texture onto tilemap
		mesh_renderer.sharedMaterials[0].mainTexture = texture;

	}
}
