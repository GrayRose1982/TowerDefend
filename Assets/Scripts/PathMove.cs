using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMove : MonoBehaviour
{
    public Transform[] Paths;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < Paths.Length - 1; i++)
        {
            Gizmos.DrawLine(Paths[i].position, Paths[i + 1].position);
        }
    }
#endif
}
