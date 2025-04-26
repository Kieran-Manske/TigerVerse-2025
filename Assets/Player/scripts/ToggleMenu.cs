using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public GameObject menu;  // The menu UI element to toggle
    public Transform leftHand; // Left hand transform
    public Transform rightHand; // Right hand transform
    public float interactionDistance = 3f; // Distance from hand to interact with the menu
    public LayerMask interactableLayer; // Layer for interactable objects

    private bool menuActive = false; // Track if the menu is currently active

    void Update()
    {
        // Check for interaction with left and right hand controllers
        if (Input.GetButtonDown("Fire1")) // Left hand input (change to your input mapping)
        {
            ToggleMenuVisibility(leftHand);
        }

        if (Input.GetButtonDown("Fire2")) // Right hand input (change to your input mapping)
        {
            ToggleMenuVisibility(rightHand);
        }
    }

    void ToggleMenuVisibility(Transform hand)
    {
        RaycastHit hit;
        // Perform a raycast from the hand to check if it's pointing at the menu
        if (Physics.Raycast(hand.position, hand.forward, out hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.CompareTag("MenuButton"))
            {
                // If raycast hits an interactable object (like a button), toggle the menu
                menuActive = !menuActive;
                menu.SetActive(menuActive);  // Show or hide the menu
            }
        }
    }
}

