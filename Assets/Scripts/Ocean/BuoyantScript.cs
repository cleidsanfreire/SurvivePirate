using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyantScript : MonoBehaviour {
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float buoyancyForce = 40f;
    private Rigidbody thisRigidbody;
    private bool hasTouchedWater;
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float diffy = transform.position.y;
        bool isUnderwater = diffy < 0;
        if (isUnderwater) {
            hasTouchedWater = true;
        }

        // Ignore if never touched water
        if (!hasTouchedWater) {
            return;
        }

        // Buoyancy

        if (isUnderwater) {
        Vector3 vector = Vector3.up * buoyancyForce * -diffy;
        thisRigidbody.AddForce(vector, ForceMode.Acceleration);
        }
        thisRigidbody.drag = isUnderwater ? underwaterDrag : airDrag;
        thisRigidbody.angularDrag = isUnderwater ? underwaterAngularDrag : airAngularDrag;
    }
}
