using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    public Collider triggerCollider;
    public Camera mainCamera;
    public Camera followCamera;

    private bool isFollowing = false;

    void Start(){


    followCamera.gameObject.SetActive(false);
    mainCamera.gameObject.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            mainCamera.gameObject.SetActive(false);
            followCamera.gameObject.SetActive(true);
            isFollowing = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            StopFollowing();
        }
    }

    private void LateUpdate()
    {
        if (isFollowing)
        {
            // Follow the object
            if (objectToFollow != null)
            {
                // Offset the camera position if needed
                Vector3 offset = new Vector3(0f, 2f, -5f); // Example offset, adjust as needed
                followCamera.transform.position = objectToFollow.transform.position + offset;

                // Make the camera look at the object
                followCamera.transform.LookAt(objectToFollow.transform.position);
            }
            else
            {
                // If the object to follow is null, stop following
                StopFollowing();
            }
        }
    }

    // Method to stop following the object
    private void StopFollowing()
    {
        mainCamera.gameObject.SetActive(true);
        followCamera.gameObject.SetActive(false);

        isFollowing = false;
    }
}