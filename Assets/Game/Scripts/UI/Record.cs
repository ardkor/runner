using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecordsData
{
    public Record[] records;
}
[System.Serializable]
public class Record
{
    public string name;
    public int score;

    public Record(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
