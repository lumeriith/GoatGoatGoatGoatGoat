using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatNewFPSController : MonoBehaviour {

    //Fix to remove unintended rotations
    public Vector2 mouseSensitivity = new Vector2(2.5f, 2.5f);
    public float gravity = 15f;
    public float stickToGroundVelocity = 1f;
    public float walkSpeed = 5f;

    public float jumpVelocity = 5f;
    public float jumpVelocityPowFactor = 0.7f;

    public float midairControl = 1f;

    private Rigidbody rb;
    private MeshCollider meshCollider;
    private Vector3 extents;

    public bool isGrounded = false;

    public Vector3 velocity;

    public Vector3 realVelocity;
    private GameObject cam;
    public LayerMask test;

    public float cameraShakeAmount = 0f;

    private float cvx;
    private float cvz;
    private bool wantsToJump = false;
    private Vector3 originalOffset;
    void Start () {
        cam = GetComponentInChildren<Camera>().gameObject;
        rb = GetComponent<Rigidbody>();
        meshCollider = GetComponentInChildren<MeshCollider>();
        extents = meshCollider.bounds.extents;
        originalOffset = cam.transform.position - transform.position;

    }
    
    private void FixedUpdate()
    {
        int colliders = Physics.OverlapBox(transform.position - transform.up * extents.y, new Vector3(extents.x / 2f, .1f, extents.z / 2f)).Length;
        isGrounded = colliders > 1;
        if (wantsToJump)
        {
            if (isGrounded)
            {
                rb.AddForce(transform.up * jumpVelocity, ForceMode.VelocityChange);
                wantsToJump = false;
            }
            else
            {
                wantsToJump = false; // Can't jump
            }
        }

        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 newVelocity = Vector3.zero;

        if (isGrounded)
        {
            newVelocity.x = xAxis * walkSpeed;
            newVelocity.z = yAxis * walkSpeed;

        }
        else
        {
            newVelocity = transform.InverseTransformDirection(rb.velocity);
            newVelocity.y = 0;

            if (xAxis != 0)
            {
                newVelocity.x = Mathf.MoveTowards(newVelocity.x, xAxis * walkSpeed, midairControl * Time.deltaTime);
            }
            if (yAxis != 0)
            {
                newVelocity.z = Mathf.MoveTowards(newVelocity.z, xAxis * walkSpeed, midairControl * Time.deltaTime);
            }

        }


        newVelocity = transform.TransformDirection(newVelocity);
        newVelocity.y = rb.velocity.y;

        rb.velocity = newVelocity;

    }



    void Update () {

        if (Input.GetMouseButtonDown(0) && !GameManager.instance.disableIngameControls && !GameManager.instance.disableAiming)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if((Cursor.lockState != CursorLockMode.None) && (GameManager.instance.disableIngameControls|| GameManager.instance.disableAiming))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }



        if (!GameManager.instance.disableIngameControls && !GameManager.instance.disableAiming)
        {
            updateAim();
        }
        
        if (Input.GetButtonDown("Jump") && !GameManager.instance.disableIngameControls && !GameManager.instance.disableMovement)
        {
            wantsToJump = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            cameraShakeAmount = .5f;
        }
        cam.transform.position = transform.position + originalOffset + Random.insideUnitSphere * cameraShakeAmount;
        cameraShakeAmount = Mathf.MoveTowards(cameraShakeAmount, 0, Time.deltaTime);

	}

    private void updateAim()
    {


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
