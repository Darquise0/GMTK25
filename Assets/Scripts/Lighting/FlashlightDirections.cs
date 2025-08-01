using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightDirections : MonoBehaviour
{
    [Header("Flashlight Settings")]
    [SerializeField] private Transform flashlight; // The child object that holds the flashlight
    [SerializeField] private GameObject lightUp; // Light that does not include Player layer
    [SerializeField] private GameObject lightRegular; // Light that includes Player layer when facing down
    private Lights lightsController;

    // Position offsets relative to the player
    private readonly Vector2 rightOffset = new Vector2(0.615f, -0.127f);
    private readonly Vector2 leftOffset = new Vector2(-0.568f, -0.127f);
    private readonly Vector2 upOffset = new Vector2(0.242f, -0.009f);
    private readonly Vector2 downOffset = new Vector2(0.242f, -0.009f);

    private void Start()
    {
        lightsController = GetComponentInParent<Lights>();
    }

    private void Update()
    {
        Vector2 inputDir = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) inputDir += Vector2.up;
        if (Keyboard.current.sKey.isPressed) inputDir += Vector2.down;
        if (Keyboard.current.aKey.isPressed) inputDir += Vector2.left;
        if (Keyboard.current.dKey.isPressed) inputDir += Vector2.right;

        if (inputDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(inputDir.y, inputDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

            // Apply position offset and set correct light
            if (inputDir == Vector2.left)
            {
                flashlight.localPosition = leftOffset;
                SetFlashlightLayer(true);
            }
            else if (inputDir == Vector2.right)
            {
                flashlight.localPosition = rightOffset;
                SetFlashlightLayer(true);
            }
            else if (inputDir == Vector2.up)
            {
                flashlight.localPosition = upOffset;
                SetFlashlightLayer(false);
            }
            else if (inputDir == Vector2.down)
            {
                flashlight.localPosition = downOffset;
                SetFlashlightLayer(true); // Include Player layer when facing down
            }
        }
    }

    private void SetFlashlightLayer(bool includePlayer)
    {
        // Toggle the correct flashlight GameObject
        if (lightUp != null) lightUp.SetActive(!includePlayer);
        if (lightRegular != null) lightRegular.SetActive(includePlayer);

        if (lightsController != null)
        {
            lightsController.SetDirectionDown(includePlayer);
        }
    }
}
