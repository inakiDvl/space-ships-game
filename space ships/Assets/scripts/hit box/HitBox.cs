using UnityEngine;

public class HitBox : MonoBehaviour
{
    private CollisionManager collisionManager;

    private void OnTriggerEnter(Collider other)
    {
        if (collisionManager.CanCollide(other))
            Debug.Log(other.name);
    }

    private void Start()
    {
        collisionManager = CollisionManager.Instance; 
    }
}
