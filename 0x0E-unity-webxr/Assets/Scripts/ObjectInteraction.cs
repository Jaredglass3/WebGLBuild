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
        if (isGrabbing)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            currentObject.transform.position = mousePos + offset;
        }
    }

    void GrabObject(Vector3 hitPoint)
    {
        isGrabbing = true;
        // Calculate the offset between object position and mouse position on grab
        offset = currentObject.transform.position - hitPoint;
    }

    void ReleaseObject()
    {
        isGrabbing = false;

        // Calculate the direction to throw the object
        Vector3 throwDirection = Camera.main.transform.forward;

        // Add force to the object to project it forward
        Rigidbody rb = currentObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("The object does not have a Rigidbody component.");
        }

        currentObject = null; // Reset currentObject after releasing
    }
}
