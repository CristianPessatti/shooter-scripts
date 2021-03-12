using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInvader : MonoBehaviour
{

    public float speed = 180.0f;
    private bool rotateY;
    private bool rotateX;
    private bool rotateZ;

    void Start() {
        rotateY = (Random.value > 0.5f);
        rotateX = (Random.value > 0.5f);
        rotateZ = (Random.value > 0.5f);
    }


    void Update() {
        if (rotateX) {
            gameObject.transform.Rotate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if (rotateY) {
            gameObject.transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
        if (rotateZ) {
            gameObject.transform.Rotate(Vector3.back * speed * Time.deltaTime, Space.World);
        }   
    }
}
