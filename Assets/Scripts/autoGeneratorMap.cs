using UnityEngine;
using UnityEngine.Tilemaps;

public class autoGeneratorMap : MonoBehaviour
{
	public int width = 10;
	public int height = 10;
	public Tilemap collisionTilemap; // Chứa cả tường và chướng ngại vật
	public Tilemap groundTilemap;    // Chứa mặt đất
	public TileBase wallTile;
	public TileBase groundTile;
	public TileBase obstacleTile;

	void Start()
	{
		GenerateMap();
	}

	void GenerateMap()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Vector3Int tilePosition = new Vector3Int(x, y, 0);

				if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
				{
					// Tạo tường (Wall) trong Tilemap_Collision
					collisionTilemap.SetTile(tilePosition, wallTile);
				}
				else
				{
					// 20% tạo chướng ngại vật, 80% tạo mặt đất
					if (Random.value > 0.8f)
					{
						collisionTilemap.SetTile(tilePosition, obstacleTile);

						TilemapRenderer tilemapRenderer = collisionTilemap.GetComponent<TilemapRenderer>();
						tilemapRenderer.sortingLayerName = "Grass"; 

					}
					else
					{
						groundTilemap.SetTile(tilePosition, groundTile);

						TilemapRenderer tilemapRenderer2 = groundTilemap.GetComponent<TilemapRenderer>();
						tilemapRenderer2.sortingLayerName = "Group"; 
					}
				}
			}
		}
	}
}
