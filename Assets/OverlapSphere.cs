using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSphere : MonoBehaviour
{
    public float radius = 5f;
    public LayerMask mask;

    void Update()
    {
        Vector3 position = transform.position;

        Debug.DrawLine(start: position + Vector3.up * radius, end:position - Vector3.up * radius, Color.red);
        Debug.DrawLine(start: position + Vector3.right * radius, end: position - Vector3.right * radius, Color.red);

        Collider[] colliders = Physics.OverlapSphere(position, radius, mask);

        foreach (Collider collider in colliders)
        {
            Debug.Log(message: "Detected: " + collider.name);
        }
    }
}
