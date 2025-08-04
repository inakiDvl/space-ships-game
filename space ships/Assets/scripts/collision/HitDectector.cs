using System;
using UnityEngine;

public class HitDetector : MonoBehaviour, IUpdateable
{
    public event Action OnHit;

    private CollisionManager collisionManager;
    private Vector3 previousPosition;

    private void DetectCollistion()
    {
        Vector3 currentPosition = transform.position;
        Vector3 direction = currentPosition - previousPosition;

        float distance = direction.magnitude;

        if (distance == 0)
            return;

        if (Physics.Raycast(previousPosition, direction.normalized, out RaycastHit hitInfo, distance))
        {
            if (collisionManager.CanCollide(hitInfo.collider))
                OnHit?.Invoke();
        }

        previousPosition = currentPosition;
    }

    public void DoUpdate(float deltaTime)
    {
        DetectCollistion();
    }

    private void OnEnable()
    {
        previousPosition = transform.position;
    }

    private void Start()
    {
        collisionManager = CollisionManager.Instance;
        UpdateManager.Instance.AddUpdateable(this);
    }
}


