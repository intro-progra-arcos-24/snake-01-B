using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjercicioExplosión : MonoBehaviour
{
    public LayerMask mask;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Explosion(transform.position, 10f);
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            Atraer(transform.position, 10f);
        }
    }

    private void Explosion(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, mask);

        foreach (var hitCollider in hitColliders)
        {
            print("Chocó algo");
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 dir = hitCollider.gameObject.transform.position - transform.position;
                hitCollider.GetComponent<Rigidbody>().AddForce(1200 * dir.normalized);
            }
        }
    }

    private void Atraer(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, mask);

        foreach (var hitCollider in hitColliders)
        {
            print("Chocó algo");
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 dir = hitCollider.gameObject.transform.position - transform.position;
                hitCollider.GetComponent<Rigidbody>().AddForce(-1200 * dir.normalized);
            }
        }
    }
}
