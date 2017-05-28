using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    bool DebugMouse = false;
    GameObject selectedItem;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            WorldController.currentMode = WorldController.Mode.Free;
        }

        // Left click (I think this should be used to select things in the world.)
        if (Input.GetMouseButtonDown(0))
        {
            Camera theCamera = Camera.main;
            RaycastHit hitInfo;
            Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                Vector3 hitVector = hitInfo.transform.position;

                if (DebugMouse)
                {
                    Debug.LogWarning("You just clicked on: " + hitInfo.collider.gameObject.name);
                    Debug.LogWarning("hit: " + hitVector.x + ", " + hitVector.y + ", " + hitVector.z);
                }

                if (WorldController.currentMode == WorldController.Mode.Build)
                {
                    Vector3 newPosition = hitInfo.collider.transform.position + Vector3.Scale(hitInfo.normal, hitInfo.collider.gameObject.transform.lossyScale) / 2;
                    Vector3 normal = hitInfo.normal;
                    WorldController.Instance.buildMenu.Build(newPosition, normal);
                }
                else if (WorldController.currentMode == WorldController.Mode.Move)
                {
                    WorldController.MovementController.MoveItem(selectedItem, hitVector);
                    WorldController.currentMode = WorldController.Mode.Free;
                }
                else
                {
                    selectedItem = hitInfo.transform.gameObject;
                    WorldController.currentMode = WorldController.Mode.Move;
                }
            }
        }

        // Right click 
        if (Input.GetMouseButtonDown(1))
        {

        }

        // Move Character Forward.
        if (Input.GetKey(KeyCode.W))
        {

        }
    }
}
