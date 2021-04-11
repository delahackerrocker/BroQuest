using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class CSVParsing : MonoBehaviour 
{
	public TextAsset csvFile; // Reference of CSV file
	public InputField IDInputField;// Reference of ID input field
	public InputField TextureInputField; // Reference of Texture input filed
	public Text contentArea; // Reference of contentArea where records are displayed

	private char lineSeperator = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate character

	void Start () 
	{
		readData();
	}
	
	// Read data from CSV file
	private void readData()
	{
		string[] records = csvFile.text.Split (lineSeperator);
		foreach (string record in records)
		{
			string[] fields = record.Split(fieldSeperator);
			foreach(string field in fields)
			{
				contentArea.text += field + "\t";
			}
			contentArea.text += '\n';
		}
	}

	// Add data to CSV file
	public void addData()
	{
		// Following line adds data to CSV file
		File.AppendAllText(getPath() + "/BroQuest/CSV_Parsing/BoardData.csv",lineSeperator + IDInputField.text + fieldSeperator + TextureInputField.text);
		// Following lines refresh the edotor and print data
		IDInputField.text = "";
		TextureInputField.text = "";
		contentArea.text = "";
		#if UNITY_EDITOR
			UnityEditor.AssetDatabase.Refresh ();
		#endif
		readData();
	}

	// Get path for given CSV file
	private static string getPath(){
		#if UNITY_EDITOR
		return Application.dataPath;
		#elif UNITY_ANDROID
		return Application.persistentDataPath;// +fileName;
		#elif UNITY_IPHONE
		return GetiPhoneDocumentsPath();// +"/"+fileName;
		#else
		return Application.dataPath;// +"/"+ fileName;
		#endif
	}

	// Get the path in iOS device
	private static string GetiPhoneDocumentsPath()
	{
		string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
		path = path.Substring(0, path.LastIndexOf('/'));
		return path + "/Documents";
	}

}