using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Events", menuName = "Scriptable Objects/Game Events")]
public class GameEventsSO : ScriptableObject
{
    public event Action<GameObject> OnProjectileEnabled;

    public void ProjectileEnabled(GameObject projectile)
    {
        OnProjectileEnabled?.Invoke(projectile);
    }
}
