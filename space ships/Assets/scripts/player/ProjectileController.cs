using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameEventsSO gameEvents;

    private List<GameObject> projectiles = new();

    private void AddProjectile(GameObject projectile)
    {
        projectiles.Add(projectile);
    }

    private void Update()
    {
        foreach (var projectile in projectiles)
        {
            Transform projectileTranform = projectile.transform;

            projectileTranform.position += 15f * Time.deltaTime * projectileTranform.right;
        }
    }

    private void OnEnable()
    {
        gameEvents.OnProjectileSpawned += AddProjectile;
    }

    private void OnDisable()
    {
        gameEvents.OnProjectileSpawned -= AddProjectile;
    }
}