using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBounds : MonoBehaviour
{
    [SerializeField] public Vector3 dimensions;
    [SerializeField] public GameObject objectToSpawn;

    private void Start()
    {
        if (objectToSpawn == null) { return; }

        Vector3 halfDimensions = dimensions / 2.0f;

        for (int i = 0; i < 100; ++i)
        {
            Vector3 pos = gameObject.transform.position + new Vector3(Random.Range(-halfDimensions.x, halfDimensions.x), Random.Range(-halfDimensions.y, halfDimensions.y), Random.Range(-halfDimensions.z, halfDimensions.z));
            Instantiate(objectToSpawn, pos, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, dimensions);
    }
}
