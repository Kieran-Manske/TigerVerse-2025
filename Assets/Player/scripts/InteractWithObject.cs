using UnityEngine;
using VirtualGrasp;  // Gleechi namespace for hand interaction

public class InteractWithObject : MonoBehaviour
{
    public VG_Hand hand;  // Assign the hand you're tracking (left or right)
    private GameObject grabbedObject;
    private bool isGrabbing = false;

    void Update()
    {
        // Check if the hand is grabbing something
        if (hand.isGrabbing && !isGrabbing)
        {
            TryGrabObject();
        }
        else if (!hand.isGrabbing && isGrabbing)
        {
            ReleaseObject();
        }
    }

    // Try to grab the object
    void TryGrabObject()
    {
        RaycastHit hit;
        // Cast a ray from the hand to find an interactable object
        if (Physics.Raycast(hand.transform.position, hand.transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Interactable"))  // Ensure it's an interactable object
            {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.SetParent(hand.transform);  // Attach object to hand
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;  // Disable physics while holding
                isGrabbing = true;
            }
        }
    }

    // Release the object when the hand stops grabbing
    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.SetParent(null);  // Detach object from hand
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;  // Enable physics again
            grabbedObject = null;
            isGrabbing = false;
        }
    }
}
