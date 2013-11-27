//File:      FloatingScript.cs
//Desc:      Causes a script to float by an offset size (e.g. if it's 1.0 it'll go 0.5 up, and then 0.5 down using a gravity value)
//Date:      3/19/2011
//Author(s): Eagan Rackley

using UnityEngine;
using System.Collections;

public class FloatingScript : MonoBehaviour
{
    public float myBoundarySize = 0.01f;
    private const float kVelocityModifier = 0.001f;
    private float myVelocity = 0.001f;
    private float myVelocityModifier = kVelocityModifier;
    private Vector3 myStartingPosition;

    void Awake()
    {
        myStartingPosition = new Vector3();
        myStartingPosition = transform.position;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        myVelocity += myVelocityModifier;
        if (myVelocity > myBoundarySize)
        {
            myVelocityModifier = -(kVelocityModifier);
        }
        else if (myVelocity < -myBoundarySize)
        {
            myVelocityModifier = (kVelocityModifier);
        }

        // Update our position based on the current velocity
        Vector3 newPosition = transform.position;
        newPosition.y += myVelocity;
        newPosition.z = myStartingPosition.z;
        transform.position = newPosition;

    }

}
