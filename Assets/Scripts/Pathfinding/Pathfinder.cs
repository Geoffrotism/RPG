using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Example:
/// Queue<Tile> tilePath = new Pathfinder(World.Instance.GetTileAt(0, 0, 0), World.Instance.GetTileAt(55, 0, 23)).getPath();
/// </summary>
public class Pathfinder
{
    bool debugPath = false;

    List<Node> openNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();
    GameObject go;
    Node destination;
    Node origin;
    Node lastNode;
    int lowestCost = 0;
    private Queue<Tile> path; // This will contain the built path.
    Dictionary<Node, Node> came_From = new Dictionary<Node, Node>();

    /// Contains the time it took to find the path
    public float Duration
    {
        get; private set;
    }

    public Pathfinder(Tile origin, Tile destination, GameObject goIn)
    {
        if (origin == null || destination == null)
        {
            return;
        }
        this.lowestCost = getDistance(origin, destination);
        this.destination = new Node(destination.X, destination.Y, destination.Z, 0, getDistance(origin, destination));
        this.origin = new Node(origin.X, origin.Y, origin.Z, getDistance(origin, destination), 0);
        PathFinderHelper.AddSorted(openNodes, new Node(origin.X, origin.Y, origin.Z, getDistance(origin, destination), 0));
        go = goIn;
    }

    public Queue<Tile> getPath()
    {
        float startTime = Time.realtimeSinceStartup;
        //int cameFromDir = 0;

        while (openNodes.Count > 0 && (Time.realtimeSinceStartup - startTime) < 10)
        {
            if (openNodes.Count <= 0)
            {
                if (debugPath)
                {
                    Debug.LogWarning("NO PATH?!?!?");
                }
                return null;
            }

            Node current = openNodes[0];

            if (isAtGoal(current))
            {
                Duration = (Time.realtimeSinceStartup - startTime);
                Reconstruct_path(came_From, current);

                if (debugPath)
                {
                    Debug.LogWarning("WE GOT A PATH");
                    Debug.LogWarning("Time taken: " + Duration);
                    Debug.LogWarning("Closed Nodes: " + closedNodes.Count);
                    Debug.LogWarning("Open Nodes: " + openNodes.Count);
                    Debug.LogWarning("Total Nodes in path: " + path.Count);  
                }

                return path;
            }

            openNodes.RemoveAt(0);
            closedNodes.Add(current);

            for (int ii = 1; ii <= 4; ii++)
            {
                // Skip if we came from that direction. Do we want this?
                //if (cameFromDir == ii) { continue; }

                Tile temp = World.Instance.GetTileAt(current.X, current.Y, current.Z).getAdjacentTile(ii);
                if (temp == null) { continue; }

                // If the node is already in the list of open nodes, ignore.
                if (openNodes.Where(x => x.X == temp.X && x.Y == temp.Y && x.Z == temp.Z).Count() != 0)
                {
                    if (debugPath)
                    {
                        Debug.LogWarning("Already in open Nodes:" + temp.toString());
                    }
                    continue;
                }

                // TODO: get pathweight out of here and implement removing nodes that shouldnt be in the list because they are untraversable.
                if (closedNodes.Where(x => x.X == temp.X && x.Y == temp.Y && x.Z == temp.Z).Count() != 0 || temp.pathWeight == 0)
                {
                    if (debugPath)
                    {
                        Debug.LogWarning("Already in closedNodes: " + temp.toString());
                    }
                    continue;
                }

                Node test = new Node(temp.X, temp.Y, temp.Z, getDistance(temp, destination), getDistance(temp, origin));

                if (test.distanceTotal > current.distanceTotal)
                {
                    if (debugPath)
                    {
                        Debug.LogWarning("Total Distance of: " + current.toString() + " is greater than: " + test.toString());
                    }
                    continue;
                }

                came_From[test] = current;
                PathFinderHelper.AddSorted(openNodes, test);

                if (debugPath)
                {
                    Debug.LogWarning("Number of Open Nodes: " + openNodes.Count);
                    Debug.LogWarning("Just Added:" + test.toString());
                }
            }
        }

        if (debugPath)
        {
            foreach (KeyValuePair<Node, Node> tile in came_From)
            {
                Debug.LogWarning("To: " + tile.Key.toString() + "From: " + tile.Value.toString());
            }
            foreach (Tile tile in path)
            {
                Debug.LogWarning(tile.X + ", " + tile.Y + ", " + tile.Z);
            }
        }
        return null;
    }

    public bool isAtGoal(Node current)
    {
        return getDistance(current, destination) == 0;
    }

    public int getDistance(Tile a, Tile b)
    {
        return (Mathf.Abs(a.X - b.X) +
                        Mathf.Abs(a.Z - b.Z));

    }
    public int getDistance(Node a, Node b)
    {
        return (Mathf.Abs(a.X - b.X) +
                        Mathf.Abs(a.Z - b.Z));
    }
    public int getDistance(Tile a, Node b)
    {
        return (Mathf.Abs(a.X - b.X) +
                        Mathf.Abs(a.Z - b.Z));
    }

    private void Reconstruct_path(Dictionary<Node, Node> came_From, Node current)
    {
        // So at this point, current IS the goal.
        // So what we want to do is walk backwards through the Came_From
        // map, until we reach the "end" of that map...which will be
        // our starting node!
        Queue<Tile> total_path = new Queue<Tile>();
        total_path.Enqueue(World.Instance.GetTileAt(current.X, current.Y, current.Z)); // This "final" step is the path is the goal!

        while (came_From.ContainsKey(current))
        {
            /*    Came_From is a map, where the
            *    key => value relation is real saying
            *    some_node => we_got_there_from_this_node
            */

            current = came_From[current];
            total_path.Enqueue(World.Instance.GetTileAt(current.X, current.Y, current.Z));
        }

        // At this point, total_path is a queue that is running
        // backwards from the END tile to the START tile, so let's reverse it.
        path = new Queue<Tile>(total_path.Reverse());
    }
}

/// <summary>
/// Helps with sorting the list of open nodes.<see cref="World"/> 
/// </summary>
public static class PathFinderHelper
{
    public static void AddSorted(this List<Node> @this, Node item)
    {
        if (@this.Count == 0)
        {
            @this.Add(item);
            return;
        }
        if (@this[@this.Count - 1].distanceTotal.CompareTo(item.distanceTotal) <= 0)
        {
            @this.Add(item);
            return;
        }
        if (@this[0].distanceTotal.CompareTo(item.distanceTotal) >= 0)
        {
            @this.Insert(0, item);
            return;
        }
        int index = @this.BinarySearch(item, new NodeComparer());
        if (index < 0)
            index = ~index;
        @this.Insert(index, item);
    }
}