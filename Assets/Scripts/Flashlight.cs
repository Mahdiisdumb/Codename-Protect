using UnityEngine;
using UnityEngine.InputSystem;
public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    public float duration = 0.1f;
    public float distanceFromCamera = 2f;
    private float timer = 0f;
    private Camera cam;
    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f;
    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 targetPos = transform.position;
        if (Mouse.current != null)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            targetPos = cam.ScreenToWorldPoint(new Vector3(
                mouseScreenPos.x,
                mouseScreenPos.y,
                distanceFromCamera
            ));

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Flash();
            }
        }
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            var touch = Touchscreen.current.primaryTouch;
            Vector2 touchPos = touch.position.ReadValue();
            targetPos = cam.ScreenToWorldPoint(new Vector3(
                touchPos.x,
                touchPos.y,
                distanceFromCamera
            ));
            if (touch.press.wasPressedThisFrame)
            {
                if (Time.time - lastTapTime < doubleTapThreshold)
                {
                    Flash();
                }
                lastTapTime = Time.time;
            }
        }
        transform.position = targetPos;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                flashlight.enabled = false;
        }
    }
    private void Flash()
    {
        flashlight.enabled = true;
        timer = duration;
    }
}