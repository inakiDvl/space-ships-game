using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private PlayerInputsSO playerInputs;
    [Header("Projectiles")]
    [SerializeField] private ProjectileContainerSO projectileContainer;

    private List<GameObject> projectilesToControl = new();
    private List<GameObject> removeQueue = new();

    private float maxX;
    private float maxY;

    private int nextProjectileIndex = 0;

    private void FireProjectile()
    {
        if (nextProjectileIndex == projectileContainer.GetProjectileCount())
            nextProjectileIndex = 0;

        GameObject projectile = projectileContainer.GetProjectile(nextProjectileIndex);

        if (projectilesToControl.Contains(projectile))
            return;

        projectilesToControl.Add(projectile);

        projectile.transform.position = transform.position;
        projectile.SetActive(true);
        
        nextProjectileIndex++;
    }

    private void Update()
    {
        foreach (var projectileToRemove in removeQueue)
        {
            projectilesToControl.Remove(projectileToRemove);
            projectileToRemove.SetActive(false);
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