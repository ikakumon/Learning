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
using System.Xml;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public bool isPause = true;
    public GameObject block;
    public GameObject block1;
    public GameObject block2;
    string path;
    public Button b;
    public GameObject[] GOs;
    private void Awake()
    {
        _instance = this;
        for (int i = 0; i < 10; i++)
        {
            if (File.Exists(Application.dataPath + "/StreamingFile" + "/ScreenShot" + i + ".png"))
            {
                Texture2D texture = new Texture2D(512, 512);
                texture.LoadImage(File.ReadAllBytes(Application.dataPath + "/StreamingFile" + "/ScreenShot" + i + ".png"));
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
             //   Instantiate(b, new Vector3(300 * i, 200, 0), Quaternion.identity, GameObject.Find("Canvas").transform).image.sprite = sprite;
                
            }
        }
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
            LoadByJson();
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
        Save.SaveNum += 1;
        return save;
    }
    private void SaveByJson()
    {
        Save save = CreateSaveGO();
        path = Application.dataPath + "/StreamingFile" + "/byJson.json";
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
    private void LoadByJson()
    {
        path = Application.dataPath + "/StreamingFile" + "/byJson.json";
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
            print(i);

            switch (save.Types[i])
            {
                case 0:
                    Instantiate(block, new Vector3((float)save.PositionX[i], (float)save.PositionY[i], (float)save.PositionZ[i]), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(block1, new Vector3((float)save.PositionX[i], (float)save.PositionY[i], (float)save.PositionZ[i]), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(block2, new Vector3((float)save.PositionX[i], (float)save.PositionY[i], (float)save.PositionZ[i]), Quaternion.identity);
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
    public void LoadGame()
    {
        //LoadByBin();
        //LoadByXml();
        LoadByJson();
    }
   
}