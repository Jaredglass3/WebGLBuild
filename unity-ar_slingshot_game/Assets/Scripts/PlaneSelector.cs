using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneSelector : MonoBehaviour
{
    public static ARPlane selectedPlane;
    public Canvas uiCanvas;

    private ARPlaneManager arPlaneManager;

    private void Start()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("Touch detected: " + Input.GetTouch(0).phase);

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Debug.Log("Ray cast!");

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Ray hit!");
                ARPlane plane = hit.collider.GetComponent<ARPlane>();

                if (plane != null)
                {
                    // Save the selected plane
                    selectedPlane = plane;

                    // Disable other planes
                    foreach (var p in arPlaneManager.trackables)
                    {
                        if (p != selectedPlane)
                        {
                            Debug.Log("Selected Plane: " + selectedPlane);
                            p.gameObject.SetActive(false);
                        }
                    }

                    // Activate the Canvas
                    if (uiCanvas != null)
                    {
                        uiCanvas.gameObject.SetActive(true);
                        Debug.Log("Canvas Activated");
                    }
                }
            }
        }
    }
}
