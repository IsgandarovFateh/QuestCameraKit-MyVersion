using UnityEngine;

public class CoffeeMachineLockController : MonoBehaviour
{
    [Header("Rigidbody")]
    [SerializeField] private Rigidbody targetRigidbody;

    [Header("Components disabled when locked")]
    [SerializeField] private Behaviour[] componentsToDisableWhenLocked;

    private bool isLocked = false;

    private Vector3 lockedPosition;
    private Quaternion lockedRotation;

    private bool originalUseGravity;
    private bool originalIsKinematic;
    private RigidbodyConstraints originalConstraints;

    private void Awake()
    {
        if (targetRigidbody == null)
        {
            targetRigidbody = GetComponent<Rigidbody>();
        }

        if (targetRigidbody != null)
        {
            originalUseGravity = targetRigidbody.useGravity;
            originalIsKinematic = targetRigidbody.isKinematic;
            originalConstraints = targetRigidbody.constraints;
        }
    }

    public void ToggleLock()
    {
        if (isLocked)
        {
            UnlockObject();
        }
        else
        {
            LockObject();
        }
    }

    public void LockObject()
    {
        isLocked = true;

        lockedPosition = transform.position;
        lockedRotation = transform.rotation;

        foreach (Behaviour component in componentsToDisableWhenLocked)
        {
            if (component != null)
            {
                component.enabled = false;
            }
        }

        if (targetRigidbody != null)
        {
            targetRigidbody.linearVelocity = Vector3.zero;
            targetRigidbody.angularVelocity = Vector3.zero;

            targetRigidbody.useGravity = false;
            targetRigidbody.isKinematic = true;
            targetRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        Debug.Log("Coffee machine locked.");
    }

    public void UnlockObject()
    {
        isLocked = false;

        foreach (Behaviour component in componentsToDisableWhenLocked)
        {
            if (component != null)
            {
                component.enabled = true;
            }
        }

        if (targetRigidbody != null)
        {
            targetRigidbody.useGravity = originalUseGravity;
            targetRigidbody.isKinematic = originalIsKinematic;
            targetRigidbody.constraints = originalConstraints;
        }

        Debug.Log("Coffee machine unlocked.");
    }

    private void LateUpdate()
    {
        if (isLocked)
        {
            transform.SetPositionAndRotation(lockedPosition, lockedRotation);
        }
    }
}