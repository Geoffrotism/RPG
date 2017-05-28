using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class WorldController : MonoBehaviour {
    public static int Debugging = 1;

    public static LoadController LoadController { get; protected set; }
    public static PathfindingController PathfindingController { get; protected set; }
    public static MouseController MouseController { get; protected set; }
    public static MovementController MovementController { get; protected set; }

    World world;
    public Sprite defaultTile;
	public static WorldController Instance { get; protected set; }
	public ArrayList Structures = new ArrayList();
    public BuildMenu buildMenu;
    public static Mode currentMode = Mode.Free;
    public Pathfinder pathfinder;
    public enum Mode
	{
		Free,
        Build,
        Move
	}

	// Use this for initialization.
	public void Awake()
	{
        buildMenu = BuildMenu.CreateInstance (typeof(BuildMenu)) as BuildMenu;

		if (Instance != null)
		{
			Debug.LogError(string.Format("{0}: There should never be two world controllers.", new []{"WorldController"}));
		}

        // Order Matters?
		Instance = this;

		LoadController = LoadController.CreateInstance (typeof(LoadController)) as LoadController;
        world = new World(100, 1, 100);
        // Is pathfindingController needed?
        PathfindingController = new PathfindingController();
        MouseController = GameObject.Find("_MOUSE_").GetComponent<MouseController>();
        MovementController = GameObject.Find("_MOVEMENT_").GetComponent<MovementController>();

        


		/*
		// Used to display the object names in the list.
		foreach (string k in baseObjects.Keys) {
			Debug.LogError (k);
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
