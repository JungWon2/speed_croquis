using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class ImageManager : MonoBehaviour {

	public UnityEngine.UI.Image bgImage;
	public UnityEngine.UI.Image croquis;
	public UnityEngine.UI.Text textLog;

	HashSet<string> imageTypes = new HashSet<string>();
	List<string> imageList = new List<string>();


	string dirPath;

	void addImageFile(string fileName)
	{
		string extension = Path.GetExtension (fileName);
		if(false ==imageTypes.Contains (extension))
			return;
		imageList.Add (fileName);
		Debug.Log (fileName);
	}

	// Use this for initialization
	void Start () {

		imageTypes.Add (".png");
		imageTypes.Add (".jpg");
		dirPath = Application.persistentDataPath + "/image/";
		Debug.Log ("dir:"+System.Environment.CurrentDirectory);

		Debug.Log (dirPath);
		textLog.text = "dirPath==>" + dirPath;
		DirectoryInfo di = new DirectoryInfo(dirPath);
		if (di.Exists == false)
		{
			di.Create();
		}

		foreach (var file in di.GetFiles()) {
			//Debug.Log (file.Name);	
			addImageFile (file.Name);

		}


	}

	
	// Update is called once per frame
	void Update () {
		
	}

	string makeFilePath(string fileName)
	{
		return dirPath + fileName;
	}
	public void nextImageBtn()
	{
		foreach (var file in imageList) {
			Debug.Log (file);
		}
		Debug.Log ("==========");

		string fileName = imageList [0];
		string filePath = makeFilePath(fileName);
		Debug.Log (filePath);

		Texture2D tex = null;
		byte[] fileData;

		fileData = File.ReadAllBytes(filePath);
		tex = new Texture2D(2, 2);
		textLog.text = filePath;
		tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.

		Rect rect = new Rect(0, 0, tex.width, tex.height);
		//mainImage.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f)); 
		croquis.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f)); 
		//bg.sprite = spriteToUse;
		//FileStream fs = new FileStream(Application.persistentDataPath + "/image/configuration.json", FileMode.CreateNew);
		//byte[] info = new UTF8Encoding(true).GetBytes(source);

		//fs.Write(info, 0, info.Length);
	}
}
