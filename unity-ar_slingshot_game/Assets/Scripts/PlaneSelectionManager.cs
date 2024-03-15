using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneSelectionManager : MonoBehaviour
{
    private static ARPlane selectedPlane;
    private bool planeSelected = false;

    [SerializeField] private Canvas[] canvasesToActivate;

    private void Update()
    {
        if (!planeSelected && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                ARPlane plane = hit.transform.GetComponent<ARPlane>();
                if (plane != null)
                {
                    SelectPlane(plane);
                }
            }
        }
    }

    private void SelectPlane(ARPlane plane)
    {
        selectedPlane = plane;
        planeSelected = true;
        DisplayCanvas();
    }

    private void DisplayCanvas()
    {
        foreach (Canvas canvas in canvasesToActivate)
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
            }
        }
    }

    public static ARPlane GetSelectedPlane()
    {
        return selectedPlane;
    }
}
