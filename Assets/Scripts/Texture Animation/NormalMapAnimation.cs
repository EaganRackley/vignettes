//File:      NormalMapAnimation.cs
//Desc:      Handles animation of texture offsets for a normal map texture sheet
//Date:      12/16/2011
//Author(s): Eagan Rackley

using UnityEngine;
using System.Collections;

public class NormalMapAnimation: MonoBehaviour

{
	
	
	private Vector2 myNormalOffset;
	private Vector2 myTextureOffset;
	
	public float TextureIncrement = 0.01f;
	public float NormalIncrement = 0.02f;

    private Renderer myRenderer;
    
    void Start ()
    {
		myRenderer = renderer;
        if(myRenderer == null)
		{
            enabled = false;
		}
		myNormalOffset = new Vector2( 0.0f, 0.0f );
		myTextureOffset = new Vector2( 0.0f, 0.0f );
    }
	
    // Update is called once per frame
    void Update()
    {
		myNormalOffset.x += NormalIncrement;
		myTextureOffset.x += TextureIncrement;
		
		if(myNormalOffset.x > 1.0f) myNormalOffset.x = 0.0f;
		if(myTextureOffset.x > 1.0f) myTextureOffset.x = 0.0f;
		
		myRenderer.material.SetTextureOffset ("_MainTex", myTextureOffset);
		myRenderer.material.SetTextureOffset ("_BumpMap", myNormalOffset);
		
    }
}