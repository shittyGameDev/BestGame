using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebugSphereCollissionDetection : MonoBehaviour
{
    private List<Collider> cols=new List<Collider>();
    public Collider[] colliders;
    private void OnTriggerEnter(Collider col)
    {
        cols.Add(col);

        colliders = cols.ToArray();

    }
    private void OnTriggerExit(Collider col)
    {
        cols.Remove(col);
        colliders = cols.ToArray();

    }
}
