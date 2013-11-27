using UnityEngine;
using System.Collections;
using mms.common;
using mms.input;

namespace mms.motion
{

    /**
     * @class   SpriteMotion
     * @brief   Used to store values used to handle common sprite motion.
     * @author  Eagan
     * @date    9/17/2013
     */
    public class SpriteMotion : MonoBehaviour
    {
        private string myID;
        private Vector2 myVelocity;
        private float myMaxVelocity;
        private float myUnmodifiedMaxVelocity;
        private float myMovementLength;
        private float myAccelerationSpeed;
        private float myDrag;
        private float myGravity;
        private float myJumpVelocity;
        private float myLastAngle;
        private bool mySpriteIsOnGround = true;

        public SpriteMotion(string id, Vector2 velocity, float maxVelocity, float accelerationSpeed, float drag, float gravity, float jumpVelocity)
        {
            myID = id;
            myVelocity = velocity;
            myMaxVelocity = maxVelocity;
            myUnmodifiedMaxVelocity = maxVelocity;
            myAccelerationSpeed = accelerationSpeed;
            myDrag = drag;
            myGravity = gravity;
            myJumpVelocity = jumpVelocity;
            myMovementLength = 0.0f;
        }

        public string ID
        {
            get { return myID; }
            set { myID = value; }
        }

        public Vector2 Velocity
        {
            get { return myVelocity; }
            set { myVelocity = value; }
        }

        public float MaxVelocity
        {
            get { return myMaxVelocity; }
            set { myMaxVelocity = value; }
        }

        public float AccelerationSpeed
        {
            get { return myAccelerationSpeed; }
            set { myAccelerationSpeed = value; }
        }

        public float Drag
        {
            get { return myDrag; }
            set { myDrag = value; }
        }

        public float Gravity
        {
            get { return myGravity; }
            set { myGravity = value; }
        }

        /**
         * @fn  void HandleInputAcceleration()
         * @brief   Modifies the movement length by the acceleration speed depending on the current
         *          values in max velocity @see ProcessMagnitude.
         * @author  Eagan
         * @date    9/18/2013
         * ### param    angle   The angle.
         */
        void HandleInputAcceleration()
        {
            if (myMovementLength < myMaxVelocity)
            {
                myMovementLength += myAccelerationSpeed;
            }
        }

        /**
         * @fn  void ProcessMagnitude(float magnitude)
         * @brief   Adjust the maximum velocity that can be achieved based on
         * 			the magnitude of the controller (usually 0.0f - 1.0f)
         * 			so that if the controller is only slightly pressed
         * 			the sprite will move more slowly.
         * @author  Eagan
         * @date    9/18/2013
         * @param   magnitude   The magnitude of the controller input.
         */
        void ProcessChangesInMagnitude(float magnitude)
        {
            myMaxVelocity = (myUnmodifiedMaxVelocity * magnitude);
            myMaxVelocity = (myUnmodifiedMaxVelocity * magnitude);
        }

        /**
         * @fn  private float AdjustVelocityByDrag(float velocity, float drag)
         * @brief   Adjusts the specified velocity by the specified drag.
         * @author  Eagan
         * @date    9/26/2013
         * @param   velocity    The velocity.
         * @param   drag        The drag.
         * @return  .
         */
        private float AdjustVelocityByDrag(float velocity, float drag)
        {
            if (velocity > 0.0f)
            {
                velocity -= drag;
            }
            else if (velocity < 0.0f)
            {
                velocity += drag;
            }
            else
            {
                velocity = 0.0f;
            }
            return velocity;
        }

        /**
         * @fn  void HandleJumpStateActivation(JUMP_STATE jumpState)
         * @brief   Handle jump state activation.
         * @author  Eagan
         * @date    10/21/2013
         * @param   jumpState   State of the jump.
         */
        void HandleJumpStateActivation(JUMP_STATE jumpState)
        {
            if (jumpState == JUMP_STATE.PENDING)
            {
                myVelocity.y = myJumpVelocity;
                jumpState = JUMP_STATE.ACTIVE;
            }
        }

        /**
         * @fn  void ApplyChangesInVelocity(float angle, float magnitude)
         * @brief   Applies the changes in velocity.
         * @author  Eagan
         * @date    9/19/2013
         * @param   angle           The current angle.
         * @param   magnitude       The magnitude.
         * ### param    jumpState   State of jumping that a sprint may be in.
         */
        void ApplyChangesInVelocity(float angle, float magnitude)
        {
            if (myMovementLength > 0.0f)
            {
                myMovementLength -= myDrag;
            }
            else
            {
                myMovementLength = 0.0f;
            }

            if (magnitude > 0.15f)
            {
                float adjustedAngle = angle + 180.0f;
                if (adjustedAngle > 360.0f) adjustedAngle -= 360.0f;
                myLastAngle = adjustedAngle;
                float angleRads = (Mathf.Deg2Rad * adjustedAngle);
                float targetXVelocity = (myMovementLength * Mathf.Cos(angleRads)) * Time.deltaTime;
                //float targetYVelocity = (myMovementLength * Mathf.Sin(angleRads)) * Time.deltaTime;

                // print("Angle: " + adjustedAngle.ToString() + " Rads: " + angleRads.ToString() + " X/X: "
                //     + myVelocity.x.ToString() + "/" + targetXVelocity.ToString() );

                // Change our x velocity based on the target x for our angle
                ApplyTargetVelocity(targetXVelocity);
            }
            else
            {
                myVelocity.x = AdjustVelocityByDrag(myVelocity.x, myDrag);
            }

            // Handle gravity and changes in y velocity
            if (mySpriteIsOnGround == false)
            {
                myVelocity.y += myGravity;
            }
            else
            {
                myVelocity.y = 0.0f;
            }

        }

