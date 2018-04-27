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
	public UnityEngine.UI.Text textTime;

	int width;
	int height;

	int remaingSec = 65;

	HashSet<string> imageTypes = new HashSet<string>();
	List<string> imageList = new List<string>();

	string dirPath;
	int imgCnt =0;

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
		int ret = imgCnt;
		imgCnt++;
		if (imageList.Count <= imgCnt)
			imgCnt = 0;
		return ret;
	}

	// Use this for initialization
	void Start () {

		imageTypes.Add (".png");
		imageTypes.Add (".jpg");
		textLog.text = "start";
		dirPath = System.Environment.CurrentDirectory + "/image/";
		refresh ();

		StartCoroutine (updateSec());
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

	void endTime()
	{
		nextImage ();
	}

	IEnumerator updateSec()
	{
		while(true)
		{
			remaingSec--;
			if (remaingSec < 0) {
				endTime ();
			} else {
				string displayTime = timeToText (remaingSec);
				textTime.text = displayTime;
			}
			yield return new WaitForSeconds (1);
		
		}
	}

	string timeToText(int sec)
	{
		int minDisplay = sec / 60;
		int secDisplay = sec % 60;

		string ret = String.Format ("{0:D2}:{1:D2}", minDisplay, secDisplay);
		//string ret = minDisplay.ToString() + ":" + secDisplay.ToString ();
		return ret;
	}

	// Update is called once per frame
	void Update () {
		keyInput ();
	}

	void keyInput()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit ();
		}
	}


	string makeFilePath(string fileName)
	{
		return dirPath + fileName;
	}
	public void nextImageBtn()
	{
		nextImage ();
	}

	void nextImage()
	{
		int cnt = getNumberingCnt ();

		string fileName = imageList[cnt];
		string filePath = makeFilePath(fileName);
		textLog.text = filePath;

		byte[] fileData = File.ReadAllBytes(filePath);
		Texture2D tex = new Texture2D(2, 2);
		textLog.text = filePath;
		tex.LoadImage(fileData); 

		Rect rect = new Rect(0, 0, tex.width, tex.height);
		//mainImage.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f)); 
		Debug.Log("width:"+ tex.width);
		Debug.Log ("height:" + tex.height);

		croquis.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f)); 
	//	croquis.sprite.
	}
}
