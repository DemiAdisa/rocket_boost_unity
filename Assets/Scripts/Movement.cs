using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    //InputActions help with control mapping
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction thrustRotation;

    [SerializeField] float thrustStrength = 1000f;
    
    [SerializeField] float rotateStrength = 10f;

    Rigidbody thisRigidBody;

    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody>();
        
    }

    /**
    FixedUpdate is best for any Physics based calculations
    */
    void FixedUpdate()
    {
        ThrustActivate();
        RotationActivate();
    }

    //This is called when a function is enabled(activated)
    void OnEnable()
    {
        thrust.Enable();
        thrustRotation.Enable();

    }

    //Custom Functions
    void ThrustActivate()
    {
        if (thrust.IsPressed())
        {
            thisRigidBody.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

        }
    }

    void RotationActivate()
    {
        float rotationInput = thrustRotation.ReadValue<float>();

        // Rotation to the left
        if (rotationInput < 0)
        {
            transform.Rotate(Vector3.forward * rotateStrength * Time.fixedDeltaTime);
        }
        else if (rotationInput > 0)
        {
            transform.Rotate(-Vector3.forward * rotateStrength * Time.fixedDeltaTime);
        }
    }
}
