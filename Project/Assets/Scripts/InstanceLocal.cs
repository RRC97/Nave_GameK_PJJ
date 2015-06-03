using UnityEngine;
using System.Collections;

public class InstanceLocal : MonoBehaviour
{
	private bool collided;
	// Use this for initialization
	void Awake ()
	{
		tag = "Constrution";
		gameObject.AddComponent<Rigidbody>();
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionStay(Collision c)
	{
		if(c.gameObject.tag.Equals(tag))
		{
			collided = true;
		}
	}

	void OnCollisionEnter(Collision c)
	{
		if(c.gameObject.tag.Equals(tag))
		{
			collided = true;
		}
	}

	void OnCollisionExit(Collision c)
	{
		if(c.gameObject.tag.Equals(tag))
		{
			collided = false;
		}
	}

	public bool IsCollided ()
	{
		return collided;
	}
}
