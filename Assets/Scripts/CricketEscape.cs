using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CricketEscape : MonoBehaviour
{

    void OnTriggerEnter(Collider c)
    {
        gameObject.GetComponentInParent<WanderingAI_escape>().Escape(c);
    }

}