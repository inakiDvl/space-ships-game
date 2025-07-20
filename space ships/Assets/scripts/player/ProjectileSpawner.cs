using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private ProjectileContainerSO projectileContainer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int maxProjectiles;

    private void InstantiateProjectiles()
    {
        for (int i = 0; i < maxProjectiles; i++)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.SetActive(false);
            projectileContainer.AddProjectile(projectileInstance);
        }
    }

    private void Awake()
    {
        InstantiateProjectiles();
    }
}
