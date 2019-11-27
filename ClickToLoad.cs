using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickToLoad : MonoBehaviour
{

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Camera.main.GetComponent<GameManager>().LoadByJson(Application.streamingAssetsPath + "/" + transform.name+".json");
        Camera.main.GetComponent<GameManager>().n =int.Parse(transform.name.Substring(transform.name.Length-1,1));
    }

}