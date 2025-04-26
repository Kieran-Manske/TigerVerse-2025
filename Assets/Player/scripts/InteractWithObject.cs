using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    public GameObject hand;  // Reference to the hand from the Gleechi hand rig
    public GameObject interactableObject;  // The object to interact with
    private bool isInteracting = false;
    private GameObject grabbedObject;

    void Update()
    {
        // Detect gestures or inputs from the Gleechi hand rig
        if (IsHandGrabbing(hand) && !isInteracting)
        {
            AttemptGrabObject();  // Try to grab object with hand gesture
        }

        if (isInteracting && IsHandReleased(hand))
        {
            ReleaseObject();  // Release the object when hand is not grabbing anymore
        }
    }

    // This would be a method tied to the hand tracking system, checking if the hand is grabbing
    bool IsHandGrabbing(GameObject hand)
    {
        // Use the Gleechi hand tracking system to detect if the hand is performing a grab gesture
        return hand.GetComponent<GleechiHandGesture>().isGrabbing;  // Example method
    }

    // Check if the hand has released the object
    bool IsHandReleased(GameObject hand)
    {
        // Replace with your actual check for when the hand is no longer grabbing
        return !hand.GetComponent<GleechiHandGesture>().isGrabbing;  // Example method
    }

    // Attempt to grab the object when the hand gesture is a grab
    void AttemptGrabObject()
    {
        RaycastHit hit;

        // Perform a raycast from the hand to find interactable objects
        if (Physics.Raycast(hand.transform.position, hand.transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Interactable"))  // Check if it’s an interactable object
            {
                grabbedObject = hit.collider.gameObject;  // Grab the object
                grabbedObject.transform.SetParent(hand.transform);  // Attach the object to the hand
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;  // Disable physics
                isInteracting = true;  // Set interaction flag
            }
        }
    }

    // Release the object from the hand
    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.SetParent(null);  // Detach the object from the hand
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;  // Re-enable physics
            grabbedObject = null;  // Reset grabbed object
            isInteracting = false;  // Reset interaction flag
        }
    }
}
