using System;

[Serializable]
public class Item
{
    public int id_i;
    public string id_s;
    public string type;
    public string name;
    public float weight;
    public int width = 1;
    public int height = 1;
    public string description;
    public string prefabPath;
    public string spritePath;

    public int value;
}
