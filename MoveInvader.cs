using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInvader : MonoBehaviour {
    public float speed = 20.0f;
    private float magnitude;
    private bool moveX = false;
    private bool moveY = false;
    private bool moveZ = false;

    private float minimoX;
    private float maximoX;
    private float minimoY;
    private float maximoY;
    private float minimoZ;
    private float maximoZ;

    private float x;
    private float y;
    private float z;
    void Start() {
        moveX = (Random.value > 0.5f);
        moveY = (Random.value > 0.6f);
        moveZ = (Random.value > 0.8f);

        minimoX = gameObject.transform.position.x - 40;
        maximoX = gameObject.transform.position.x + 40;
        
        minimoY = gameObject.transform.position.y;
        maximoY = gameObject.transform.position.y + 10;
        
        minimoZ = gameObject.transform.position.z - 7;
        maximoZ = gameObject.transform.position.z + 3;
    }

    void Update() {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        z = gameObject.transform.position.z;

        magnitude = Time.time * speed;

        gameObject.transform.position = new Vector3(
            (moveX ? Mathf.PingPong(magnitude, maximoX-minimoX) + minimoX : x),
            (moveY ? Mathf.PingPong(magnitude, maximoY-minimoY) + minimoY : y),
            (moveZ ? Mathf.PingPong(magnitude, maximoZ-minimoZ) + minimoZ : z)
        );
    }
}
