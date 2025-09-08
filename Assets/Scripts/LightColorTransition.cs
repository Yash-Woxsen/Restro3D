using UnityEngine;

public class LightColorTransition : MonoBehaviour
{
    [Header("Assign your spotlight here")]
    public Light spotLight;

    [Header("Colors to transition between")]
    public Color[] colors;

    [Header("Transition settings")]
    public float transitionSpeed = 1.0f; // Time in seconds for one full transition

    private int currentColorIndex = 0;
    private int nextColorIndex = 1;
    private float t = 0f;

    void Start()
    {
        if (spotLight == null)
        {
            Debug.LogError("SpotLight not assigned!");
            this.enabled = false;
            return;
        }

        if (colors == null || colors.Length < 2)
        {
            Debug.LogError("At least 2 colors are required.");
            this.enabled = false;
            return;
        }

        // Initialize to the first color
        spotLight.color = colors[currentColorIndex];
    }

    void Update()
    {
        // Update interpolation timer
        t += Time.deltaTime / transitionSpeed;

        // Interpolate between current and next color
        spotLight.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);

        if (t >= 1f)
        {
            // Move to the next color in the list
            t = 0f;
            currentColorIndex = nextColorIndex;
            nextColorIndex = (nextColorIndex + 1) % colors.Length;
        }
    }
}
