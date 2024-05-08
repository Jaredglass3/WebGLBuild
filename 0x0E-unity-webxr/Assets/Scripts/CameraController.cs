using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 10.0f;
    public Transform mainCamera;
    public float rotationSpeed = 10.0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Disable animator
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator != null)
                animator.enabled = false;
        }

        // Zoom in and out with up and down arrow keys
        float zoomInput = Input.GetAxis("Vertical");
        mainCamera.transform.Translate(Vector3.forward * zoomInput * zoomSpeed * Time.deltaTime * 10);

        // Rotate left and right with left and right arrow keys
        float rotationInput = Input.GetAxis("Horizontal");
        mainCamera.transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime * 10);
    }
}
