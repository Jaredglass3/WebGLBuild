using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;


public class PlaneSelection : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlane selectedPlane;
    private Material planeMaterial;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        // Check if user touches the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Perform raycast from the touch point
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // If the ray hits an ARPlane, select it
                ARPlane plane = GetSelectedPlane(hit);
                if (plane != null)
                {
                    SelectPlane(plane);
                }
            }
        }
    }

    // Get the ARPlane hit by the raycast
    private ARPlane GetSelectedPlane(RaycastHit hit)
    {
        ARPlane plane = null;

        // Try to get the ARPlane component from the hit object or its parents
        plane = hit.transform.GetComponentInParent<ARPlane>();

        return plane;
    }

    // Select the given ARPlane
    private void SelectPlane(ARPlane plane)
    {
        // Deselect the previously selected plane if any
        if (selectedPlane != null)
        {
            selectedPlane.GetComponent<MeshRenderer>().material = planeMaterial;
        }

        // Set the selected plane
        selectedPlane = plane;

        // Store the material of the selected plane
        planeMaterial = selectedPlane.GetComponent<MeshRenderer>().material;

        // Create an outline material
        Material outlineMaterial = new Material(Shader.Find("Standard"));
        outlineMaterial.color = Color.red; // Change color to red
        outlineMaterial.SetFloat("_Outline", 0.002f);
        outlineMaterial.SetFloat("_OutlineWidth", 0.01f);
        outlineMaterial.SetFloat("_Glossiness", 0f);

        // Apply the outline material to the selected plane
        selectedPlane.GetComponent<MeshRenderer>().material = outlineMaterial;

    }
}