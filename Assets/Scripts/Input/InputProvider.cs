using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using mms.input;

public class InputProvider : MonoBehaviour 
{
	public bool ProcessKeyboardInput = true;
	public bool ProcessTouchInput = false;	
	private List<AbstractInputDevice> myInputDevices = new List<AbstractInputDevice>();
	private InputData myCurrentInputData;
	
	// Use this for initialization
	void Start () 
	{		
		if( ProcessKeyboardInput == true )
		{			
			AbstractInputDevice keyboardInput = this.gameObject.AddComponent<KbInputDevice>();
			myInputDevices.Add(keyboardInput);
		}
		if( ProcessTouchInput == true )
		{
			// TODO: Instantiate touch object
		}
	}
	
	/// <summary>
	/// Gets input information aggregated from the supported devices that were instantiated in the InputProvider.
	/// </summary>
	/// <value>
	/// Data used to process input.
	/// </value>
	public InputData Data
	{
		get
		{
			return myCurrentInputData;
		}
	}
		
	// Update is called once per frame
	void Update () 
	{
		foreach( AbstractInputDevice device in myInputDevices)
		{
			device.HandleInput();
			// TODO: Sort through all of the input controllers and determine what the state of input should be...
			myCurrentInputData = device.Data;
		}
	}
}
