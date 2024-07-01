using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorBasura : MonoBehaviour{
    public int numero;
    public GameObject basuras;
    public Vector3 min, max;

    void Start(){
        for (int i = 0; i < numero; i++){
            Vector3 position = new Vector3(
                Random.Range(min.x,max.x),
                Random.Range(min.y,max.y),
                Random.Range(min.z,max.z)
            );
            Instantiate(basuras, position, Quaternion.identity, transform);
        }
    }
}
