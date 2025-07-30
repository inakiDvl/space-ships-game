using System;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public event Action OnCollision;

    private CollisionManager collisionManager;
    private Collider hitBox;
    private Rigidbody body;

    private void Setup()
    {
        hitBox = GetComponent<Collider>();
        body = GetComponent<Rigidbody>();

        hitBox.isTrigger = true;
        body.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collisionManager.CanCollide(other))
            OnCollision.Invoke();
    }

    private void Awake()
    {
        Setup();
    }

    private void Start()
    {
        collisionManager = CollisionManager.Instance;
    }
}