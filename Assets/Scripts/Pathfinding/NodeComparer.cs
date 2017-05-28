using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeComparer : IComparer<Node> {

    public int Compare(Node x, Node y)
    {
        if (x == null) return -1;
        if (y == null) return 1;

        if (x.distanceTotal == y.distanceTotal) return 0;

        return x.distanceTotal > y.distanceTotal ? 1 : -1;
    }
}
