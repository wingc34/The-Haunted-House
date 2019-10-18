using UnityEngine;
using System.Collections;
using CnControls;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
//[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.5f;				// a public setting for overall animator animation speed
	public float lookSmoother = 3f;			// a smoothing setting for camera motion
    public float turnSmoothing = 15f;       // A smoothing value for turning the player.
    public float speedDampTime = 1f;        // The damping for the speed parameter

    private Animator anim;					// A reference to the animator on the character
    private ChangeMaterial changeMaterial;  // A reference to changed material on the character
    private float oldH, oldV;               // Old position of Horizontal and Vertical from input

    private const float amplitube = 1f;     //scalar
    private const float freqeuncy = 1f;   //in Hz


    private enum cloths {
        Zombie,
        Mummy,
        Pumpkin,
        Skeleton,
        Joker,
        Frankenstein,
        NumberOfTypes
    };

    void Start ()
	{
		// initialising reference variables
		anim = gameObject.GetComponent<Animator>();					  
        changeMaterial = gameObject.GetComponent<ChangeMaterial>();
    }
	
	
	void FixedUpdate ()
	{
		float h = CnInputManager.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
		float v = CnInputManager.GetAxis("Vertical");				// setup v variables as our vertical input axis
        Vector3 offsetVector = Vector3.zero;

        switch (changeMaterial.changedClothIndex) { // default speed is 2.5
            case (int)cloths.Zombie: // faster
                resetOldHV();
                speed = 3.0f;
                break;
            case (int)cloths.Mummy: // slowest
                resetOldHV();
                speed = 1.8f;
                break;
            case (int)cloths.Pumpkin: // default speed, but anti orientation movement
                resetOldHV();
                speed = 2.5f;
                h = -h;
                v = -v;
                break;
            case (int)cloths.Skeleton: // fastest
                resetOldHV();
                speed = 3.5f;
                break;
            case (int)cloths.Joker: // slower, walking on a WAVE path
                resetOldHV();
                speed = 2.0f;
                float offsetH = amplitube * v * Mathf.Cos(Time.time * freqeuncy * 2f * Mathf.PI);
                float offsetV = amplitube * h * Mathf.Sin(Time.time * freqeuncy * 2f * Mathf.PI);
                offsetVector = new Vector3(offsetH, 0f, offsetV);
                break;
            case (int)cloths.Frankenstein: // slower, non-stop movement
                speed = 2.2f;
                if(h != 0f || v != 0f) {
                    oldH = h;
                    oldV = v;
                } else {
                    h = oldH;
                    v = oldV;
                }
                break;
        }
        
        MovementManagement(h, v, offsetVector);	
		anim.speed = speed;								// set the speed of our animator to the public variable 'animSpeed'

	}

    void MovementManagement(float horizontal, float vertical, Vector3 offsetVector)
    {

        // If there is some axis input...
        if (horizontal != 0f || vertical != 0f) {
            // ... set the players rotation and the speed
            Rotating(horizontal, vertical, offsetVector);
            anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
        } else {
            // Otherwise set the speed parameter to 0.
            anim.SetFloat("Speed", 0);

        }
    }

    void Rotating(float horizontal, float vertical, Vector3 offsetVector)
    {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical) + offsetVector; // offset vector is to modify the movement path
        if(targetDirection != Vector3.zero)
        {
            // Create a rotation based on this new vector assuming that up is the global y axis.
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            // Create a rotation that is an increment closer to the target rotation from the player's
            //rotation.
            Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
            // Change the players rotation to this new rotation.
            GetComponent<Rigidbody>().MoveRotation(newRotation);
        }
    }

    void OnTriggerStay(Collider other)
    {
		if (other.gameObject.name == "Customer(Clone)" && (CnInputManager.GetButtonDown("Action") || Input.GetKeyDown("l")) )
        {
            anim.SetBool("Scaring", true);
            StartCoroutine(timeout());
            RandomScareThing scaredthing = other.gameObject.GetComponent<RandomScareThing>();
            CustomerAI customer = other.gameObject.GetComponent<CustomerAI>();
            if(scaredthing.isMatchScareThing(changeMaterial.changedClothIndex))
            {
                customer.isScared();
                //print("Scared");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        anim.SetBool("Scaring", false);
    }

    IEnumerator timeout()
    {  
        yield return new WaitForSeconds(2);
        anim.SetBool("Scaring", false);
    }

    private void resetOldHV()
    {
        oldH = 0;
        oldV = 0;
    }

}