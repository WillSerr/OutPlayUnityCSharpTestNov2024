using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Explode>().Detonate();
    }
}
