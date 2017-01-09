using UnityEngine;
using System.Collections;

public class addForce : MonoBehaviour {

    public float thrust;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(gameObject.transform.forward * thrust);
    }
}
