using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFlags : MonoBehaviour
{
    [SerializeField] private float move_speed = 10;

    [SerializeField] private Vector3 pointOne = Vector3.zero;
    [SerializeField] private Vector3 pointTwo = Vector3.zero;
    [SerializeField] private Vector3 pointThree = Vector3.zero;


    private Vector3 CurrentTarget = Vector3.zero;
    private int currentTarget = 0;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTarget = pointOne;
    }

    // Update is called once per frame
    void Update()
    {
        //a -> b = b - a
        Vector3 distance = CurrentTarget - gameObject.transform.position;

        Vector3 direction = distance.normalized;


        if (distance.magnitude > move_speed * Time.deltaTime)
        {
            gameObject.transform.position += direction * move_speed * Time.deltaTime;
        }
        else
        {
            gameObject.transform.position = CurrentTarget;
            if (CurrentTarget == pointOne) 
            { 
                CurrentTarget = pointTwo; 
            }
            else if (CurrentTarget == pointTwo) 
            { 
                CurrentTarget = pointThree; 
            }
            else if (CurrentTarget == pointThree) 
            {
                //Die
                GetComponent<Explode>().Detonate();
            }
        }
    }
}
