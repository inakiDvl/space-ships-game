using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Events", menuName = "Scriptable Objects/Game Events")]
public class GameEventsSO : ScriptableObject
{
    public event Action<GameObject> OnProjectileSpawned;

    public void ProjectileSpawned(GameObject projectile)
    {
        OnProjectileSpawned?.Invoke(projectile);
    }
}
