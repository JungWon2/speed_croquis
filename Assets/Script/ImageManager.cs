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
	int cnt =0;

	void addImageFile(string fileName)
	{
		string extension = Path.GetExtension (fileName);
		if(false ==imageTypes.Contains (extension))
			return;
		imageList.Add (fileName);
		Debug.Log (fileName);
	}

	int getNumberingCnt()
	{
		int ret = cnt;
		cnt++;
		if (imageList.Count <= cnt)
			cnt = 0;
		return ret;
	}

	// Use this for initialization
	void Start () {

		imageTypes.Add (".png");
		imageTypes.Add (".jpg");

		dirPath = System.Environment.CurrentDirectory + "/image/";
		refresh ();
	}

	void refresh()
	{
		DirectoryInfo di = new DirectoryInfo (dirPath);
		if (di.Exists == false) {
			di.Create ();
		}

		foreach (var file in di.GetFiles()) {
			addImageFile (file.Name);
		}

		for (int i = 0; i < imageList.Count; i++) {
			string temp = imageList[i];
			int randomIndex = UnityEngine.Random.Range(i, imageList.Count);
			imageList[i] = imageList[randomIndex];
			imageList[randomIndex] = temp;
		}

//		foreach (var file in imageList) {
//			Debug.Log (file + "=====");
//		}
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
		int cnt = getNumberingCnt ();

		string fileName = imageList[cnt];
		string filePath = makeFilePath(fileName);

		byte[] fileData = File.ReadAllBytes(filePath);
		Texture2D tex = new Texture2D(2, 2);
		textLog.text = filePath;
		tex.LoadImage(fileData); 

		Rect rect = new Rect(0, 0, tex.width, tex.height);
		//mainImage.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f)); 
		croquis.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f)); 
	}
}
