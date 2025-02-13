using UnityEngine;

public class ExtinguisherSelector : MonoBehaviour
{
    private Extinguisher hoveredExtinguisher;

    void Update()
    {
        CheckMousePosForExtinguisher();

        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            MouseClick();
        }
    }

    void CheckMousePosForExtinguisher()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Raycast from the camera to the mouse position
        if (Physics.Raycast(ray, out hit))
        {
            Extinguisher newHoveredExtinguisher = hit.collider.GetComponent<Extinguisher>();

            if (newHoveredExtinguisher != null)
            {
                if (hoveredExtinguisher != newHoveredExtinguisher)
                {
                    if (hoveredExtinguisher != null)
                    {
                        hoveredExtinguisher.Unhover();
                    }
                    hoveredExtinguisher = newHoveredExtinguisher;
                    hoveredExtinguisher.Hover();
                }
            }
            else if (hoveredExtinguisher != null)
            {
                hoveredExtinguisher.Unhover();
                hoveredExtinguisher = null;
            }
        }
        else if (hoveredExtinguisher != null)
        {
            hoveredExtinguisher.Unhover();
            hoveredExtinguisher = null;
        }
    }

    void MouseClick()
    {
        if (hoveredExtinguisher != null)
        {
            hoveredExtinguisher.Clicked();
        }
    }
}