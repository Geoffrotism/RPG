using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    public int X;
    public int Y;
    public int Z;
    // 1 = fully passable, 0 = not passable.
    public int pathWeight; // Tile movement speed. we change this whenever the modifier is changed. ALWAYS USE THIS TO CALCULATE MOVEMENT.
    //public enum terrain; //use this later when dealing with multiple terrain types, for now we assume default terrain.

    public Area area {get; protected set;}

    public Tile()
    {

    }
    public Tile(int x, int y, int z, int pathWeight = 1)
    {
        GameObject tile_go = new GameObject();
        tile_go = MonoBehaviour.Instantiate(World.baseObjects["Ground"]) as GameObject;
        this.X = x;
        this.Y = y;
        this.Z = z;
        this.pathWeight = pathWeight;  

        
        tile_go.name = "Tile_" + x + "_" + y + "_" + z;
        
        tile_go.transform.position = new Vector3(x, y, z);
    }

    public void setArea(Area inputArea)
    {
        area = inputArea;
    }
    /// <summary>
    /// Get the adjacent tile in the direction given.
    /// </summary>
    /// <param name="ii">Use numbers 1-4, starting North and going clockwise.</param>
    /// <returns>The tile adjacent to this one.</returns>
    public Tile getAdjacentTile(int ii)
    {
        switch (ii)
        {
            case 1:
                return North();
            case 2:
                return East();
            case 3:
                return South();
            case 4:
                return West();
            default:
                return North();
        }
    }

    public Tile North()
    {
        return World.Instance.GetTileAt(this.X, this.Y, this.Z + 1) ?? null;
    }
    public Tile South()
    {
        return World.Instance.GetTileAt(this.X, this.Y, this.Z - 1) ?? null;
    }
     public Tile East()
    {
        return World.Instance.GetTileAt(this.X + 1, this.Y, this.Z) ?? null;
    }
     public Tile West()
    {
        return World.Instance.GetTileAt(this.X - 1, this.Y, this.Z + 1) ?? null;
    }

    public List<Tile> getAdjTiles()
    {
        List<Tile> list = new List<Tile>(4);
        list.Add(this.North());
        list.Add(this.South());
        list.Add(this.East());
        list.Add(this.West());
        return list;
    }

    public string toString()
    {
        return "(" + this.X + ", " + this.Y + ", " + this.Z + ")";
    }
}
