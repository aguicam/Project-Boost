using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacles : MonoBehaviour {
    [SerializeField] float step = 40f;
    [SerializeField] float dir = 0f; // 0: rotation left, else rotation right

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float yRot = step * Time.deltaTime;
        if (dir <= Mathf.Epsilon)
        {

            transform.Rotate(Vector3.forward * yRot);
        }
        else
        {
            transform.Rotate(-Vector3.forward * yRot);
        }

    }
}
