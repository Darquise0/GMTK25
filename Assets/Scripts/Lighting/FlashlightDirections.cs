using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightDirections : MonoBehaviour
{
    void Update()
    {
        Vector2 inputDir = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) inputDir += Vector2.up;
        if (Keyboard.current.sKey.isPressed) inputDir += Vector2.down;
        if (Keyboard.current.aKey.isPressed) inputDir += Vector2.left;
        if (Keyboard.current.dKey.isPressed) inputDir += Vector2.right;

        if (inputDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(inputDir.y, inputDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }   
    }
}
