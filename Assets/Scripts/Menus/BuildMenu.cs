using UnityEngine;
using AssemblyCSharp;
using UnityEngine.UI;
using System;

public class BuildMenu: ScriptableObject
{
    public Button BuildObject;
    public Button BuildType;

    // type is of struct_type
    private Struct_Type type = Struct_Type.wood;
    
	//public void Build(Button btn)
    public void Build(Vector3 position, Vector3 normal)
	{
		
//		try {
//			type = (Struct_Type)Enum.Parse(typeof(Struct_Type), btn.tag.ToString(), true);
//		}
//		catch (Exception ex) {
//			Debug.LogError(btn.tag + "is not a correct enum type for: Struct_Type");
//			return;
//		}

		string typeString = "";

		switch (BuildType.name) {
		case "Wood Button":
			typeString = "Wood_";
			type = Struct_Type.wood;
			break;  
		case "Iron Button":
			typeString = "Iron_";
			type = Struct_Type.iron;
			break;
		case "Steel Button":
			typeString = "Steel_";
			type = Struct_Type.steel;
			break;
		}
        Debug.LogError(BuildObject);
        switch (BuildObject.name)
        {
            case "Build_Wall_Button":
                buildWall(typeString, position, normal);
            break;
            case "Build_Door_Button":
                buildDoor(typeString, position, normal);
            break;
            case "Build_Floor_Button":

            break;

        }
	}
    private void buildWall(string typeString, Vector3 position, Vector3 normal)
    {
        GameObject wall = Instantiate(World.baseObjects[typeString + "Wall"], new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
        position = position + Vector3.Scale(normal, wall.transform.lossyScale)/2;

        wall.name = wall.name.Substring(0, wall.name.Length - 7);
        wall.AddComponent(Type.GetType("Wall"));
        wall.GetComponent<Wall>().Init(type, position.x, position.y, position.z);
        Debug.LogError(wall.GetComponent<Wall>().CurrentHealth);
        WorldController.Instance.Structures.Add(wall);
    }

      private void buildDoor(string typeString, Vector3 position, Vector3 normal)
    {

        GameObject door = Instantiate(World.baseObjects[typeString + "Door"], new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
        position = position + Vector3.Scale(normal, door.transform.lossyScale)/2;

        door.name = door.name.Substring(0, door.name.Length - 7);
        door.AddComponent(Type.GetType("Door"));
        door.GetComponent<Door>().Init(type, position.x, position.y, position.z);
        Debug.LogError(door.GetComponent<Door>().CurrentHealth);
        WorldController.Instance.Structures.Add(door);
    }

    private Vector3 helper(Vector3 input)
    {
        if (input.x > 1)
        {
            input.x /= 2;
        }

        if (input.y > 1)
        {
            input.y /= 2;
        }

        if (input.z > 1)
        {
            input.z /= 2;
        }


       return input;
    }
}

