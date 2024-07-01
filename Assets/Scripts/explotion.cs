using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explotion : MonoBehaviour
{
    public LayerMask mask;


    void ExplotionDamage(Vector3 center, float radius)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
            foreach (var currentColl in hitColliders)
            {
                var comp = currentColl.GetComponent<Rigidbody>();
                if (comp != null)
                {
                    Vector3 dir = currentColl.gameObject.transform.position - transform.position;

                    currentColl.GetComponent<Rigidbody>().AddForce(1200 * dir.normalized);
                }
              
            }
        }
        
    }
}
