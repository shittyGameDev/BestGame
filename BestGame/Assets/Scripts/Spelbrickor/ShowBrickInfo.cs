using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBrickInfo : MonoBehaviour
{
    private GameObject brickInfoPanel;
    private GameObject brickInfoCanvas;
    private Camera mainCamera;

    public float verticalOffset = 140.0f; 

    void Start()
    {
        brickInfoCanvas = transform.GetChild(0).gameObject;
        brickInfoPanel = brickInfoCanvas.transform.GetChild(0).gameObject;
        mainCamera = Camera.main;
        brickInfoCanvas.SetActive(false);
    }


    private void OnMouseEnter()
    {
        brickInfoCanvas.SetActive(true);
        UpdatePanelPosition();
    }

    private void OnMouseExit()
    {
        brickInfoCanvas.SetActive(false);
    }


    // Uppdaterar positionen för panelen och placerar den ovanför brickan med en offset
    private void UpdatePanelPosition()
    {
        if (mainCamera != null)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                Vector3 topPosition = renderer.bounds.max;

                Vector3 screenPos = mainCamera.WorldToScreenPoint(topPosition);

                screenPos.y += verticalOffset;

                RectTransform rectTransform = brickInfoPanel.GetComponent<RectTransform>();
                if(rectTransform != null){
                    screenPos.x -= rectTransform.rect.width / 2;
                }

                brickInfoPanel.transform.position = screenPos;
            }
        }
    }
}
