using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.1f; // Duration of the camera shake
    public float shakeMagnitude = 0.1f; // Magnitude of the camera shake

    private float currentShakeDuration = 0f;
    private float currentShakeMagnitude = 0f;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            // Generate a random offset for the camera position
            Vector3 randomOffset = Random.insideUnitSphere * currentShakeMagnitude;

            // Apply the offset to the camera position
            transform.localPosition = originalPosition + randomOffset;

            // Reduce the shake duration over time
            currentShakeDuration -= Time.deltaTime;
        }
        else
        {
            // Reset the camera position
            transform.localPosition = originalPosition;
        }
    }

    public void ShakeCamera()
    {
        currentShakeDuration = shakeDuration;
        currentShakeMagnitude = shakeMagnitude;
    }
}