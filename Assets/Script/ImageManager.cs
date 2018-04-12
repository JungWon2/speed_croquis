using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour {

	public UnityEngine.UI.Image bg;
	// Use this for initialization
	void Start () {
		System.IO.DirectoryInfo di = System.IO.DirectoryInfo.GetFiles();
		foreach(var file in di.GetFiles())
			Debug.Log (file.Name);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
