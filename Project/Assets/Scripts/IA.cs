using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour 
{
    Vector3 posIA1, dif;
    [SerializeField]
    Transform constrution, posConstrution;

    void Start()
    {
        dif = new Vector3(0, 0.7f);
        posIA1 = constrution.position + dif;
    }
	void FixedUpdate () 
    {
        posIA1 = Vector3.Lerp(posIA1, posConstrution.position + dif, 0.1f);
        if (posIA1 == posConstrution.position + dif)
        {
            Transform temp = posConstrution;
            posConstrution = constrution;
            constrution = temp;
        }

        transform.position = posIA1;
	}
}
