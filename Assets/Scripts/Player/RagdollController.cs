using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private void Start()
    {
        SetColliderState(false);
        SetRigidBodyState(true);
    }

    public void OnDeath()
    {
        GetComponent<Animator>().enabled = false;
        SetColliderState(true);
        SetRigidBodyState(false);
    }
    
    public void OnRespawn()
    {
        GetComponent<Animator>().enabled = true;
        SetColliderState(false);
        SetRigidBodyState(true);
    }

    private void SetRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach(var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
    }

    private void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach(var collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }
}
