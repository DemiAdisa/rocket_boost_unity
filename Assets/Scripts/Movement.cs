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

    AudioSource thisAudioPlayer;

    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody>();
        thisAudioPlayer = GetComponent<AudioSource>();
        
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
            // Relative force is used to add force to an object relative to its local axis
            thisRigidBody.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

            // Only play if is ntp already playing
            if (!thisAudioPlayer.isPlaying)
            {
                thisAudioPlayer.Play();
            }

        }
        else
        {
            thisAudioPlayer.Stop();
        }
    }

    void RotationActivate()
    {
        float rotationInput = thrustRotation.ReadValue<float>();

        // Rotation to the left
        if (rotationInput < 0)
        {
            ApplyRotation(rotateStrength);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotateStrength);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        // This will prevent the Rotation in the Rigidbody pyhsics clashing with the applied rotation
        thisRigidBody.freezeRotation = true;

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);

        // Unfreeze
        thisRigidBody.freezeRotation = false;
    }
}
