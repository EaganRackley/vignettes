using UnityEngine;
using System.Collections;

namespace mms.common
{
	
	/// <summary>
	/// Describes values used to process common input operations via trajectory angle of the controller.
	/// </summary>
	public class Common 
	{
		public const float UpAngleLow       = 315.0f;
		public const float UpAngleMid       = 0.0f;
		public const float UpAngleHigh      = 45.0f;
		
		public const float RightAngleLow    = 45.0f;
		public const float RightAngleMid    = 90.0f;
		public const float RightAngleHigh   = 135.0f;
		
		public const float DownAngleLow     = 135.0f;
		public const float DownAngleMid     = 180.0f;
		public const float DownAngleHigh    = 225.0f;
		
		public const float LeftAngleLow     = 225.0f;
		public const float LeftAngleMid     = 270.0f;
		public const float LeftAngleHigh    = 315.0f;

        public const string WALKING_LEFT    = "WalkingL";
        public const string WALKING_RIGHT   = "WalkingR";
        public const string STANDING_LEFT   = "StandingL";
        public const string STANDING_RIGHT  = "StandingR";
        public const string JUMPING_RIGHT   = "JumpingR";
        public const string JUMPING_LEFT    = "JumpingL";
        public const string CLIMBING        = "Climbing";

        public const string WATER_COLLIDER  = "Water";
        public const string GROUND_COLLIDER = "Ground";
        public const string WALL_COLLIDER   = "Wall";

		public const string PLAYER_TAG		= "Player";
	}

}
