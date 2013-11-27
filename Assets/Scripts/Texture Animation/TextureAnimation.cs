//File:      NormalMapAnimation.cs
//Desc:      Handles animation of texture offsets for a normal map texture sheet
//Date:      12/16/2011
//Author(s): Eagan Rackley

using UnityEngine;
using System.Collections;

public class TextureAnimation : MonoBehaviour
{   
    private Vector2 myTextureOffset;
	
	public float myTextureXIncrement = 0.01f;
	public float myTextureYIncrement = 0.0f;
	public float myTimePerFrame = 0.1f;
	private float myTimer = 0.0f;
     
    private Renderer myRenderer;

    void Start()
    {
		
        myRenderer = renderer;
        if (myRenderer == null)
        {
            enabled = false;
        }
        myTextureOffset = new Vector2(-0.0f, -0.0f);
    }

    // Update is called once per frame
    void Update()
    {
		myTimer += Time.deltaTime;
		if(myTimer > myTimePerFrame)
		{
            myTimer = 0.0f;

            myTextureOffset.x += myTextureXIncrement + (myTextureXIncrement * Time.deltaTime);
            myTextureOffset.y += myTextureYIncrement + (myTextureYIncrement * Time.deltaTime);

            //if ((myTextureOffset.x > 0.2f) || (myTextureOffset.x < -0.2f))
			if(myTextureOffset.x > 0.5f )
            {                
                //myTextureXIncrement = -myTextureXIncrement;
                //myTextureOffset.x += myTextureXIncrement;
				myTextureOffset.x = 0;
            }
            if ((myTextureOffset.y > 0.2f) || (myTextureOffset.y < -0.2f))
            {
                myTextureOffset.y = -myTextureOffset.y;
				myTextureOffset.y = 0;
            }

        	myRenderer.material.SetTextureOffset("_MainTex", myTextureOffset);        
		}
    }
}