using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private PlayerInputsSO playerInputs;

    private ProjectileContainer projectileContainer;

    private List<Projectile> projectilesToControl = new();
    private List<Projectile> removeQueue = new();

    private float maxX;
    private float maxY;

    private int nextProjectileIndex = 0;

    private void FireProjectile()
    {
        if (nextProjectileIndex == projectileContainer.Projectiles.Count)
            nextProjectileIndex = 0;

        Projectile projectile = projectileContainer.Projectiles[nextProjectileIndex];

        if (projectilesToControl.Contains(projectile))
            return;

        projectilesToControl.Add(projectile);
        projectile.transform.position = transform.position;
        
        projectile.gameObject.SetActive(true);

        projectile.Reset();

        nextProjectileIndex++;
    }

    private void Update()
    {
        foreach (var projectileToRemove in removeQueue)
        {
            projectilesToControl.Remove(projectileToRemove);
            projectileToRemove.gameObject.SetActive(false);
        }

        removeQueue.Clear();
        
        foreach (var projectile in projectilesToControl)
        {
            Transform projectileTranform = projectile.transform;

            projectileTranform.position += 15f * Time.deltaTime * projectileTranform.right;

            if (projectileTranform.position.x > maxX)
                removeQueue.Add(projectile);
        }
    }
    
    private void Awake()
    {
        projectileContainer = GetComponent<ProjectileContainer>();
        
        Camera playerCamera = Camera.main;
        float playerCameraZ = MathF.Abs(playerCamera.transform.position.z);
        
        maxY = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, playerCameraZ)).y;
        maxX = playerCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, playerCameraZ)).x;
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