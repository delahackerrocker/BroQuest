using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Board : MonoBehaviour
{
    public GameObject boardSpace;
    public GameObject[] spaces;

    public TextAsset csvFile; // Reference of CSV file
	public Text contentArea; // Reference of contentArea where records are displayed
    public string defaultPrefab;
    protected string createdData = "";

	private char lineSeperator = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate character

    private int[] record_rows;
    private int[] record_columns;
    private int[] record_prefab;

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

    void CreateSavedSpace(int spaceCount, int currentRow, int currentColumn, int currentPrefab)
    {
        spaces[spaceCount] = (GameObject) Instantiate(Resources.Load("Prefabs/BoardSpace"), new Vector3((int)currentColumn, 0, (int)currentRow), Quaternion.identity);
        spaces[spaceCount].name = currentRow+"_"+currentColumn;
        spaces[spaceCount].GetComponent<BoardSpace>().row = (Row) currentRow;
        spaces[spaceCount].GetComponent<BoardSpace>().column = (Column) currentColumn;
        spaces[spaceCount].GetComponent<BoardSpace>().savedTileName = currentPrefab;
        BuildData(""+spaceCount, ""+currentRow, ""+currentColumn, defaultPrefab);
    }

    // Read data from CSV file
    private void ReadSavedData()
	{
        record_rows = new int[494];
        record_columns = new int[494];
        record_prefab = new int[494];

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
                record_rows[recordCount] = int.Parse(fields[1]);
                record_columns[recordCount] = int.Parse(fields[2]);
                record_prefab[recordCount] = int.Parse(fields[3]);

                foreach(string field in fields)
                {
                    contentArea.text += field + "\t";
                }
                contentArea.text += '\n';
                recordCount++;
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

public enum Row : int
{
    Row_00 = 0,
    Row_01 = 1,
    Row_02 = 2,
    Row_03 = 3,
    Row_04 = 4,
    Row_05 = 5,
    Row_06 = 6,
    Row_07 = 7,
    Row_08 = 8,
    Row_09 = 9,
    Row_10 = 10,
    Row_11 = 11,
    Row_12 = 12,
    Row_13 = 13,
    Row_14 = 14,
    Row_15 = 15,
    Row_16 = 16,
    Row_17 = 17,
    Row_18 = 18,
    Row_19 = 19,
    END
}
public enum Column : int
{
    Column_00 = 0,
    Column_01 = 1,
    Column_02 = 2,
    Column_03 = 3,
    Column_04 = 4,
    Column_05 = 5,
    Column_06 = 6,
    Column_07 = 7,
    Column_08 = 8,
    Column_09 = 9,
    Column_10 = 10,
    Column_11 = 11,
    Column_12 = 12,
    Column_13 = 13,
    Column_14 = 14,
    Column_15 = 15,
    Column_16 = 16,
    Column_17 = 17,
    Column_18 = 18,
    Column_19 = 19,
    Column_20 = 20,
    Column_21 = 21,
    Column_22 = 22,
    Column_23 = 23,
    Column_24 = 24,
    Column_25 = 25,
    Column_26 = 26,
    END
}