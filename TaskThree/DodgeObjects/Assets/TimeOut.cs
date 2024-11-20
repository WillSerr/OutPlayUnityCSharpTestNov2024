using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOut : MonoBehaviour
{
    [SerializeField] private float lifetime = 1.5f;
    private float deathTimer = 0;


    // Update is called once per frame
    void Update()
    {
        //Destroy object after lifetime expires
        if (deathTimer > lifetime)
        {
            Destroy(gameObject);
        }
        else
        {
            deathTimer += Time.deltaTime;
        }
    }
}
