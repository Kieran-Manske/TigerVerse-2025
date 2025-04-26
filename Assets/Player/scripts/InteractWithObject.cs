using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
    public Transform leftHand;            // Reference to the left hand transform
    public Transform rightHand;           // Reference to the right hand transform
    public float interactionDistance = 3f; // Distance at which objects can be interacted with
    public LayerMask interactableLayer;   // The layer for interactable objects

    private GameObject leftHandObject = null;  // The object being held by the left hand
    private GameObject rightHandObject = null; // The object being held by the right hand

    private void Update()
    {
        // Check for interaction with the left hand
        if (leftHandObject == null)
        {
            TryGrabObject(leftHand);
        }
        else
        {
            TryReleaseObject(leftHand, leftHandObject);
        }

        // Check for interaction with the right hand
        if (rightHandObject == null)
        {
            TryGrabObject(rightHand);
        }
        else
        {
            TryReleaseObject(rightHand, rightHandObject);
        }
    }

    private void TryGrabObject(Transform hand)
    {
        RaycastHit hit;
        // Perform a raycast from the hand to detect interactable objects
        if (Physics.Raycast(hand.position, hand.forward, out hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                // Grab the object if it's interactable
                GameObject objectToGrab = hit.collider.gameObject;
                objectToGrab.transform.SetParent(hand); // Attach to hand
                objectToGrab.GetComponent<Rigidbody>().isKinematic = true; // Disable physics
                if (hand == leftHand)
                {
                    leftHandObject = objectToGrab;
                }
                else
                {
                    rightHandObject = objectToGrab;
                }
            }
        }
    }

    private void TryReleaseObject(Transform hand, GameObject objectToRelease)
    {
        // Check if the user has released the grip or button for releasing the object
        if (Input.GetButtonDown(hand == leftHand ? "Fire1" : "Fire2")) // Replace with your own button mappings
        {
            objectToRelease.transform.SetParent(null); // Detach from hand
            objectToRelease.GetComponent<Rigidbody>().isKinematic = false; // Re-enable physics
            if (hand == leftHand)
            {
                leftHandObject = null;
            }
            else
            {
                rightHandObject = null;
            }
        }
    }
}
