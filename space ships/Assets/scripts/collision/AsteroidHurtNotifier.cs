using UnityEngine;

public class AsteroidHurtNotifier : MonoBehaviour
{
    [SerializeField] private GameEventsSO gameEvents;
    [SerializeField] private HurtDetector hurtDetector;

    private void NotifyHurt()
    {
        gameEvents.AsteroidHurt(gameObject);
    }

    private void Awake()
    {
        hurtDetector.OnHurt += NotifyHurt;
    }
}