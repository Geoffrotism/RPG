using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingController {

    List<Area> areas = new List<Area>();
    const int edgeLength = 10;

    public PathfindingController()
    {
        //List<Tile> tiles = new List<Tile>();
        //foreach (Tile tile in World.Instance.tiles)
        //{
        //    tiles.Add(tile);
        //}
        //createAreas(tiles);        
    }

    /// <summary>
    /// Not used yet.
    /// </summary>
    /// <param name="tiles"></param>
    private void createAreas(List<Tile> tiles)
    {
        int ii = 0;
        int level = 0;
        List<Tile> areaTiles = new List<Tile>();

        while (ii < 100)
        {
            if (tiles[ii].Y != level)
            {
                level++;
                return;
            }
            areaTiles.Add(tiles[ii]);
            ii++;
        }

        tiles.RemoveRange(0, ii);
        Area area = new Area(areaTiles, level);
        areas.Add(area);
        if (tiles.Count != 0) {createAreas(tiles); }
    }

    //public List<Tile> getPath(Tile origin, Tile destination)
    //{
    //    Pathfinder pathfinder = new Pathfinder();
    //    pathfinder.getPath(origin, destination);
    //}

}
