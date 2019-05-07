using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatFPSController : MonoBehaviour {

    
    public Vector2 mouseSensitivity = new Vector2(2.5f, 2.5f);
    public float gravity = 15f;
    public float stickToGroundVelocity = 1f;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpVelocity = 5f;
    public float jumpVelocityPowFactor = 0.7f;

    public float cameraShakeAmount;
    private Vector3 originalOffset;

    public bool isGrounded = false;

    public Vector3 velocity;
    public Vector3 realVelocity;
    private GameObject cam;
    private CharacterController characterController;
    public LayerMask test;

    private float cvx;
    private float cvz;
    private bool wantsToJump = false;

    void Start () {
        cam = GetComponentInChildren<Camera>().gameObject;
        characterController = GetComponent<CharacterController>();
        originalOffset = transform.InverseTransformDirection(cam.transform.position - transform.position);

    }

    private void FixedUpdate()
    {
        
        Vector3 lastPos = transform.position;
        characterController.Move(velocity * Time.deltaTime);
        realVelocity = Vector3.Lerp(realVelocity, (transform.position - lastPos) / Time.deltaTime, .2f);
        
        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position + (transform.up * (-characterController.height / 2f + characterController.radius - 0.01f))*transform.localScale.x, characterController.radius*transform.localScale.x, test, QueryTriggerInteraction.Ignore);
        bool wasGrounded = isGrounded;
        if (colliders.Length > 1)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        Vector2 movementAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (GameManager.instance.disableIngameControls || GameManager.instance.disableMovement)
        {
            movementAxis = new Vector2(0, 0);
        }
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        Vector3 movementVector = (gameObject.transform.forward * movementAxis.y * speed +
            gameObject.transform.right * movementAxis.x * speed) * transform.localScale.x;


        if (isGrounded)
        {
            velocity = movementVector;
            velocity.y = -stickToGroundVelocity * transform.localScale.x;
            if (wantsToJump)
            {
                Goat.instance.goatBleater.Bleat(.5f);
                velocity.y = jumpVelocity * Mathf.Pow(transform.localScale.x, jumpVelocityPowFactor);
                isGrounded = false;
            }
        }
        else
        {
            if (wasGrounded)
            {
                velocity = realVelocity;
            }
            velocity.y -= gravity * Time.deltaTime * transform.localScale.x;
            velocity.x = Mathf.SmoothDamp(velocity.x, movementVector.x, ref cvx, 0.15f, 10f, Time.deltaTime);
            velocity.z = Mathf.SmoothDamp(velocity.z, movementVector.z, ref cvz, 0.15f, 10f, Time.deltaTime);

            /*if(Mathf.Sqrt(velocity.x*velocity.x + velocity.z * velocity.z) < 5f)
            {
                velocity.x = Mathf.SmoothDamp(
            }*/

        }
        wantsToJump = false;

    }



    void Update () {

        if (Input.GetMouseButtonDown(0) && !GameManager.instance.disableIngameControls && !GameManager.instance.disableAiming)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if((GameManager.instance.disableIngameControls|| GameManager.instance.disableAiming))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(Cursor.lockState == CursorLockMode.Locked) { Cursor.visible = false; }


        if (!GameManager.instance.disableIngameControls && !GameManager.instance.disableAiming)
        {
            updateAim();
        }
        
        if (Input.GetButtonDown("Jump") && !GameManager.instance.disableIngameControls && !GameManager.instance.disableMovement)
        {
            wantsToJump = true;
        }

        cam.transform.position = transform.position + transform.TransformDirection(originalOffset) + Random.insideUnitSphere * cameraShakeAmount;
        cameraShakeAmount = Mathf.MoveTowards(cameraShakeAmount, 0, Time.deltaTime);



    }

    private void updateAim()
    {

        Vector3 a = transform.localEulerAngles;

        Vector2 rotateAngle = new Vector2(Input.GetAxis("Mouse X") * mouseSensitivity.x, -Input.GetAxis("Mouse Y") * mouseSensitivity.y);
        float currentPitch = cam.transform.localEulerAngles.x;


        if (currentPitch >= 0 && currentPitch <= 90 && currentPitch + rotateAngle.y >= 89)
        {
            rotateAngle.y = 89 - currentPitch;
        }
        else if (currentPitch <= 360 && currentPitch >= 270 && currentPitch + rotateAngle.y <= 271)
        {
            rotateAngle.y = 271 - currentPitch;
        }

        gameObject.transform.Rotate(transform.up, rotateAngle.x, Space.World);
        cam.transform.Rotate(cam.transform.right, rotateAngle.y, Space.World);
    }


}
