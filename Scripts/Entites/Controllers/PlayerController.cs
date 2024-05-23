using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<float> OnMoveEvent;
    public event Action OnFireEvent;

    public void CallMoveEvent(float input)
    {
        OnMoveEvent?.Invoke(input);
    }

    public void CallFireEvent()
    {
        OnFireEvent?.Invoke();
    }
}
