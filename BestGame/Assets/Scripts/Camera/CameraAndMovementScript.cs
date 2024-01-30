using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAndMovementScript : MonoBehaviour
{

    void Start()
    {
        
    }
    [Header("DebugSphere")]
    public Transform debugMousePositionSphere;
    public Material debugCorrectMaterial;
    public Material debugWrongMaterial;
    public MeshRenderer sphereMeshRenderer;
    
    void Update()
    {
        CameraHitPosCalc();

        
    }

    private void CameraHitPosCalc()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            sphereMeshRenderer.material = debugCorrectMaterial;
        }
        else
        {
            sphereMeshRenderer.material = debugWrongMaterial;
        }
        debugMousePositionSphere.position = hit.point;

    }
}
