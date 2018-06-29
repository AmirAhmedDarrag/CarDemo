using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {
    public Transform Path;

    private List<Transform> nodes;
    private int CurrentNode = 0;
    public float maxSteerAngle = 45.0f;
    public WheelCollider wheel_fl;
    public WheelCollider wheel_fr;
    public float maxMotorTorque = 100.0f;
    public float currentSpeed;
    public float maxSpeed = 100.0f;
    public Vector3 centerOfMass;


    [Header("Sensors")]
    public float sensorLength = 5.0f;
    public float frontSensorPos = 0.5f;
    // Use this for initialization
    void Start () {

        GetComponent<Rigidbody>().centerOfMass = centerOfMass;

        Transform[] pathTransform = Path.GetComponentsInChildren<Transform>();

        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {

            if (pathTransform[i] != Path.transform)
            {

                nodes.Add(pathTransform[i]);
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Sensors();
        ApplySteer();
        Drive();
        checkWayPointDistance();
		
	}

    private void Sensors() {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos.z = frontSensorPos;

        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength)) {
        }
        Debug.DrawLine(sensorStartPos, hit.point);
    }

    private void ApplySteer() {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[CurrentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheel_fl.steerAngle = newSteer;
        wheel_fr.steerAngle = newSteer;
    }

    private void Drive() {
        currentSpeed = 2 * Mathf.PI * wheel_fl.radius * wheel_fr.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed)
        {
            wheel_fl.motorTorque = maxMotorTorque;
            wheel_fr.motorTorque = maxMotorTorque;
        }
        else {
            wheel_fl.motorTorque = 0;
            wheel_fr.motorTorque = 0;
        }

    }
    private void checkWayPointDistance() {
        if((Vector3.Distance(transform.position,nodes[CurrentNode].position))< 4.0f)
        {
            if (CurrentNode == nodes.Count - 1)
            {
                CurrentNode = 0;
            }
            else {
                CurrentNode++;
            }
        }
    }
}
