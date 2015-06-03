using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	Camera mainCamera;

	[SerializeField]
	KeyCode up, down, left, right;

	[SerializeField]
	EventManager em;

	private float x, y, lastX, lastY;
	private bool moveMouse;
	// Update is called once per frame
	void Update ()
	{
		
		MovimentWithKeyboard ();

		MovimentWithMouse ();
	}

	void MovimentWithKeyboard ()
	{
		if(!moveMouse)
		{
			if(Input.GetKey(up))
				transform.Translate(0, 0, 0.1f);

			if(Input.GetKey(down))
				transform.Translate(0, 0, -0.1f);

			if(Input.GetKey(left))
				transform.Translate(-0.1f, 0, 0);

			if(Input.GetKey(right))
				transform.Translate(0.1f, 0, 0);
		}
	}

	void MovimentWithMouse ()
	{
		if(moveMouse)
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			float difX = (x - lastX) / 100;
			float difY = (y - lastY) / 100;
			
			transform.Translate(-difX, 0, -difY);
			
			lastX = x;
			lastY = y;
		}
		
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.collider.gameObject.tag.Equals("Ground"))
				{
					moveMouse = true;
					x = lastX = Input.mousePosition.x;
					y = lastY = Input.mousePosition.y;
				}
			}
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			moveMouse = false;
		}
	}
}
