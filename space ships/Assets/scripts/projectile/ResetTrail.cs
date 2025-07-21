using UnityEngine;

public class ResetTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;

    private void OnDisable()
    {
        trailRenderer.Clear();
    }
}
