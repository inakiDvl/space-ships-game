using UnityEngine;

public class ResetTrail : MonoBehaviour
{
    private ResetEventContainer resetEventContainer;
    private TrailRenderer trailRenderer;
    
    private void Reset()
    {
        trailRenderer.Clear();
    }

    private void Awake()
    {
        resetEventContainer = GetComponent<ResetEventContainer>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        resetEventContainer.OnReset += Reset;
    }

    private void OnDisable()
    {
        resetEventContainer.OnReset -= Reset;
    }
}