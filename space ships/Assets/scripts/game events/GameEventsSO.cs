using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Events", menuName = "Scriptable Objects/Game Events")]
public class GameEventsSO : ScriptableObject
{
    public event Action<GameObject> OnProjectileHit;
    public event Action<GameObject> OnAsteroidHurt;

    public void ProjectileHit(GameObject projectile)
    {
        OnProjectileHit?.Invoke(projectile);
    }

    public void AsteroidHurt(GameObject asteroid)
    {
        OnProjectileHit?.Invoke(asteroid);
    }
}
