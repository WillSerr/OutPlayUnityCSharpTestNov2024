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
            dead = true;    //prevent multiple explosions from Detonate being called multiple times before the object is destroyed
            Instantiate(explosionObject, gameObject.transform.position, Quaternion.identity);            
        }
        Destroy(gameObject);
    }
}
