using System;
using UnityEngine;

public class ResetEventContainer : MonoBehaviour
{
    public event Action OnReset;

    public void Reset()
    {
        OnReset?.Invoke();
    }
}
