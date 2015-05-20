using UnityEngine;
using System.Collections;

public class EventButton : MonoBehaviour
{
    [SerializeField]
    private GodEffect effect;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public GodEffect GetEffect()
    {
        return effect;
    }
}
