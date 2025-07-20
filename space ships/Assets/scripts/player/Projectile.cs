using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ResetEventContainer resetEventContainer;

    public void Reset()
    {
        resetEventContainer.Reset();
    }

    private void Awake()
    {
        resetEventContainer = GetComponent<ResetEventContainer>();
    }
}
