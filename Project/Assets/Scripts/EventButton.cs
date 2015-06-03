using UnityEngine;
using System.Collections;

public class EventButton : MonoBehaviour
{
    [SerializeField]
	private TypeEffect effect;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public TypeEffect GetEffect()
    {
        return effect;
    }
}
