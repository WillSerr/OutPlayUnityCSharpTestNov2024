using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject explosionObject = null;
    private bool dead = false;

    public void Detonate()
    {
        if (explosionObject != null && !dead)
        {
            dead = true;
            Instantiate(explosionObject, gameObject.transform.position, Quaternion.identity);            
        }
        Destroy(gameObject);
    }
}
