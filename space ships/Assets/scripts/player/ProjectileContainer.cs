using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Proyectile Container", menuName = "Scriptable Objects/Proyectile Container")]
public class ProjectileContainerSO : ScriptableObject
{
    private List<GameObject> projectiles = new();

    public void AddProjectile(GameObject projectile)
    {
        projectiles.Add(projectile);
    }

    public GameObject GetProjectile(int index)
    {
        return projectiles[index];
    }

    public int GetProjectileCount()
    {
        return projectiles.Count;
    }

    private void OnEnable()
    {
        projectiles.Clear();
    }
}
