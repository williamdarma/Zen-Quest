using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform Path;
    public float maxSteerAngle = 40f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public List<Transform> nodes;
    public float CarSpeed;
    public float currentSpeed;
    public float MaxSpeed = 3f;

    int currentNode = 0;
    // Start is called before the first frame update
    void Start()
    {
        Path = GameObject.Find("Path").transform;
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


    void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWaypointDistance();
    }
    public bool Turning;
    void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
         print(currentSpeed);
        if (currentSpeed < MaxSpeed)
        {
            if (!Turning)
            {
                wheelFL.motorTorque = CarSpeed;
                wheelFR.motorTorque = CarSpeed;
            }
            else
            {
                wheelFL.motorTorque = 0;
                wheelFR.motorTorque = 0;
            }
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }

    }
    void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = relativeVector.x / relativeVector.magnitude * maxSteerAngle;
        if (newSteer >= 2f || newSteer <= -2f)
        {
            Turning = true;
        }
        else
        {
            Turning = false;
        }
        //print(newSteer);
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < .8f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}
