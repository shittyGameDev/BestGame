using System.Collections;
using System.Collections.Generic;

using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;


public class CameraAndMovementScript : MonoBehaviour
{

    void Start()
    {

        debugSphereCollissionDetection = debugMousePositionSphere.GetComponent<DebugSphereCollissionDetection>();

        topRightBorderCorner = topRightCornerT.position;
        bottomLeftBorderCorner = BottomLeftCornerT.position;
    }

    public Rigidbody RigidBodyOFCameraMover;
  

    [Header("DebugSphere")]//

    public Transform debugMousePositionSphere;
    public Material debugCorrectMaterial;
    public Material debugWrongMaterial;
    public MeshRenderer sphereMeshRenderer;
    //
    [Header("Camera Movement Borders")]//
    private Vector3 bottomLeftBorderCorner;
    private Vector3 topRightBorderCorner;

    public Transform topRightCornerT;
    public Transform BottomLeftCornerT;
    //

    void Update()
    {
        CameraHitPosCalc();
        moveCamera();
        
        
    }
    void FixedUpdate()
    {
        movePlayer();

    }
    [Header("Movement:")]
    public float moveSpeed;
    public float smoothing=2;
    private Vector3 moveDir;
    private void movePlayer()
    {
        Vector3 inputDir = Vector3.zero;
        if(Input.GetKey("w"))
        {
            inputDir+=Vector3.forward;
        }
        if(Input.GetKey("s"))
        {
            inputDir-=Vector3.forward;
        }
        if(Input.GetKey("d"))
        {
            inputDir+=Vector3.right;
        }
        if(Input.GetKey("a"))
        {
            inputDir-=Vector3.right;
        }
        inputDir = inputDir.normalized;
        if(Input.GetKey("left shift"))
        {
            inputDir*=2f;
        }
        //moveDir = Vector3.Lerp(moveDir,inputDir)
        RigidBodyOFCameraMover.MovePosition(RigidBodyOFCameraMover.position+inputDir*moveSpeed*Time.fixedDeltaTime);
        
        
        //

        
        
        

    }
    private void moveCamera()
    {
        Transform tR = RigidBodyOFCameraMover.transform;
        
        // if(tR.position.x<bottomLeftBorderCorner.x)
        // {
        //     tR.position+=new Vector3(0.1f,0,0);
        // }
        // if(tR.position.x>topRightBorderCorner.x)
        // {
        //     tR.position-=new Vector3(0.1f,0,0);
        // }

        // if(tR.position.z<bottomLeftBorderCorner.z)
        // {
        //     tR.position+=new Vector3(0,0,0.1f);
        // }
        // if(tR.position.z>topRightBorderCorner.z)
        // {
        //     tR.position-=new Vector3(0,0,0.1f);
        // }

        // if(tR.position.x<bottomLeftBorderCorner.x)
        // {
        //     tR.position=new Vector3(bottomLeftBorderCorner.x+0.01f,tR.position.y,tR.position.z);
        // }
        // if(tR.position.x>topRightBorderCorner.x)
        // {
        //     //tR.position-=new Vector3(0.1f,0,0);
        //     tR.position=new Vector3(bottomLeftBorderCorner.x-0.01f,tR.position.y,tR.position.z);
        // }

        // if(tR.position.z<bottomLeftBorderCorner.z)
        // {
        //     //tR.position+=new Vector3(0,0,0.1f);
        //     tR.position=new Vector3(tR.position.x,tR.position.y,bottomLeftBorderCorner.x+0.01f);
        // }
        // if(tR.position.z>topRightBorderCorner.z)
        // {
        //     //tR.position-=new Vector3(0,0,0.1f);
        //     tR.position=new Vector3(tR.position.x,tR.position.y,bottomLeftBorderCorner.x-0.01f);
        // }



        //transform.position = Vector3.Lerp(transform.position, RigidBodyOFCameraMover.transform.position,smoothing*Time.deltaTime);
    }
    
    private DebugSphereCollissionDetection debugSphereCollissionDetection;




    private void CameraHitPosCalc()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200))
        {
            if(hit.transform!=debugMousePositionSphere)
            debugMousePositionSphere.position = hit.point;
        }

        foreach(Collider c in debugSphereCollissionDetection.colliders)
        {
            if(c.transform.tag=="Ground")
            {
                sphereMeshRenderer.material = debugCorrectMaterial;
            }
            else if(c.transform.tag=="PlacementObst")
            {
                sphereMeshRenderer.material = debugWrongMaterial;
                return;
            }

        }


        

        
    }

}
