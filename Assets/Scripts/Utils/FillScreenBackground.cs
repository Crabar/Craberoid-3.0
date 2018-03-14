using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillScreenBackground : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
	{
		float maxScale;
		
		if (Camera.main.orthographic)
		{
			var sizeY = Camera.main.orthographicSize;
			var sizeX = (float)(sizeY * Camera.main.aspect / 1.6);
			maxScale = Math.Max(sizeX, sizeY);
		}
		else
		{
			var sizeY = 2 * Math.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad) * Camera.main.transform.position.y;
			var sizeX = (float)(sizeY * Camera.main.aspect / 1.6);
			maxScale = (float) Math.Max(sizeX, sizeY);
		}

		transform.localScale = new Vector3(maxScale / 6, maxScale / 6, 1);
	}
}
