using UnityEngine;

public class HurtBox : MonoBehaviour
{
    private Collider hurtCollider;
    
    private void SetupCollider()
    {
        hurtCollider = GetComponent<Collider>();
        hurtCollider.isTrigger = true;
    }

    private void Awake()
    {
        SetupCollider();
    }

    private void Start()
    {
        CollisionManager.Instance.AddCollider(hurtCollider);
    }
}
