using UnityEngine;
using System.Collections;
using mms.common;
using mms.motion;
using mms.input;


/// <summary>
/// Used to set up and manage the standard character animations based on the specified Input provider. <see cref=">AbstractInputConsumer"/> <see cref=">InputProvider"/>
/// </summary>
[RequireComponent(typeof(OTAnimatingSprite))]
public class CharacterInputConsumer : MonoBehaviour
{
    // Attributes
    public string ID;
    public Vector2 Velocity;
    public float MaxVelocity;
    public float AccelerationSpeed;
    public float Drag;
    public float Gravity;
    public float JumpVelocity;
    
    // Associations
    private OTAnimatingSprite mySprite;
    public InputProvider myInputProvider;
    private SpriteMotion myMovement;


    void Start()
    {
        mySprite = GetComponent<OTAnimatingSprite>();
        myMovement = new SpriteMotion(ID, Velocity, MaxVelocity, AccelerationSpeed, Drag, Gravity, JumpVelocity);
    }

    /// <summary>
    /// Handles collision behaviors based on layers and tags
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        myMovement.HandleCollision( this.collider, other);
    }

    /// <summary>
    /// Handles setting the character animation based on the current character state.
    /// </summary>
    void HandleCharacterAnimation()
    {
        if (myInputProvider.Data == null)
        {
            print("Input provider is null!");
            return;
        }

        //print(myInputProvider.Data.Magnitude.ToString() + "," + myInputProvider.Data.Angle.ToString() + ": " + Common.RightAngleLow.ToString() + ": " + Common.RightAngleHigh.ToString());		
        
        // Handle Horizontal and vertical movement and animation
        if (myInputProvider.Data.Magnitude > 0.0f)
        {
            if ((myInputProvider.Data.Angle >= Common.RightAngleLow) && (myInputProvider.Data.Angle <= Common.RightAngleHigh))
            {
                if (mySprite.animationFrameset != Common.WALKING_RIGHT)
                {
                    if (myInputProvider.Data.JumpState == JUMP_STATE.ACTIVE) { mySprite.Play(Common.JUMPING_RIGHT); }
                    else { mySprite.Play(Common.WALKING_RIGHT); }
                    // Add force based on angle and magnitude...
                }
            }
            else if ((myInputProvider.Data.Angle >= Common.LeftAngleLow) && (myInputProvider.Data.Angle <= Common.LeftAngleHigh))
            {
                if (mySprite.animationFrameset != Common.WALKING_LEFT)
                {
                    if (myInputProvider.Data.JumpState == JUMP_STATE.ACTIVE) { mySprite.Play(Common.JUMPING_LEFT); }
                    else { mySprite.Play(Common.WALKING_LEFT); }
                }
            }
        }
        else
        {            
            if ((myInputProvider.Data.Angle >= Common.RightAngleLow) && (myInputProvider.Data.Angle <= Common.RightAngleHigh))
            {
                mySprite.Play(Common.STANDING_RIGHT);
            }
            if ((myInputProvider.Data.Angle >= Common.LeftAngleLow) && (myInputProvider.Data.Angle <= Common.LeftAngleHigh))
            {
                mySprite.Play(Common.STANDING_LEFT);
            }              
        }        
    }

    /// <summary>
    /// Handles the movement of the character based on public settings and state.
    /// </summary>
    void HandleCharacterMovement()
    {
        if (myInputProvider.Data == null)
        {
            print("Input provider is null!");
            return;
        }        
        Vector3 newPosition = myMovement.ProcessMotion(this.transform.position, myInputProvider);        
        this.transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCharacterAnimation();
        HandleCharacterMovement();
    }

} // end class
