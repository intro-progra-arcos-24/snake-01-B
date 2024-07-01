using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explo : MonoBehaviour{
    public KeyCode teclaExplosion;
    public KeyCode teclaAtraccion;
    public float radio;
    public LayerMask layermask;
    public float force;

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(teclaExplosion)){
            AplicarFuerzas(force);
        }
        if (Input.GetKeyDown(teclaAtraccion)){
            AplicarFuerzas(-force);
        }
    }

    void AplicarFuerzas(float fuerza){
        Collider[] colls = Physics.OverlapSphere(transform.position, radio, layermask);
        foreach (Collider coll in colls){
            if (coll.attachedRigidbody != null){
                Vector3 direction = (coll.transform.position - transform.position).normalized;
                coll.attachedRigidbody.AddForce(direction * fuerza);
            }
        }
    }
}
