using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private bool effectActive;
    private TypeEffect effect;
    private InstanceLocal cube;
    private string TypeMod = "God";
    private float Timeday = 0;
    private bool pause = false;
    private bool advanced = false;
    private int cubes, fertility;
	private Vector3 oldPos;
	private float timeFertility;

	[SerializeField]
	GameObject effectGod, painelGod, painelKing, effectKing;

    [SerializeField]
	Text mode, TimeGod, countCube, countFertilty;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
	{
            
        if (advanced)
        {
			Time.timeScale = 2;
        }
		else
		{
			Time.timeScale = 1;
		}
		
		if (pause)
		{
			Time.timeScale = 0;
		}

		Timeday += Time.deltaTime;


		if(timeFertility > 0)
			timeFertility -= Time.deltaTime;

		cubes = GameObject.FindGameObjectsWithTag("Constrution").Length;

		TextManager();

        if (effect == TypeEffect.Farm)
        {
            SelectedBase();
        }

		if(effect == TypeEffect.Fertility)
		{
			if(timeFertility <= 0)
			{
				EffectBase ();
			}
		}

		if (effect == TypeEffect.Remove)
		{
			RemoveBase();
		}
	}

	private void EffectBase()
	{
		if (TypeMod == "God")
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Input.GetMouseButtonDown(0))
			{
				effect = TypeEffect.None;
				
				if(Physics.Raycast(ray, out hit))
				{
					if(hit.collider != null && hit.collider.gameObject.tag.Equals("Constrution"))
					{
						GameObject go = (GameObject)Instantiate(effectGod);
						go.transform.position = hit.collider.transform.position;
						go.transform.parent = hit.collider.transform;
						fertility += 5;
						timeFertility = 10;
					}
				}
			}
		}
	}

	public bool InEffect()
	{
		if(effect != TypeEffect.None)
			return true;

		return false;
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
					if(cube.IsCollided())
                    {
						cube.transform.position = oldPos;
					}

					oldPos = cube.transform.position;
					cube.rigidbody.MovePosition(hit.point);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                effect = TypeEffect.None;
				
				if(cube.IsCollided())
				{
					Destroy(cube.gameObject);
				}

				cube = null;
			}
        }
    }

	private void RemoveBase()
	{
		if (TypeMod == "King")
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Input.GetMouseButtonDown(0))
			{
				effect = TypeEffect.None;

				if(Physics.Raycast(ray, out hit))
				{
                    if (hit.collider != null && hit.collider.gameObject.tag.Equals("Constrution"))
                    {
                        GameObject retry = (GameObject)Instantiate(effectKing);
                        retry.transform.position = hit.collider.transform.position;
                        //retry.transform.parent = hit.collider.transform;
                        Destroy(hit.collider.gameObject);
                    }
				}
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
            if (effect == TypeEffect.Farm)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube = go.AddComponent<InstanceLocal> ();
                cube.renderer.material = (Material)Resources.Load("Material/Cube");
            }
        }
        if (effect == TypeEffect.Change)
        {
            if (TypeMod == "King")
            {
                TypeMod = "God";
				painelGod.SetActive(true);
				painelKing.SetActive(false);
            }
            else if (TypeMod == "God")
            {
				TypeMod = "King";
				painelGod.SetActive(false);
				painelKing.SetActive(true);
			}

			effect = TypeEffect.None;
		}
        if (TypeMod == "God")
        {
            if (effect == TypeEffect.Advanced)
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
				
				effect = TypeEffect.None;
			}
            if (effect == TypeEffect.Pause)
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
				
				effect = TypeEffect.None;
			}
        }
    }

    public void TextManager()
    {
		mode.text = "Mod: " + TypeMod;

		int hours = (Mathf.FloorToInt(Timeday) / 60) % 60;
		int minute = Mathf.FloorToInt(Timeday) % 60;
		TimeGod.text = "Time:" + " " + string.Format("{0:00}:{1:00}", hours, minute);

		countCube.text = "Construtions: " + cubes;
		countFertilty.text = "Fertility: " + fertility;
    }
}
