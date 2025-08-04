using UnityEngine;

public class ProjectileHitNotifier : MonoBehaviour
{
    [SerializeField] private GameEventsSO gameEvents;
    [SerializeField] private HitDetector hitDetector;

    private void NotifyCollision()
    {
        gameEvents.ProjectileHit(gameObject);
    }

    private void Awake()
    {
        hitDetector.OnHit += NotifyCollision;
    }
}
