using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordTable : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _recordPrefab;
    public void Open()
    {
        LoadRecords();
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void LoadRecords()
    {
        JsonSerializer jsonSerializer = new JsonSerializer();
        List<Record> records = jsonSerializer.LoadJson();
        foreach (var record in records)
        {
            GameObject nameRecord = Instantiate(_recordPrefab, _content.transform);
            nameRecord.GetComponent<TMP_Text>().text = record.name;
            GameObject scoreRecord = Instantiate(_recordPrefab, _content.transform);
            scoreRecord.GetComponent<TMP_Text>().text = record.score.ToString();
        }
    }
    
}
