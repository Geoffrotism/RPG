// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    //
    // VARIABLES
    //
	public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
	public float panSpeed = 4.0f;		// Speed of the camera when being panned
	public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth
	
	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isPanning;		// Is the camera being panned?
	private bool isRotating;	// Is the camera being rotated?
	private bool isZooming;		// Is the camera zooming?
	
	//
	// UPDATE
	//
	
	void Update () 
	{
        //if (Camera.main.transform.position.y < 15 
        //    || Camera.main.transform.position.x < 0
        //    || Camera.main.transform.position.x > 100
        //    || Camera.main.transform.position.z < 0
        //    || Camera.main.transform.position.z > 100)
        //{
        //    return;
        //}

		// Get the left mouse button
		if(Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}
		
		// Get the right mouse button
		if(Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isPanning = true;
		}
		
		// Get the middle mouse button
		var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
         {
            if (Camera.main.transform.position.y > 15) { zoom(1); }
         }
         else if (d < 0f)
         {
            zoom(-1);
         }
		
		// Disable movements on button release
		if (!Input.GetMouseButton(2)) isRotating=false;
		if (!Input.GetMouseButton(1)) isPanning=false;
		
		// Rotate camera along X and Y axis
		if (isRotating)
		{
	        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
                if ((Camera.main.transform.rotation.x < .6 && pos.y > 0) || 
                (Camera.main.transform.rotation.x > .05 && pos.y < 0))
                {
                    transform.RotateAround(transform.position, transform.right, pos.y * turnSpeed);
                Debug.Log("herer");

                }
                //Debug.Log(Camera.main.transform.rotation.x);
                transform.RotateAround(transform.position, Vector3.up, -pos.x * turnSpeed);
            
		}
		
		// Move the camera on it's XY plane
		if (isPanning)
		{
	        	Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

	        	Vector3 move = new Vector3(-pos.x * panSpeed, -pos.y * panSpeed, 0);
	        	transform.Translate(move, Space.Self);
		}
	}

    // Move the camera linearly along Z axis
    private void zoom(int input)
    {     
            Vector3 move = input * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
    }
}