using UnityEngine;

public class Ability : MonoBehaviour
{
    public GameObject abilityPrefab;
    private GameObject tempPrefabInstance;
    private bool isPrefabFollowingMouse = false;
    private bool canPlacePrefab = false;

    private Material defaultMaterial;
    public Material validMaterial;
    public Material invalidMaterial;

    private MeshRenderer prefabMeshRenderer;
    private Renderer rend;


    void Start()
    {
        rend = abilityPrefab.GetComponent<Renderer>();
        defaultMaterial = rend.sharedMaterial;

    }
    void Update()
    {
        if (isPrefabFollowingMouse)
        {
            FollowMouse();
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement() && canPlacePrefab)
            {
                PlacePrefab(); // Place the prefab at the current mouse position
                isPrefabFollowingMouse = false;
            }
        }
    }

    // Call this from the UI button
    public void StartPlacingAbility()
    {
        if (tempPrefabInstance == null)
        {
            tempPrefabInstance = Instantiate(abilityPrefab);
            prefabMeshRenderer = tempPrefabInstance.GetComponent<MeshRenderer>();
            isPrefabFollowingMouse = true;
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
                // Adding an offset to the y-coordinate
                Vector3 prefabPosition = hit.point + new Vector3(0, 1, 0); // Adjust the 0.5f value as needed
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
        if (!canPlacePrefab)
        {
            Destroy(tempPrefabInstance);

        }
        
        prefabMeshRenderer.material = defaultMaterial;
        isPrefabFollowingMouse = false;
        tempPrefabInstance = null;
    }

    // Utility method to check if the pointer is over a UI element
    private bool IsPointerOverUIElement()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
