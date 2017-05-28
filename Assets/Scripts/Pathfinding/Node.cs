using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public int X;
    public int Y;
    public int Z;
    public int distanceToGoal = 0;
    public int distanceFromStart = 0;
    public int distanceTotal = 0;

    public Node(int x, int y, int z, int distanceToGoal, int distanceFromStart)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
        this.distanceToGoal = distanceToGoal;
        this.distanceFromStart = distanceFromStart;
        this.distanceTotal = distanceToGoal + distanceFromStart;
    }

    public string toString()
    {
        return "(" + this.X + ", " + this.Y + ", " + this.Z + ")";
    }
}
