using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class LoadManager : MonoBehaviour
{
    public Button template;
    void Start()
    {
        string[] files = Directory.GetFiles(Application.streamingAssetsPath, "*.png");
        foreach (var f in files)
        {
            if (File.Exists(f))
            {
                Texture2D texture = new Texture2D(512, 512);
                texture.LoadImage(File.ReadAllBytes(f));
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                Button b = Instantiate(template, Vector3.zero, Quaternion.identity, transform);
                b.image.sprite = sprite;
                b.name = f.Substring(f.Length - 9).Remove(5);
                //  b.name= Regex.Match(b.name, @"[^/]+(?=.png)").Value;//获取文件名
            }
        }

        //for (int i = 0; i < 10; i++)
        //{
        //    if (File.Exists(Application.streamingAssetsPath + "/ScreenShot" + i + ".png"))
        //    {
        //        Texture2D texture = new Texture2D(512, 512);
        //        texture.LoadImage(File.ReadAllBytes(Application.streamingAssetsPath + "/ScreenShot" + i + ".png"));
        //        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        //        Instantiate(b, Vector3.zero, Quaternion.identity, transform).image.sprite = sprite;
        //    }
        //}

    }
    private static List<string> GetFiles(string folder, string extension)
    {
        //若文件夹路径不存在，返回空
        if (!Directory.Exists(folder))
        {
            return null;
        }
        //扩展名必须存在
        if (string.IsNullOrEmpty(extension))
        {
            return null;
        }
        DirectoryInfo dInfo = new DirectoryInfo(folder);
        //文件夹下的所有文件
        FileInfo[] aryFInfo = dInfo.GetFiles();
        List<string> lstRet = new List<string>();
        //将扩展名转化为小写的形式（如“.TXT”与“.txt”其实是相同的），方便后续处理
        extension = extension.ToLower();
        //循环判断每一个文件
        foreach (FileInfo fInfo in aryFInfo)
        {
            //如果当前文件扩展名与指定的相同，则将其加入返回值中
            if (fInfo.Extension.ToLower().Equals(extension))
            {
                lstRet.Add(fInfo.FullName);
            }
        }
        return lstRet;
    }

    void Update()
    {

    }
}
