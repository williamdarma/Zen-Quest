using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public bool usedOffsetValues;
    // Start is called before the first frame update
    void Start()
    {
        if (!usedOffsetValues)
        {
            offset = target.transform.position - transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        transform.position = target.transform.position - offset;
        transform.LookAt(target.transform);
    }
}
