using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Collider[] collisions = Physics.OverlapSphere(transform.position, 20);
            foreach (Collider currentColl in collisions)
            {
                Rigidbody comp = currentColl.GetComponent<Rigidbody>();
                if (comp != null)
                {
                    Vector3 dir = currentColl.gameObject.transform.position - transform.position;

                    currentColl.GetComponent<Rigidbody>().AddForce(1200 * dir.normalized);
                }
            }
        }
    }
}
