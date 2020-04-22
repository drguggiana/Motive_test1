using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class AdjustNormal : MonoBehaviour
{

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Wall" || c.gameObject.tag == "Floor")
        {
            gameObject.GetComponentInParent<WanderingAI_escape>().SetNormal(c);
        }

    }

}
