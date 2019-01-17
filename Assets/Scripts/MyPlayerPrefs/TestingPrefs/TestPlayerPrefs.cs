using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyAssets.PlayerPrefs;

public class TestPlayerPrefs : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            Write();
        if (Input.GetKeyDown(KeyCode.Keypad2))
            Load();
        if (Input.GetKeyDown(KeyCode.Space))
            SavePrefs();
    }
    
    void Write()
    {
        int dataInt = 5;
        MyPlayerPrefs.SetInt("SimpleInt", dataInt);
        float dataFloat = 0.5f;
        MyPlayerPrefs.SetFloat("SimpleFloat", dataFloat);
        string dataString = "Saved data atata";
        MyPlayerPrefs.SetString("SimpleString", dataString);
        Vector3 dataVector = new Vector3(0.5f, 0.5f, 1f);
        MyPlayerPrefs.SetVector3("SimpleVector", dataVector);
        Quaternion dataQuat = new Quaternion(1f, 2f, 3f, 4f);
        MyPlayerPrefs.SetQuaternion("SimpleQuat", dataQuat);
        Color dataColor = new Color(1f, 1f, 1f, 1f);
        MyPlayerPrefs.SetColor("SimpleColor", dataColor);
        Debug.LogWarning("WRITED");
    }

    void Load()
    {
        int dataInt = MyPlayerPrefs.GetInt("SimpleInt", 0);
        float dataFloat = MyPlayerPrefs.GetFloat("SimpleFloat", 0.8f);
        string dataString = MyPlayerPrefs.GetString("SimpleString", "wrong");
        Vector3 dataVector = MyPlayerPrefs.GetVector3("SimpleVector", Vector3.zero);
        Quaternion dataQuat = MyPlayerPrefs.GetQuaternion("SimpleQuat", new Quaternion(0f, 0f, 0f, 0f));
        Color dataColor = MyPlayerPrefs.GetColor("SimpleColor", new Color(0f, 0f, 0f, 0f));
        Debug.LogWarning(string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", dataInt, dataFloat, dataString, dataVector, dataQuat, dataColor));
    }

    void SavePrefs()
    {
        MyPlayerPrefs.Save();
        Debug.LogWarning("SAVED?");
    }
}
