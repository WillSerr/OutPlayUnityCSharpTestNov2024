using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFlags : MonoBehaviour
{
    [SerializeField] private float move_speed = 10;

    [SerializeField] private Vector3 pointOne = Vector3.zero;
    [SerializeField] private Vector3 pointTwo = Vector3.zero;
    [SerializeField] private Vector3 pointThree = Vector3.zero;


    private Vector3 currentTarget = Vector3.zero;
    private int currentTargetIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = pointOne;
    }

    // Update is called once per frame
    void Update()
    {
        //a -> b = b - a
        Vector3 distance = currentTarget - gameObject.transform.position;

        Vector3 direction = distance.normalized;

        //If the next movement wont overshoot the target
        if (distance.magnitude > move_speed * Time.deltaTime)
        {
            gameObject.transform.position += direction * move_speed * Time.deltaTime;
        }
        else //Set position to target position and update target position
        {
            gameObject.transform.position = currentTarget;
            if (currentTargetIndex == 0) 
            {
                currentTargetIndex = 1;
                currentTarget = pointTwo; 
            }
            else if (currentTargetIndex == 1) 
            {
                currentTargetIndex = 2;
                currentTarget = pointThree; 
            }
            else if (currentTargetIndex == 2) 
            {
                //Die
                GetComponent<Explode>().Detonate();
            }
        }
    }
}
