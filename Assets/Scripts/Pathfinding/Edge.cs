using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour {

    public class Path_Edge<T>
    {
        // Cost to traverse this edge (i.e. cost to ENTER the tile)
        public Tile tile;

        public float cost;

    }
}
