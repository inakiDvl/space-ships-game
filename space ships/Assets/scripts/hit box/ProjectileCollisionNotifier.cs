using UnityEngine;

public class ProjectileCollisionNotifier : MonoBehaviour
{
    [SerializeField] private HitBox hitBox;
    [SerializeField] private GameEventsSO gameEvents;

    private void Awake()
    {
        hitBox.OnCollision += () => gameEvents.ProjectileCollided(gameObject);    
    }
}