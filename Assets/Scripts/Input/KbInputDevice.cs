using UnityEngine;
using System.Collections;
using mms.input;
using mms.common;

/// <summary>
/// Implements <see cref=">AbstractInputDevice"/> to provide keyboard based input via <see cref="InputProvider"/>
/// </summary>	
public class KbInputDevice : AbstractInputDevice 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	/// <summary>
	/// Updates the state of myInputData using keyboard input codes
	/// </summary>
	public override void HandleInput()
	{		
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
		{ 
			
			myInputData.Magnitude = 1.0f;
			myInputData.Angle = Common.LeftAngleMid;
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
		{ 
			myInputData.Magnitude = 1.0f;
			myInputData.Angle = Common.RightAngleMid;
		}
		else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
		{
			myInputData.Magnitude = 1.0f;
			myInputData.Angle = Common.UpAngleMid;
		}
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
		{
			myInputData.Magnitude = 1.0f;
			myInputData.Angle = Common.DownAngleMid;
		}
		else
		{
			myInputData.Magnitude = 0.0f;
		}

		if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E)) 
		{
			myInputData.JumpState = JUMP_STATE.ACTIVE;
		}
		else
		{
			myInputData.JumpState = JUMP_STATE.INACTIVE;
		}
	}
}
