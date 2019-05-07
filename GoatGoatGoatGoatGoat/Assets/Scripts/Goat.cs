using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goat : MonoBehaviour {

    // There can be only one goat. S I N G L E T O N  D E S I G N
    public static Goat instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Goat>();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    private static Goat _instance;

    private SkinnedMeshRenderer skinnedMeshRenderer;
    public GameObject cam;
    private CharacterController characterController;
    public GoatFPSController goatFPSController;
    public GoatBleater goatBleater;
    public LayerMask whatIsInteractable;
    public LayerMask whatIsEdible;
    public LayerMask whatIsMovable;
    public LayerMask whatBlocksRaycast;

    public float interactDistance = 2f;
    public float grabDistance = 2.5f;
    public float eatDistance = 10f;
    public float throwVelocityModifier = 1.5f;

    public GameObject interactionTarget;
    public Food eatTarget;
    public Rigidbody grabbingObject;
    public Food eatingObject;
    public bool cantInteract = false;

    private bool didHaveProblemWithTransfer = false;
    public bool isDead = false;

    public float eatSpeed = 0.5f;
    public GameObject gib;


    [Header("Weight Settings")]
    public float currentWeight = 10f;
    public float minWeight = 1f;
    public float maxWeight = 100f;


    private GameObject lastInteractionTarget;
    private float gibPerSecond = 60;


    public float size
    {
        get
        {
            return transform.localScale.x;
        }
    }

    public float grabbedObjectOriginalMass;




    public void Kill()
    {

        isDead = true;
    }
    private void Awake()
    {
        cam = GetComponentInChildren<Camera>().gameObject;
        characterController = GetComponent<CharacterController>();
        goatBleater = GetComponent<GoatBleater>();
        goatFPSController = GetComponent<GoatFPSController>();
        SetProperScale();
        
    }
    private void Start()
    {
        goatBleater.Bleat(); // Battlecry
    }
    private void OnValidate()
    {
        SetProperScale();
    }


    private void FixedUpdate()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit interactionHit, eatHit;

        if (GameManager.instance.disableInteraction)
        {
            lastInteractionTarget = null;
            interactionTarget = null;
        }
        else
        {
            if (Physics.Raycast(ray, out interactionHit, interactDistance * size, whatIsInteractable | whatBlocksRaycast, QueryTriggerInteraction.Ignore))
            {
                if(whatBlocksRaycast == (whatBlocksRaycast | (1 << interactionHit.collider.gameObject.layer))){
                    lastInteractionTarget = null;
                    interactionTarget = null;
                }
                else
                {
                    if (lastInteractionTarget == null || lastInteractionTarget != interactionHit.collider.gameObject)
                    {
                        lastInteractionTarget = interactionHit.collider.gameObject;
                        interactionTarget = interactionHit.collider.gameObject;
                        if (whatIsMovable == (whatIsMovable | (1 << interactionTarget.layer)))
                        {
                            if (interactionTarget.GetComponent<Rigidbody>().mass > currentWeight)
                            {
                                cantInteract = true;
                            }
                            else
                            {
                                cantInteract = false;
                            }
                        }
                        else
                        {
                            cantInteract = false;
                        }
                    }
                }
            }
            else
            {
                lastInteractionTarget = null;
                interactionTarget = null;
            }
        }

        if(Physics.Raycast(ray, out eatHit, eatDistance*size, whatIsEdible | whatBlocksRaycast, QueryTriggerInteraction.Ignore))
        {
            if (whatBlocksRaycast == (whatBlocksRaycast | (1 << eatHit.collider.gameObject.layer)))
            {
                eatTarget = null;
            }
            else
            {
                if (eatTarget == null || (eatHit.collider.gameObject != eatTarget.gameObject))
                {
                    eatTarget = eatHit.collider.gameObject.GetComponent<Food>();
                }
            }

            
        }
        else
        {
            eatTarget = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !GameManager.instance.disableIngameControls)
        {
            GameManager.instance.Restart();
        }

        if (Input.GetKeyDown(KeyCode.E) && !GameManager.instance.disableIngameControls)
        {
            if (grabbingObject == null && interactionTarget != null)
            {
                Interactable interactable = interactionTarget.GetComponent<Interactable>();
                if (interactable != null) {
                    interactable.Trigger();
                    
                }
                else
                {
                    Grab();
                }
                
            }
            else if(grabbingObject != null)
            {
                Ungrab();
                goatBleater.Bleat(.7f);
            }
        }

        if(grabbingObject != null)
        {
            StayGrab();
        }


        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !GameManager.instance.disableIngameControls)
        {
            if(grabbingObject != null)
            {
                Rigidbody rb = grabbingObject;
                Ungrab();
                rb.velocity = cam.transform.forward * grabDistance * size * throwVelocityModifier;
                goatBleater.Bleat(.7f);
            }
            else if (eatTarget!= null)
            {
                eatingObject = eatTarget;

            }
        }


        bool fire1 = Input.GetMouseButton(0) && !GameManager.instance.disableIngameControls;
        bool fire2 = Input.GetMouseButton(1) && !GameManager.instance.disableIngameControls;

        if(goatBleater.isSlurping && !(fire1 || fire2))
        {
            goatBleater.StopSlurp();
        }

        if ((fire1 || fire2) && eatingObject != null) // TRANSFER START
        {
            float weightDelta;
            
            if (fire1)
            {
                // EAT, TAKE MASS
                weightDelta = eatingObject.weightTransferPerSecond * Time.deltaTime * -1;
                
            }
            else
            {
                // GIVE MASS
                weightDelta = eatingObject.weightTransferPerSecond * Time.deltaTime;
            }

            if(weightDelta < 0 && GameManager.instance.disableEating)
            {
                weightDelta = 0;
            } else if (weightDelta > 0 && GameManager.instance.disableGiving)
            {
                weightDelta = 0;
            }
            


            if (eatingObject.currentWeight + weightDelta > eatingObject.maxWeight)
            {
                weightDelta = eatingObject.maxWeight - eatingObject.currentWeight;
            }
            else if (eatingObject.currentWeight + weightDelta < eatingObject.minWeight)
            {
                weightDelta = eatingObject.minWeight - eatingObject.currentWeight;
            }

            if (currentWeight + -weightDelta > maxWeight)
            {
                weightDelta = currentWeight - maxWeight;

            } else if (currentWeight + -weightDelta < minWeight)
            {
                weightDelta = currentWeight - minWeight;
            }

            float gibs;

            if(weightDelta != 0)
            {
                goatBleater.StartSlurp();
            }
            else
            {
                goatBleater.StopSlurp();
            }

            if (weightDelta > 0)
            {
                
                for (gibs = Time.deltaTime * gibPerSecond; gibs >= 1; gibs--)
                {
                    Instantiate(gib, transform.position, transform.rotation).GetComponent<Gib>().destination = eatingObject.transform;
                }
                if (Random.value < gibs)
                {
                    Instantiate(gib, transform.position, transform.rotation).GetComponent<Gib>().destination = eatingObject.transform;
                }
                
            }
            else if (weightDelta < 0)
            {
                
                for (gibs = Time.deltaTime * gibPerSecond; gibs >= 1; gibs--)
                {
                    Instantiate(gib, eatingObject.transform.position, transform.rotation).GetComponent<Gib>().destination = Goat.instance.transform;
                }
                if (Random.value < gibs)
                {
                    Instantiate(gib, eatingObject.transform.position, transform.rotation).GetComponent<Gib>().destination = Goat.instance.transform;
                }


                
            }

            if (weightDelta != 0)
            {
                eatingObject.SetWeight(eatingObject.currentWeight + weightDelta);
                
                SetWeight(currentWeight + -weightDelta);
                lastInteractionTarget = null;
            }
            if (!didHaveProblemWithTransfer)
            {
                if ((fire2 && (currentWeight <= minWeight) && !GameManager.instance.disableGiving) || (fire1 && (currentWeight >= maxWeight) && !GameManager.instance.disableEating))
                {
                    didHaveProblemWithTransfer = true;
                    HUDManager.instance.goatDetails.Flash();
                    goatBleater.Bleat(.5f);
                }
                else if ((fire1 && (eatingObject.currentWeight <= eatingObject.minWeight) && !GameManager.instance.disableEating) || (fire2 && (eatingObject.currentWeight >= eatingObject.maxWeight) && !GameManager.instance.disableGiving))
                {
                    didHaveProblemWithTransfer = true;
                    HUDManager.instance.foodIndicator.Flash();
                    goatBleater.Bleat(.5f);
                }
                else
                {
                    didHaveProblemWithTransfer = false;
                }
                
            }

            


            
        }
        else
        {
            didHaveProblemWithTransfer = false;
        }


        if ((Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) && !GameManager.instance.disableIngameControls)
        {
            if(eatingObject != null)
            {
                eatingObject = null;
            }
        }


    }

    void Grab()
    {

        grabbingObject = interactionTarget.GetComponent<Rigidbody>();
        if (grabbingObject.mass > currentWeight)
        {
            HUDManager.instance.WarnWeight();
            HUDManager.instance.goatDetails.Flash();
            HUDManager.instance.foodIndicator.Flash();
            HUDManager.instance.weightIndicator.Flash();
            goatBleater.Bleat(.5f);
            //GameManager.instance.Message(Units.readableText(currentWeight) + "<" + Units.readableText( grabbingObject.mass), 1);
            grabbingObject = null;
            return;
        }
        goatBleater.Bleat(.7f);
        grabbingObject.useGravity = false;
        grabbedObjectOriginalMass = grabbingObject.mass;
        grabbingObject.mass = 0;
        Physics.IgnoreCollision(characterController, grabbingObject.GetComponent<Collider>(), true);
        //grabbingObject.interpolation = RigidbodyInterpolation.Interpolate;

    }

    public void Ungrab()
    {
        if(grabbingObject == null) { return; }
        grabbingObject.useGravity = true;
        grabbingObject.mass = grabbedObjectOriginalMass;
        // grabbingObject.interpolation = RigidbodyInterpolation.None;
        grabbingObject.velocity = Vector3.zero;
        Physics.IgnoreCollision(characterController, grabbingObject.GetComponent<Collider>(), false);
        grabbingObject = null;
        
    }

    void StayGrab()
    {
        if (grabbingObject.mass > currentWeight)
        {
            HUDManager.instance.WarnWeight();
            Ungrab();
            return;
        }
        Vector3 desiredPos = cam.transform.position + cam.transform.forward * size * grabDistance;
        float modifier = Mathf.Min(8f / grabbingObject.mass * currentWeight, 20f);

        grabbingObject.velocity = (desiredPos - grabbingObject.position) * modifier;
        
        grabbingObject.angularVelocity = Vector3.zero;
        float angle = Vector3.Angle(cam.transform.forward, grabbingObject.position - cam.transform.position);
        if (angle > 90) { Ungrab(); }
        // if ((desiredPos - grabbingObject.position).magnitude > 2f * size) { Ungrab(); }
    }
    public void SetWeight(float weight)
    {
        currentWeight = weight;
        SetProperScale();
    }

    public void SetProperScale()
    {

        transform.localScale = Vector3.one * Mathf.Pow(currentWeight / 10f, 1f / 3f);
        if (Application.isPlaying && characterController != null)
        {
            characterController.stepOffset = 0.3f * transform.localScale.x;
            characterController.Move(Vector3.zero);
        }

    }

    public void ApplyCameraShake(float amount)
    {
        goatFPSController.cameraShakeAmount = Mathf.Max(goatFPSController.cameraShakeAmount, amount);

    }
}
