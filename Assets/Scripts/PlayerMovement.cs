using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Transform PlayerCamera;
    [Space]
    [SerializeField] public float Speed;
    [SerializeField] public float Sensitivity;
    [SerializeField] private float JumpForce;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Sensitivity = SensitivityManager.Sensitivity; 
    }



    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();

    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.linearVelocity = new Vector3(MoveVector.x, PlayerBody.linearVelocity.y, MoveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask))
            {
                PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }

    }

    private void MovePlayerCamera()
    {
        if (!Pause.instance.isPaused)
        {
            xRot -= PlayerMouseInput.y * Sensitivity;
            xRot = Mathf.Clamp(xRot, -90f, 90f);


            transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
            PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        }
    }
    public void SetSensitivity()
    {
        Sensitivity = SensitivityManager.Sensitivity; 
    }
}
