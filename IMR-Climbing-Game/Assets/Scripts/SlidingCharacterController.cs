using UnityEngine;

public class SlidingCharacterController : MonoBehaviour
{
    public float slideSpeed = 2f;
    [SerializeField] private HandDirectedJump jumper;
    private GameObject lastCollidedObject;
    private bool isSliding = false;
    private Vector3 slidingVelocity = Vector3.zero;
    private CharacterController characterController;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var hitNormal = hit.normal;
        var angle = Vector3.Angle(Vector3.up, hitNormal);
        isSliding = (angle > hit.controller.slopeLimit && angle <= 90f);

        if (isSliding)
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitNormal);
            slidingVelocity = slopeRotation * new Vector3(hitNormal.x, 0f, hitNormal.z) * slideSpeed;

            var collisionFlags = hit.controller.collisionFlags;
            lastCollidedObject = hit.collider.gameObject;
            if (!(collisionFlags.HasFlag(CollisionFlags.Below) && collisionFlags.HasFlag(CollisionFlags.Sides)))
            {
                jumper.IsSliding = true;
            }
        }
        else
        {
            slidingVelocity = Vector3.zero;
            jumper.IsSliding = false;
            lastCollidedObject = null;
        }
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isSliding)
        {
            characterController.Move(slidingVelocity * Time.deltaTime);
        }

        if (lastCollidedObject != null && !CheckIfStillColliding(lastCollidedObject))
        {
            slidingVelocity = Vector3.zero;
            jumper.IsSliding = false;
            lastCollidedObject = null;
        }
    }

    private bool CheckIfStillColliding(GameObject collidedObject)
    {
        if (collidedObject == null)
            return false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == collidedObject)
            {
                return true;
            }
        }
        return false;
    }
}