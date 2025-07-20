using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContainer : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int maxProjectiles;

    public List<GameObject> Projectiles { get; private set; } = new();

    private void InstantiateProjectiles()
    {
        for (int i = 0; i < maxProjectiles; i++)
        {
            var projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.SetActive(false);
            Projectiles.Add(projectileInstance);
        }
    }

    private void Awake()
    {
        InstantiateProjectiles();
    }
}
