using UnityEngine;
using System.Collections;

public class rotateScript : MonoBehaviour {

    public float rotationSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.RotateAround(Vector3.up, Time.deltaTime * rotationSpeed);

          
	}
}
