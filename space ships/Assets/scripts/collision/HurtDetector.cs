using System;
using UnityEngine;

public class HurtDetector : MonoBehaviour
{
    public event Action OnHurt;

    [SerializeField] private Collider hurtCollider;

    private void Awake()
    {
        hurtCollider.isTrigger = true;
    }

    private void Start()
    {
        CollisionManager.Instance.AddCollider(hurtCollider);
    }
}