using UnityEngine;

public class MouseObjectInteraction : MonoBehaviour
{
    private bool isGrabbing = false;
    private GameObject currentObject;
    private Vector3 offset; // Store the offset between object position and mouse position
    private float throwForce = 10f; // Adjust the throw force as needed

    void Update()
    {
        // Check for mouse input
        if (Input.GetMouseButtonDown(0) && !isGrabbing)
        {
            // Cast a ray from the mouse position to detect objects
            RaycastHit hit;
            Debug.Log("Camera: " + Camera.main.gameObject.name);
            Debug.Log("Input Pos: " + Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // If the ray hits an object, grab it
                currentObject = hit.collider.gameObject;
                GrabObject(hit.point);
            }
        }
        else if (Input.GetMouseButtonUp(0) && isGrabbing)
        {
            // If currently grabbing with the mouse, release the object and throw it
            ReleaseObject();
        }

        // While grabbing, update the position of the object based on mouse movement
        if (isGrabbing && currentObject != null) // Added null check here
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            currentObject.transform.position = mousePos + offset;
        }
    }

    private void GrabObject(Vector3 hitPoint)
    {
        isGrabbing = true;
        offset = currentObject.transform.position - hitPoint;
        currentObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ReleaseObject()
    {
        isGrabbing = false;
        currentObject.GetComponent<Rigidbody>().isKinematic = false;
        Rigidbody rb = currentObject.GetComponent<Rigidbody>();
        Vector3 throwDirection = Camera.main.transform.forward;
        rb.velocity = throwDirection * throwForce;
        currentObject = null; // Reset currentObject after releasing
    }
}
