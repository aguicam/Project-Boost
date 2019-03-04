using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator : MonoBehaviour {
    [SerializeField] Vector3 movementVector = new Vector3(0,0,20f);
    [SerializeField] float period = 2f;

    //todo remove from inspecto later
    [Range(0,1)]float movementFactor; // 0 for not moved, 1 for moved
    Vector3 starttingPos;

	// Use this for initialization
	void Start () {
        starttingPos = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        if(period <= Mathf.Epsilon) { return; }
        float cylces = Time.time / period;
        float rawSinWave = Mathf.Sin(2f * Mathf.PI * cylces);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = starttingPos + offset;

    }
}
