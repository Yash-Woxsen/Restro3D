using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CameraFollow : MonoBehaviour, PlayerInputs.ICameraActions
    {
        [Header("References")]
        public Transform player;

        [Header("Camera Offsets")]
        public Vector3 defaultOffset = new Vector3(0, 10, -10);
        public Vector3 zoomedOffset = new Vector3(0, 5, -5);

        [Header("Settings")]
        public float followSpeed = 5f;
        [Range(0f, 1f)] public float zoomLevel = 0f; // 0 = far, 1 = close
        public float scrollSensitivity = 0.2f;       // How fast scroll affects zoom

        private PlayerInputs _inputRef;

        void Awake()
        {
            _inputRef = new PlayerInputs();
            _inputRef.Camera.SetCallbacks(this);
        }

        void OnEnable() => _inputRef.Camera.Enable();
        void OnDisable() => _inputRef.Camera.Disable();

        void LateUpdate()
        {
            if (ReferenceEquals(player, null)) return;

            // Interpolate between defaultOffset and zoomedOffset based on zoomLevel
            Vector3 targetOffset = Vector3.Lerp(defaultOffset, zoomedOffset, zoomLevel);
            Vector3 targetPos = player.position + targetOffset;

            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }

        // Mouse scroll input (from Input System)
        public void OnCamZoom(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;

            float scroll = ctx.ReadValue<float>(); // ✅ correct
            if (Mathf.Abs(scroll) > 0.01f)
            {
                //zoomLevel = Mathf.Clamp01(zoomLevel - scroll * scrollSensitivity * Time.deltaTime * 60f);
            }
        }


        // Called from UI Slider (0–1)
        public void SetZoomLevel(float value)
        {
            //zoomLevel = Mathf.Clamp01(value);
        }
    }
}
