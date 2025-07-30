using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static CollisionManager Instance { get; private set; }

    private Dictionary<Collider, Collider> colliders = new();

    public void AddCollider(Collider collider)
    {
        colliders.Add(collider, collider);
    }

    public bool CanCollide(Collider collider)
    {
        return colliders.ContainsKey(collider);
    }

    private void CreateInstance()
    {
        if (Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        CreateInstance();
    }
}
