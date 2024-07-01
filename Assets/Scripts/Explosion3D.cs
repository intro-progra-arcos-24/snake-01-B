using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion3D : MonoBehaviour
{
    public LayerMask mask;
    public float radius = 5f;
    public float poder = 5;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            ExplosionDamage(transform.position, radius);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            ChupaDamage(transform.position, radius);
        }
    }

    void ExplosionDamage(Vector3 center, float radius)
    {

       Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, mask);
       foreach (Collider hitCollider in hitColliders)
       {
            Rigidbody comp = hitCollider.GetComponent<Rigidbody>();
            if (comp != null)
            {
                
                Vector3 dir = hitCollider.gameObject.transform.position-transform.position; //libera

                hitCollider.GetComponent<Rigidbody>().AddForce(poder*dir.normalized);
            }
       }
       
        
    }

    void ChupaDamage(Vector3 center, float radius)
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, mask);
        foreach (Collider hitCollider in hitColliders)
        {
            Rigidbody comp = hitCollider.GetComponent<Rigidbody>();
            if (comp != null)
            {
                Vector3 dir = transform.position - hitCollider.gameObject.transform.position; //chupa
                //Vector3 dir = hitCollider.gameObject.transform.position - transform.position; //libera

                hitCollider.GetComponent<Rigidbody>().AddForce(poder * dir.normalized);
            }
        }


    }
    
}
