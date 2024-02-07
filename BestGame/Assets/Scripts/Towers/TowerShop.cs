using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject tempPrefabInstance;
    private bool isPrefabFollowingMouse = false;
    private bool canPlacePrefab = false;


    private Material defaultMaterial;
    [Header("Materials")]
    public Material validMaterial;
    public Material invalidMaterial;

    private MeshRenderer prefabMeshRenderer;
    private Renderer rend;


    void Start()
    {
        rend = towerPrefab.GetComponent<Renderer>();
        defaultMaterial = rend.sharedMaterial;

    }
    void Update()
    {
        if (isPrefabFollowingMouse && tempPrefabInstance != null)
        {
            FollowMouse();
            if (Input.GetMouseButtonDown(0))
            {
                if (!IsPointerOverUIElement() && canPlacePrefab)
                {
                    Debug.Log("Placing Prefab");
                    PlacePrefab();
                    isPrefabFollowingMouse = false;
                    canPlacePrefab = false;
                }
            }
        }
    }


    void OnMouseDown()
    {
        StartPlacingTower();
    }


    public void StartPlacingTower()
    {
        if (tempPrefabInstance == null)
        {
            tempPrefabInstance = Instantiate(towerPrefab);
            prefabMeshRenderer = tempPrefabInstance.GetComponent<MeshRenderer>();
            isPrefabFollowingMouse = true;

            FollowMouse();
        }
    }

    private void FollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit object has the tag "Ground"
            if (hit.collider.CompareTag("Ground"))
            {
                // Adding an offset to the y-coordinate based on the prefab's height
                Vector3 prefabPosition = hit.point + new Vector3(0, prefabMeshRenderer.bounds.size.y / 2, 0);
                tempPrefabInstance.transform.position = prefabPosition;
                canPlacePrefab = true;
                prefabMeshRenderer.material = validMaterial;
            }
            else if (hit.collider.CompareTag("PlacementObst"))
            {
                canPlacePrefab = false;
                prefabMeshRenderer.material = invalidMaterial;
            }
        }
    }


    private void PlacePrefab()
    {
        if (canPlacePrefab && CanPlaceOtherTowers())
        {
            TowerBase towerBase = tempPrefabInstance.GetComponent<TowerBase>();
            if (towerBase != null)
            {
                GameManager.Instance.SpendGold(towerBase.cost);
                towerBase.ActivateTower(); // Activate the tower
                Debug.Log("Tower placed");
            }

            prefabMeshRenderer.material = defaultMaterial;
            isPrefabFollowingMouse = false;
            tempPrefabInstance = null;
        }
        else
        {
            Destroy(tempPrefabInstance);
            tempPrefabInstance = null;
            isPrefabFollowingMouse = false;
        }
    }

    // Utility method to check if the pointer is over a UI element
    private bool IsPointerOverUIElement()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    private bool CanPlaceOtherTowers()
    {
        GoldTower goldTower = FindObjectOfType<GoldTower>();
        return tempPrefabInstance.GetComponent<GoldTower>() != null || goldTower != null;
    }
}
