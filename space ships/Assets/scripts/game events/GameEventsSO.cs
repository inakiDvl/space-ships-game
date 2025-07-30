using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Events", menuName = "Scriptable Objects/Game Events")]
public class GameEventsSO : ScriptableObject
{
    public event Action<GameObject> OnProjectileCollided;

    public void ProjectileCollided(GameObject projectile)
    {
        OnProjectileCollided?.Invoke(projectile);
    }
}
