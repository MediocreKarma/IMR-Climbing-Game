using UnityEngine;

public class OutOfBoundsChecker : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private LayerMask objectLayerMask;
    [SerializeField] private Transform resetTransform;

    private void Update()
    {
        if (IsCharacterInCollider())
        {
            TeleportToOrigin();
        }
    }

    private bool IsCharacterInCollider()
    {
        Bounds characterBounds = characterController.bounds;
        Collider[] hitColliders = Physics.OverlapBox(characterBounds.center, characterBounds.extents, Quaternion.identity, objectLayerMask);
        return hitColliders.Length > 0;
    }

    private void TeleportToOrigin()
    {
        characterController.transform.position = resetTransform.position;
        characterController.transform.rotation = resetTransform.rotation;
    }
}