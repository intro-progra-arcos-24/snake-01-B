using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5f;
    public LayerMask layerMask;
    
    private void Start() {
      
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.A)){
            EncontrarCubos();
        }
    }
public void EncontrarCubos(){
    Vector3 position = transform.position;

        Debug.DrawLine(position + Vector3.up * radius, position - Vector3.up * radius, Color.red);
        Debug.DrawLine(position + Vector3.right * radius, position - Vector3.right * radius, Color.red);

        Collider[] colliders = Physics.OverlapSphere(position, radius, layerMask);

        foreach (Collider collider in colliders)
        {
            Rigidbody comp = collider.GetComponent<Rigidbody>();

            if(comp != null){
                Vector3 dir = collider.gameObject.transform.position - transform.position;
                collider.GetComponent<Rigidbody>().AddForce(500  * dir.normalized);
            }

            Debug.Log("Detected: " + collider.name);
        }
}
}
