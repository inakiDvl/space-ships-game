using System;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private PlayerInputsSO playerInputs;
    [SerializeField] private GameEventsSO gameEvents;
    [SerializeField] private GameObject projectilePrefab;

    private void FireProjectile()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        gameEvents.ProjectileSpawned(projectileInstance);
    }

    private void OnEnable()
    {
        playerInputs.OnFire += FireProjectile;
    }

    private void OnDisable()
    {
        playerInputs.OnFire -= FireProjectile;
    }
}
