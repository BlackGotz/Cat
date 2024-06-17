using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes
    {
        line,
        loop
    }
     public PathTypes pathType;
    public Transform[] PathElements;

    public void OnDrawGizmos()
    {
        if(PathElements == null || PathElements.Length<2)
        {
            return;
        }

        for (var i =1; i<PathElements.Length; i++)
        {
            Gizmos.DrawLine(PathElements[i-1].position, PathElements[i].position);
        }

        if (pathType == PathTypes.loop)
        {
            Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length-1].position);
        }
    }

}
