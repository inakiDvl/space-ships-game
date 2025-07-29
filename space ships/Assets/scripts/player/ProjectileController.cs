using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour, IUpdateable
{
    [SerializeField] private PlayerInputsSO playerInputs;
    [SerializeField] private GameObject proyectilePrefab;
    [SerializeField] private float screenMargin;
    [SerializeField] private int proyectileCount;
    [SerializeField] private float projectileSpeed;
    

    private List<GameObject> projectiles = new();

    private List<GameObject> movingProjectiles = new();
    private List<GameObject> removeMovingProjectileQueue = new();

    private float screenBorderX;

    public void DoUpdate(float deltaTime)
    {
        MoveProjectiles();
    }

    private void FireProjectile()
    {
        if (projectiles.Count == 0)
        {
            var newProjectile = Instantiate(proyectilePrefab);
            newProjectile.SetActive(false);
            projectiles.Add(newProjectile);
        }

        int lastIndex = projectiles.Count - 1;
        var projectile = projectiles[lastIndex];

        projectile.SetActive(true);
        projectile.transform.position = transform.position;

        projectiles.RemoveAt(lastIndex);
        movingProjectiles.Add(projectile);
    }

    private void MoveProjectiles()
    {
        foreach (var projectile in removeMovingProjectileQueue)
        {
            projectile.SetActive(false);
            movingProjectiles.Remove(projectile);
            projectiles.Add(projectile);
        }

        removeMovingProjectileQueue.Clear();

        foreach (var projectile in movingProjectiles)
        {
            Transform projectileTranform = projectile.transform;

            projectileTranform.position += projectileSpeed * Time.deltaTime * projectileTranform.right;

            if (projectileTranform.position.x > screenBorderX)
                removeMovingProjectileQueue.Add(projectile);
        }
    }

    private void InstantiateProjectiles()
    {
        for (int i = 0; i < proyectileCount; i++)
        {
            var projectile = Instantiate(proyectilePrefab);
            projectile.SetActive(false);
            projectiles.Add(projectile);
        }
    }

    private void CalculateScreenBorder()
    {
        Camera mainCamera = Camera.main;
        float mainCameraZ = MathF.Abs(mainCamera.transform.position.z);
        screenBorderX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCameraZ)).x;
        screenBorderX += screenMargin;
    }

    private void Awake()
    {
        Updater.Instance.AddUpdateable(this);
        
        InstantiateProjectiles();
        CalculateScreenBorder();
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
