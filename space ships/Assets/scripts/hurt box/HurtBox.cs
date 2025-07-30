using UnityEngine;

public class HurtBox : MonoBehaviour
{
    private Collider hurtCollider;
    
    private void Setup()
    {
        hurtCollider = GetComponent<Collider>();
        hurtCollider.isTrigger = true;
    }

    private void Awake()
    {
        Setup();
    }

    private void Start()
    {
        CollisionManager.Instance.AddCollider(hurtCollider);
    }
}
