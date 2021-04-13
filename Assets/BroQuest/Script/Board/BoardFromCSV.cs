using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BoardFromCSV : MonoBehaviour
{
    public GameObject boardSpace;
    public GameObject[] spaces;

    public TextAsset csvFile; // Reference of CSV file
	public Text contentArea; // Reference of contentArea where records are displayed
    public string defaultPrefab;
    protected string createdData = "";

	private char lineSeperator = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate character

    public string[] record_prefab;

    void Start()
    {
        ReadAndCreateSavedBoard();
    }

    // If you need to make a new base board data file
    void ReadAndCreateSavedBoard()
    {
        ReadSavedData();

        spaces = new GameObject[494];
        int spaceCount = 0;
        for (int rowCount = 0; rowCount < (int) Row.END - 1; rowCount++)
        {
            for (int columnCount = 0; columnCount < (int) Column.END - 1; columnCount++)
            {
                CreateSavedSpace(spaceCount, rowCount, columnCount, record_prefab[spaceCount]);
                spaceCount++;
            }
        }
    }

    void CreateSavedSpace(int spaceCount, int currentRow, int currentColumn, string currentPrefab)
    {
        spaces[spaceCount] = (GameObject) Instantiate(Resources.Load("Prefabs/BoardSpace"), new Vector3((int)currentColumn, 0, -(int)currentRow), Quaternion.identity);
        spaces[spaceCount].name = currentRow+"_"+currentColumn;
        if (currentPrefab != null) spaces[spaceCount].GetComponent<BoardSpace>().savedTileName = int.Parse(currentPrefab);
        BuildData(""+spaceCount, ""+currentRow, ""+currentColumn, defaultPrefab);
    }

    // Read data from CSV file
    private void ReadSavedData()
	{
        record_prefab = new string[494];

        contentArea.text = "";

		#if UNITY_EDITOR
			UnityEditor.AssetDatabase.Refresh ();
		#endif

        int recordCount = 0;
		string[] records = csvFile.text.Split(lineSeperator);
		foreach (string record in records)
		{
			string[] fields = record.Split(fieldSeperator);
            
            if (recordCount < 494) {

                foreach(string field in fields)
                {
                    record_prefab[recordCount] = field;
                    contentArea.text += field + "\t";
                    recordCount++;
                }
                contentArea.text += '\n';
            }
		}
	}

    // If you need to make a new base board data file
    void CreateOriginalBoard()
    {
        EraseData();

        spaces = new GameObject[494];
        int spaceCount = 0;
        for (int rowCount = 0; rowCount < (int) Row.END - 1; rowCount++)
        {
            for (int columnCount = 0; columnCount < (int) Column.END - 1; columnCount++)
            {
                CreateOriginalDataFile(spaceCount, rowCount, columnCount);
                spaceCount++;
            }
        }
        AddData();
		ReadData();
    }

    void CreateSpace(int spaceCount, int rowCount, int columnCount)
    {
        spaces[spaceCount] = Instantiate(boardSpace, new Vector3(columnCount, 0, rowCount), Quaternion.identity);
        spaces[spaceCount].transform.Rotate(0.0f, 90.0f, 90.0f, Space.Self);
        spaces[spaceCount].name = "R"+rowCount+"_C"+columnCount;
    }
    void CreateOriginalDataFile(int spaceCount, int currentRow, int currentColumn)
    {
        spaces[spaceCount] = (GameObject) Instantiate(Resources.Load("Prefabs/BoardSpace"), new Vector3((int)currentColumn, 0, (int)currentRow), Quaternion.identity);
        spaces[spaceCount].transform.Rotate(0.0f, 90.0f, 90.0f, Space.Self);
        spaces[spaceCount].name = currentRow+"_"+currentColumn;
        spaces[spaceCount].GetComponent<BoardSpace>().row = (Row) currentRow;
        spaces[spaceCount].GetComponent<BoardSpace>().column = (Column) currentColumn;
        BuildData(""+spaceCount, ""+currentRow, ""+currentColumn, defaultPrefab);
    }

    public void BuildData(string id, string row, string column, string prefab)
	{
		// add to our string that will ultimately become the new CSV file
		createdData += lineSeperator + id + fieldSeperator + row + fieldSeperator + column + fieldSeperator + prefab;
	}

	// Add data to CSV file
	public void AddData()
	{
		// Following line adds data to CSV file
		File.AppendAllText(getPath() + "/BroQuest/CSV_Parsing/BoardData.csv", createdData);
	}

    // Add data to CSV file
	public void EraseData()
	{
        contentArea.text = "";
		#if UNITY_EDITOR
			UnityEditor.AssetDatabase.Refresh ();
		#endif
		// Following line clears the CSV file
		File.WriteAllText(getPath() + "/BroQuest/CSV_Parsing/BoardData.csv", "");
	}

    // Read data from CSV file
    private void ReadData()
	{
        contentArea.text = "";
		#if UNITY_EDITOR
			UnityEditor.AssetDatabase.Refresh ();
		#endif

		string[] records = csvFile.text.Split(lineSeperator);
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

	// Get path for given CSV file
	private string getPath(){
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