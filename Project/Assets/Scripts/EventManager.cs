using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    private bool effectActive;
    private GodEffect effect;
    private GameObject cube;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (effect != GodEffect.None)
        {
            SelectedBase();
        }
	}

    private void SelectedBase()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Ground"))
            {
                cube.transform.position = hit.point;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            effect = GodEffect.None;
            cube = null;
        }
    }

    private void EffectAction()
    {

    }

    public void ClickEventButton(EventButton e)
    {
        effect = e.GetEffect();
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.renderer.material = (Material)Resources.Load("Material/Cube");
    }
}
