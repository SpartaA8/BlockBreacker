using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    public void OnMove(InputValue value)
    {
        float inputX = value.Get<Vector2>().normalized.x;
        CallMoveEvent(inputX);
    }

    public void OnFire()
    {
        CallFireEvent();
    }
}
