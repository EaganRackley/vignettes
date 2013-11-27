using UnityEngine;
using System.Collections;

namespace mms.motion
{
    public struct CollisionInfo
    {
        private float myCollisionAngle;
  
        public CollisionInfo( Collider sprite, Collider other )
        {
            Vector2 spriteVector2d = new Vector2(sprite.transform.position.x, sprite.transform.position.y);
            Vector2 otherVector2d = new Vector2(other.transform.position.x, other.transform.position.y);
            myCollisionAngle = Vector2.Angle(spriteVector2d, otherVector2d);
        }

        /**
         * @property    public float Angle
         * @brief   Gets the angle of collision from sprite collider to other collider specified.
         * @return  The angle of collision.
         */
        public float Angle
        {
            get
            {
                return myCollisionAngle;
            }
        }
    }
}