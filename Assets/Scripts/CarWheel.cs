using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour {
    public WheelCollider Targetwheel;
    private Vector3 wheelPos = new Vector3();
    private Quaternion WheelRot = new Quaternion();
	
	
	// Update is called once per frame
	void Update () {
        Targetwheel.GetWorldPose(out wheelPos, out WheelRot);

        transform.position = wheelPos;
        transform.rotation = WheelRot;
	}
}
