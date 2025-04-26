using UnityEngine;
using VirtualGrasp;  // Gleechi SDK namespace for hand tracking

public class ToggleMenu : MonoBehaviour
{
    public GameObject menuCanvas;  // The menu Canvas you want to toggle
    public string toggleGesture = "Pinch";  // Use this gesture to toggle (or any other gesture)

    public VG_Hand leftHand, rightHand;

    void Start()
    {
        // Get the left and right hand objects from Gleechi's avatar system
        leftHand = VG_Avatar.Instance.leftHand;
        rightHand = VG_Avatar.Instance.rightHand;
    }

    void Update()
    {
        // Check for pinch gesture on either hand
        if (IsPinching(leftHand) || IsPinching(rightHand))
        {
            ToggleVisibility();
        }
    }

    // Check if the hand is performing a pinch gesture
    bool IsPinching(VG_Hand hand)
    {
        return hand.isPinching;  // Gleechi provides this method for pinch detection
    }

    void ToggleVisibility()
    {
        // Toggle the menu visibility
        menuCanvas.SetActive(!menuCanvas.activeSelf);
    }
}
