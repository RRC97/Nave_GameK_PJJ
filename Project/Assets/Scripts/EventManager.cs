using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private bool effectActive;
    private GodEffect effect;
    private GameObject cube;
    private string TypeMod = "God";
    private float Timeday = 0;
    private bool pause = false;
    private bool advanced = false;

    [SerializeField]
    Text mode;

    [SerializeField]
    Text TimeGod;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!pause)
        {
            if (!advanced)
            {
                Timeday += Time.deltaTime;
                TimeGod.text = "Time:" + " " + Mathf.Floor(Timeday) + " " + "seg";
            }
            if (advanced)
            {
                Timeday += 0.1f;
                TimeGod.text = "Time:" + " " + Mathf.Floor(Timeday) + " " + "seg";
            }
        }

        Mode();

        if (effect == GodEffect.Thunder)
        {
            SelectedBase();
        }
    }

    private void SelectedBase()
    {
        if (TypeMod == "King")
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
    }

    private void EffectAction()
    {

    }

    public void ClickEventButton(EventButton e)
    {
        effect = e.GetEffect();
        if (TypeMod == "King")
        {
            if (effect == GodEffect.Thunder)
            {
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.renderer.material = (Material)Resources.Load("Material/Cube");
            }
        }
        if (effect == GodEffect.Change)
        {
            if (TypeMod == "King")
            {
                TypeMod = "God";
                return;
            }
            if (TypeMod == "God")
            {
                TypeMod = "King";
            }
        }
        if (TypeMod == "God")
        {
            if (effect == GodEffect.Advanced)
            {
                if (!advanced)
                {
                    advanced = true;
                    return;
                }
                if (advanced)
                {
                    advanced = false;
                }
            }
            if (effect == GodEffect.Pause)
            {
                if (pause)
                {
                    pause = false;
                    return;
                }
                if (!pause)
                {
                    pause = true;
                }
            }
        }
    }

    public void Mode()
    {
        mode.text = "Mod:" + TypeMod;
    }
}
