using UnityEngine;
using UnityEngine.InputSystem; // for new Input System (if you use it)

public class VRPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public Transform headTransform; // assign your Main Camera here
    private CharacterController characterController;
    
    private Vector2 inputAxis;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Read input from the left controller
        inputAxis = new Vector2(
            Input.GetAxis("XRI_Left_Primary2DAxis_Horizontal"),
            Input.GetAxis("XRI_Left_Primary2DAxis_Vertical")
        );
        
        Vector3 move = headTransform.forward * inputAxis.y + headTransform.right * inputAxis.x;
        move.y = 0; // prevent flying
        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
}
