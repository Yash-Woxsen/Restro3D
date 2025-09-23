using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour, PlayerInputs.ICameraActions
{
    public Transform player;
    public Vector3 defaultOffset = new Vector3(0, 10, -10);
    public Vector3 zoomedOffset = new Vector3(0, 5, -5);
    public float followSpeed = 5f;

    private PlayerInputs input;
    private bool isZoomed = false;

    void Awake()
    {
        input = new PlayerInputs();
        input.Camera.SetCallbacks(this);
    }

    void OnEnable()
    {
        input.Camera.Enable();
    }

    void OnDisable()
    {
        input.Camera.Disable();
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetOffset = isZoomed ? zoomedOffset : defaultOffset;
        Vector3 targetPos = player.position + targetOffset;

        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }

    // Called whenever CamZoom triggers (scroll or pinch)
    public void OnCamZoom(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        float value = ctx.ReadValue<float>();

        // Mouse scroll up / pinch in ? zoom in
        if (value > 0)
            isZoomed = true;
        // Mouse scroll down / pinch out ? zoom out
        else if (value < 0)
            isZoomed = false;
    }
}
