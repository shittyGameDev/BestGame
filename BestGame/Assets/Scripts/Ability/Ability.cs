using UnityEngine;

public class Ability : MonoBehaviour
{
    public GameObject abilityPrefab;
    private GameObject tempPrefabInstance;
    private bool isPrefabFollowingMouse = false;

    void Update()
    {
        if (isPrefabFollowingMouse)
        {
            FollowMouse();
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                PlacePrefab(); // Place the prefab at the current mouse position
            }
        }
    }

    // Call this from the UI button
    public void StartPlacingAbility()
    {
        if (tempPrefabInstance == null)
        {
            tempPrefabInstance = Instantiate(abilityPrefab);
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
        }
    }
}


    private void PlacePrefab()
    {
        isPrefabFollowingMouse = false;
        tempPrefabInstance = null;
    }

    // Utility method to check if the pointer is over a UI element
    private bool IsPointerOverUIElement()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
