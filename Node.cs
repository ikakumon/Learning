using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    void Start()
    {
      c = GetComponent<Renderer>().material.color;
      r = GetComponent<Renderer>();
    }
    Color c;
    Renderer r;
    public Vector3 pos;
    void Update()
    {
        if (Time.frameCount%20==0)
         //   r.material.color += new Color(0.01f, 0.01f, 0.01f);
            r.material.color = c;
    }
    // 格子坐标
    
    // 与起点
    public int gCost;
    // 与目标点
    public int hCost;

    // 总的路径长度
    public int fCost
    {
        get { return gCost + hCost; }
    }

    // 父节点
    public GameObject parent;

   

    public List<GameObject> getNeibourhood()
    {
        List<GameObject> list = new List<GameObject>();
        //可以走斜线
        //for (int i = -1; i <= 1; i++)
        //{
        //    for (int j = -1; j <= 1; j++)
        //    {
        //        // 如果是自己，则跳过
        //        if (i == 0 && j == 0)
        //            continue;

        //        if (Physics.Raycast(transform.position, new Vector3(i, 0, j),out RaycastHit m, 1))
        //            list.Add(m.transform.gameObject);
        //    }
        //}

        //只能走直线
        RaycastHit m;
        if (Physics.Raycast(transform.position,Vector3.left, out  m, 1))
            list.Add(m.transform.gameObject);
        if (Physics.Raycast(transform.position, Vector3.right, out  m, 1))
            list.Add(m.transform.gameObject);
        if (Physics.Raycast(transform.position, Vector3.forward, out  m, 1))
            list.Add(m.transform.gameObject);
        if (Physics.Raycast(transform.position, Vector3.back, out  m, 1))
            list.Add(m.transform.gameObject);
        return list;
    }
   
}
