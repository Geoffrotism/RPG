using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The "Physical" representation of the world.
/// </summary>
public class World
{
    private int worldWidth; // x - direction
    private int worldHeight; // Verticality - y direction
    private int worldDepth;  // z - direction

    // A three-dimensional array to hold our tile data.
    public Tile[,,] tiles;

    public static World Instance { get; protected set; }

    public int WorldWidth {get; protected set;}
    public int WorldHeight {get; protected set;}
    public int WorldDepth {get; protected set;}
   
    public static Dictionary<string, GameObject> baseObjects = new Dictionary<string, GameObject>();

    /// <summary>
    /// Initializes a new instance of the <see cref="World"/> class.
    /// </summary>
    /// <param name="width">Width (x) in tiles.</param>
    /// <param name="height">Height (y) in tiles.</param>
    /// <param name="depth">Depth (z) in tiles.</param>
    public World(int width, int height, int depth)
    {
        Instance = this;
        WorldWidth = width;
        worldHeight = height;
        worldDepth = depth;

        baseObjects = WorldController.LoadController.AllObjects;

        tiles = new Tile[WorldWidth, worldHeight, worldDepth];
        createTiles(); 
    }

    /// <summary>
    /// Creates the tiles for the world.
    /// </summary>
    /// <note>
    /// We should only be creating new tiles at game load for now (we can create new tiles when building in 3D later)
    /// </note>
    private void createTiles()
    {
        for (int x = 0; x < WorldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                for (int z = 0; z < worldDepth; z++)
                {
                    tiles[x, y, z] = new Tile(x, y, z);
                }
            }
        }
    }

    /// <summary>
    /// Gets the tile data at coords {x, y, z}.
    /// </summary>
    /// <returns>The <see cref="Tile"/> or null if called with invalid arguments.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public Tile GetTileAt(int x, int y, int z)
    {
        if (x >= WorldWidth || x < 0 || y >= worldHeight || y < 0 || z >= worldDepth || z < 0)
        {
            return null;
        }

        return tiles[x, y, z];
    }
}
