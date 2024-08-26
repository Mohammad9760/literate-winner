using UnityEngine;

public class ParallaxSprite : MonoBehaviour
{
    [SerializeField] private float depth = 1f; // Depth parameter to control parallax effect
    [SerializeField] private float smoothing = 1f; // How smooth the parallax effect is

    private Transform cam; // Reference to the camera's transform
    private Vector3 previousCamPos; // Store the previous frame's camera position
    private Vector3 initialPosition; // Store the initial position of the sprite

    void Awake()
    {
        cam = Camera.main.transform; // Get the main camera's transform
        initialPosition = transform.position; // Store the initial position of the sprite
    }

    void Start()
    {
        previousCamPos = cam.position; // Initialize the previous camera position
    }

    void Update()
    {
        // Calculate the camera's movement
        float deltaX = cam.position.x - previousCamPos.x;

        // Calculate the parallax effect based on the depth
        float parallax = deltaX * (1f / depth); // Invert the depth for movement

        // Move the sprite based on the parallax effect
        Vector3 targetPosition = initialPosition + new Vector3(parallax, 0, 0);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);

        previousCamPos = cam.position; // Update the previous camera position
    }
}
