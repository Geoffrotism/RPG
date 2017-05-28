using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Handles the movement in the world. Should this just be merged into <see cref="PathfindingController"/>?
/// </summary>
public class MovementController : MonoBehaviour
{
    public static Dictionary<string, Coroutine> currentPaths = new Dictionary<string, Coroutine>();

    /// <summary>
    /// Moves the GO to the desired location, one tile at a time.
    /// </summary>
    /// <param name="selectedItem"></param>
    /// <param name="toVector"></param>
    public void MoveItem(GameObject selectedItem, Vector3 toVector)
    {
        Vector3 fromVector = selectedItem.transform.position;

        Pathfinder pathfinder = new Pathfinder(World.Instance.GetTileAt(Mathf.FloorToInt(fromVector.x), Mathf.FloorToInt(fromVector.y), Mathf.FloorToInt(fromVector.z)),
            World.Instance.GetTileAt(Mathf.FloorToInt(toVector.x), Mathf.FloorToInt(toVector.y), Mathf.FloorToInt(toVector.z)),
            selectedItem);

        Queue<Tile> testPath = pathfinder.getPath();
        if (currentPaths.ContainsKey(selectedItem.name))
        {
            if (currentPaths[selectedItem.name] != null)
            {
                StopCoroutine(currentPaths[selectedItem.name]);
            }
            currentPaths.Remove(selectedItem.name);
        }

        Coroutine co = StartCoroutine(MoveToPosition(selectedItem, testPath, .8f));
        currentPaths.Add(selectedItem.name, co);
    }

    /// <summary>
    /// Gets called by <see cref="MoveItem(GameObject, Vector3)"/> to move the item to each tile in the Queue, one tile at a time. (Do not mess with this unless we really need to.)
    /// </summary>
    /// <param name="selectedItem"></param>
    /// <param name="testPath"></param>
    /// <param name="timeToMove"></param>
    /// <returns></returns>
    public IEnumerator MoveToPosition(GameObject selectedItem, Queue<Tile> testPath, float timeToMove)
    {
        // The Queue is empty so we have finished the movement!
        if (testPath.Count == 0)
        {
            currentPaths.Remove(selectedItem.name);
            yield return null;
        }

        var currentPos = selectedItem.transform.position;
        Tile x = testPath.Dequeue();
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            selectedItem.transform.position = Vector3.Lerp(currentPos, new Vector3(x.X, x.Y, x.Z), t);
            yield return null;
        }

        // Recursively call this method.
        Coroutine co = StartCoroutine(MoveToPosition(selectedItem, testPath, timeToMove));
        currentPaths[selectedItem.name] = co;
    }
}
