using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using LitJson;
using System.Text.RegularExpressions;
using System.Xml;

public class MapSaver : MonoBehaviour
{
    public static MapSaver _instance;
    public bool isPause = true;
    bool isFirstSave=true;
    public GameObject canvas;
    public GameObject loadPanel;
    public GameObject cover;
    public GameObject block;
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;
    public GameObject block4;
    FileStream path;
    public Button b;
    public GameObject[] GOs;
    public int n;
    private void Awake()
    {
        _instance = this;
       
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveByJson();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadByJson("");
        }
    }
    //创建Save对象并存储当前游戏状态信息
    private Save CreateSaveGO()
    {
        Save save = new Save();
        GOs = GameObject.FindGameObjectsWithTag("Respawn");
        for (int i = 0; i < GOs.Length; i++)
        {
            save.PositionZ.Add(GOs[i].transform.position.z);
            save.PositionY.Add(GOs[i].transform.position.y);
            save.PositionX.Add((int)GOs[i].transform.position.x);
            save.Types.Add(GOs[i].GetComponent<Adobe>().type);
            
        }
        return save;
    }
    public void Load()
    {
        loadPanel.SetActive(!loadPanel.activeSelf);
        cover.SetActive(!cover.activeSelf);
    }
    public void CreateStage()
    {
        string[] files =Directory.GetFiles(Application.streamingAssetsPath, "*.txt");
       string saveNum= Regex.Match(files[0], @"[\d]+(?=.txt)").Value;//获取文件名
        n = int.Parse(saveNum)+1;
                File.Create(Application.streamingAssetsPath + n + ".txt");
                File.Create(Application.streamingAssetsPath + "/save" + n + ".json");
        //for (n = 1; n < 20; n++)
        //{
        //    if (!File.Exists(Application.streamingAssetsPath + "/save" + n + ".json"))
        //    {
        //        File.Create(Application.streamingAssetsPath + "/save" + n + ".json");
        //        break;
        //    }
        //}
    }
    private void SaveByJson()
    {
        //if (isFirstSave)
        //{
        //    isFirstSave = false;
        //    CreateStage();
        //}
        StartCoroutine(CaptureScreen());

        Save save = CreateSaveGO();
        string path = Application.streamingAssetsPath + "/save" + n + ".json";
        //利用JsonMapper将save对象转换为Json格式的字符串
        string saveJsonStr = JsonMapper.ToJson(save);
        //将这个字符串写入到文件中
        //创建一个StreamWriter，并将字符串写入
        StreamWriter sw = new StreamWriter(path);
        sw.Write(saveJsonStr);
        //关闭写入流
        sw.Close();
        AssetDatabase.Refresh();

    }
    IEnumerator CaptureScreen()
    {
        canvas.SetActive(false);//隐藏UI
        yield return new WaitForEndOfFrame();//等待一帧，因为下一帧才会刷新画面
        ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath + "/save" + n + ".png");
        canvas.SetActive(true);
    }
    public void LoadByJson(string path)
    {
        // path= Application.streamingAssetsPath + "/Json" + order + ".json";
        if (!File.Exists(path)) { print("存档文件不存在"); return; }
        //创建一个StreamReader，用来读取流
        StreamReader sr = new StreamReader(path);
        //将读取到的流赋值给saveJsonStr
        string saveJsonStr = sr.ReadToEnd();
        sr.Close();
        //将字符串转换为Save对象
        Save save = JsonMapper.ToObject<Save>(saveJsonStr);
        SetGame(save);
    }
    private void SetGame(Save save)
    {
        for (int i = 0; i < save.PositionX.Count; i++)
        {

            switch (save.Types[i])
            {
                case 1:
                    Instantiate(block, new Vector3((float)save.PositionX[i], (float)save.PositionY[i], (float)save.PositionZ[i]), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(block1, new Vector3((float)save.PositionX[i], (float)save.PositionY[i], (float)save.PositionZ[i]), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(block2, new Vector3((float)save.PositionX[i], (float)save.PositionY[i], (float)save.PositionZ[i]), Quaternion.identity);
                    break;
                case 4:
                    Instantiate(block3, new Vector3((float)save.PositionX[i], (float)save.PositionY[i], (float)save.PositionZ[i]), Quaternion.identity);
                    break;
                case 5:
                    Instantiate(block4, new Vector3((float)save.PositionX[i], (float)save.PositionY[i], (float)save.PositionZ[i]), Quaternion.identity);
                    break;
            }
        }
    }
    //public void ContinueGame()
    //{
    //    UIManager._instance.ShowMessage();
    //    UnPause();
    //}
    //public void NewGame()
    //{
    //    foreach (var targetGo in targetGOs)
    //    {
    //        targetGo.GetComponent<TargetManager>().UpdateMonsters();
    //    }
    //    UIManager._instance.shootNum = 0;
    //    UIManager._instance.score = 0;
    //    UIManager._instance.ShowMessage();
    //    UnPause();
    //}
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        //SaveByBin();
        // SaveByXml();
        SaveByJson();

    }
    public int order;
    public void LoadGame()
    {
        //LoadByBin();
        //LoadByXml();
        LoadByJson("");
    }
   
}
