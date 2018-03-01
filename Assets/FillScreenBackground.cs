using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillScreenBackground : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
	{
		var sizeY = Camera.main.orthographicSize;
		var sizeX = (float)(sizeY * Camera.main.aspect / 1.6);
		var maxScale = Math.Max(sizeX, sizeY);
		transform.localScale = new Vector3(maxScale / 6, maxScale / 6, 1);
	}
}
