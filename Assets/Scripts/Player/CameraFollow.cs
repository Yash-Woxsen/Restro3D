using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CameraFollow : MonoBehaviour, PlayerInputs.ICameraActions
    {
        public Transform player;
        public Vector3 defaultOffset = new Vector3(0, 10, -10);
        public Vector3 zoomedOffset = new Vector3(0, 5, -5);
        public float followSpeed = 5f;

        private PlayerInputs _inputRef;

        // Slider-driven zoom factor (0 = default, 1 = zoomed)
        [Range(0f, 1f)]
        public float zoomLevel = 0f;

        void Awake()
        {
            _inputRef = new PlayerInputs();
            _inputRef.Camera.SetCallbacks(this);
        }

        void OnEnable()
        {
            _inputRef.Camera.Enable();
        }

        void OnDisable()
        {
            _inputRef.Camera.Disable();
        }

        void LateUpdate()
        {
            if (ReferenceEquals(player, null)) return;

            // Interpolate between defaultOffset and zoomedOffset based on zoomLevel
            Vector3 targetOffset = Vector3.Lerp(defaultOffset, zoomedOffset, zoomLevel);
            Vector3 targetPos = player.position + targetOffset;

            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }

        // Old input-based zoom (disabled)
        public void OnCamZoom(InputAction.CallbackContext ctx) { }

        // 👇 Call this from UI Slider (0–1)
        public void SetZoomLevel(float value)
        {
            zoomLevel = Mathf.Clamp01(value);
        }
    }
}