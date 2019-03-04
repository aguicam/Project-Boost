using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour {

    [SerializeField] float step = 40f;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        float yRot = step*Time.deltaTime;
        transform.Rotate(Vector3.down*yRot);
    }
}