        /**
         * @brief   Applies the target velocity described by targetXVelocity.
         *
         * @author  Eagan
         * @date    11/13/2013
         *
         * @param   targetXVelocity Target x coordinate velocity.
         */
        private void ApplyTargetVelocity(float targetXVelocity)
        {
            if (myVelocity.x < targetXVelocity)
            {
                myVelocity.x += myAccelerationSpeed;
            }
            else if (myVelocity.x > targetXVelocity)
            {
                myVelocity.x -= myAccelerationSpeed;
            }
        }

        /**
         * @brief   Handle wall collision.
         *
         * @author  Eagan
         * @date    10/21/2013
         *
         * @param   angle   The current angle.
         * @param   sprite  The sprite object.
         * @param   other   The other angle.
         */
        public void HandleWallCollision(float angle, Collider sprite, Collider other)
        {
            CollisionInfo collisionInfo = new CollisionInfo(sprite, other);

            // Check to see where the wall is relative to the sprite and its current angle of movement and adjust velocity accordingly.
            float angleModification = 0.0f;

            // If collision is to the left of the player.
            if (collisionInfo.Angle >= Common.LeftAngleLow && collisionInfo.Angle < Common.LeftAngleHigh)
            {
                print("Left Collision");
                angleModification = Common.RightAngleMid;
            }
            // If collision is to the right of the player.
            else if (collisionInfo.Angle >= Common.RightAngleLow && collisionInfo.Angle < Common.RightAngleHigh)
            {
                print("Right Collision");
                angleModification = Common.LeftAngleMid;
            }
            // If collision is above the player.
            else if (collisionInfo.Angle >= Common.UpAngleLow && collisionInfo.Angle < Common.UpAngleHigh)
            {
                print("Above Collision");
                angleModification = Common.DownAngleMid;
            }
            // If collision is below the player.
            else if (collisionInfo.Angle >= Common.DownAngleLow && collisionInfo.Angle < Common.DownAngleHigh)
            {
                print("Below Collision");
                angleModification = Common.UpAngleMid;
            }
            else
            {
                print("Herp Derp Collision");
            }

            float adjustedAngle = angle + angleModification;
            if (adjustedAngle > 360.0f) adjustedAngle -= 360.0f;
            float angleRads = (Mathf.Deg2Rad * adjustedAngle);
            float targetXVelocity = (myMovementLength * Mathf.Cos(angleRads)) * Time.deltaTime;
            //float targetYVelocity = (myMovementLength * Mathf.Sin(angleRads)) * Time.deltaTime;                                    
            ApplyTargetVelocity(targetXVelocity);
        }

        /**
         * @fn  public void HandleCollision( Collider sprite, Collider other )
         * @brief   Handle collision with other collider objects associated with tags/layers.
         * @author  Eagan
         * @date    10/1/2013
         * @param   sprite  The sprite we're handling collision/motion for.
         * @param   other   The sprite/object we're colliding with.
         */
        public void HandleCollision(Collider sprite, Collider other)
        {
            print("HandleCollision called with tag: " + other.tag);
            if ((other.tag == Common.WALL_COLLIDER) || (other.tag == Common.GROUND_COLLIDER))
            {                
                HandleWallCollision(myLastAngle, sprite, other);                
            }
            else if (other.tag == Common.WATER_COLLIDER)
            {
                //myVelocity.x /= 2.0f;
            }

        }

        /**
         * @fn  public Vector3 ProcessMotion(Vector3 spritePosition, InputProvider inputProvider)
         * @brief   Handles processing motion based on the values SpriteMotion were created with.
         * @author  Eagan
         * @date    9/18/2013
         * @param   spritePosition  The sprite transform.position value.
         * @param   inputProvider   The angle the controller is pointing.
         * @return  Vector3 specifying the new position of the sprite.         
         */
        public Vector3 ProcessMotion(Vector3 spritePosition, InputProvider inputProvider)
        {
            Vector3 returnValue = new Vector3(spritePosition.x, spritePosition.y, spritePosition.z);

            HandleInputAcceleration();
            ProcessChangesInMagnitude(inputProvider.Data.Magnitude);
            ApplyChangesInVelocity(inputProvider.Data.Angle, inputProvider.Data.Magnitude);
            HandleJumpStateActivation(inputProvider.Data.JumpState);

            returnValue.x += myVelocity.x;
            returnValue.y += myVelocity.y;

            return returnValue;
        }
    }
}