using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebugSphereCollissionDetection : MonoBehaviour
{
    public Transform collider;
    private void OnTriggerStay(Collider col)
    {
        collider = col.transform;
    }
}
