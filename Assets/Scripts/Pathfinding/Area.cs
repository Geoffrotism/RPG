using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collection of tiles in an area. Used to prevent pathfinding recalculations over the whole map.
/// Limited to 1 level (y axis) for now.
/// </summary>
public class Area {

    List<Tile> tiles = new List<Tile>();                //List of all tiles in the area.
    List<Tile> connectionNodes = new List<Tile>();      //List of all optimal nodes that connect to other Area.>
    int level;

    public Area(List<Tile> inTiles, int inLevel)
    {
        tiles = inTiles;
        level = inLevel;
        calculateNodes();
    }

    public void calculateNodes()
    {

    }
}
