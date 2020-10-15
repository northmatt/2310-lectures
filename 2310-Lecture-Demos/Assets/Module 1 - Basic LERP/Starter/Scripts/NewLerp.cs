using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorMath {
    public static Vector3 LERP(Vector3 p0, Vector3 p1, float time) {
        return p0 * (1 - time) + p1 * time;
    }
}

public class NewLerp : MonoBehaviour {
    public Transform p0;
    public Transform p1;
    public float speed = 1;
    private Transform objTrans;
    private float time = 0;
    private bool flippedMovement = false;

    // Start is called before the first frame update
    void Start() {
        objTrans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if (flippedMovement && time == 0f || !flippedMovement && time == 1f) {
            speed *= -1;
            flippedMovement = !flippedMovement;
        }

        time = Mathf.Clamp01(time + speed * Time.deltaTime);

        /*    \/ better perf cuz doing less checks and operations
         if (flippedMovement) {
            time -= speed * Time.deltaTime;

            if (time <= 0) {
                time = 0;
                flippedMovement = false;
            }
        } else {
            time += speed * Time.deltaTime;

            if (time >= 1) {
                time = 1;
                flippedMovement = true;
            }
        }*/

        objTrans.position = VectorMath.LERP(p0.position, p1.position, time);
    }
}
