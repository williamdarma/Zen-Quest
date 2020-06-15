using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    public List<Transform> Path = new List<Transform>(); 
    public Color lineColor;
    // Start is called before the first frame update
    void Start()
    {
        // Path = GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDrawGizmos()
    {
        Transform[] pathTransform = GetComponentsInChildren<Transform>();
        Path = new List<Transform>();
        for(int i =0; i < pathTransform.Length;i++)
        {
            if (pathTransform[i] != transform)
            {
                Path.Add(pathTransform[i]);
            }
        }
        for (int i = 0; i < Path.Count; i++)
        {
            Vector3 currentPath = Path[i].position;
            Vector3 previousPath = Vector3.zero;
            if (i >0)
            {
                previousPath = Path[i-1].position;
            }
            else if (i ==0 && Path.Count > 1)
            {
                previousPath = Path[Path.Count-1].position;
            }
            Gizmos.DrawLine(previousPath,currentPath);
            Gizmos.DrawWireSphere(currentPath,.3f);
        }
    }
}
