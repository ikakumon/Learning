using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    public GameObject startNode;
    public GameObject endNode;
    public GameObject enemy;
    void Start()
    {

    }
    void Update()
    {
       if( Physics.Raycast(enemy.transform.position, Vector3.down, out RaycastHit m))
        startNode = m.transform.gameObject;

        isBlocked = true;
        FindPath();
        if (isBlocked)
            print(1);
    }
    void FindPath()
    {

        List<GameObject> openSet = new List<GameObject>();
        HashSet<GameObject> closeSet = new HashSet<GameObject>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            GameObject curNode = openSet[0];

            for (int i = 0, max = openSet.Count; i < max; i++)
            {
                if (openSet[i].GetComponent<Node>().fCost <= curNode.GetComponent<Node>().fCost &&
                    openSet[i].GetComponent<Node>().hCost < curNode.GetComponent<Node>().hCost)
                {
                    curNode = openSet[i];
                }
            }

            openSet.Remove(curNode);
            closeSet.Add(curNode);

            // 判断周围节点，选择一个最优的节点
            foreach (var Neibour in curNode.GetComponent<Node>().getNeibourhood())
            {
                // 如果已经在闭集中
                if (closeSet.Contains(Neibour))
                    continue;

                int newgCost = curNode.GetComponent<Node>().gCost + getCost(curNode, Neibour);
                // 如果cost更小，或者原来不在开集中
                if (newgCost < Neibour.GetComponent<Node>().gCost || !openSet.Contains(Neibour))
                {
                    // 更新自己到开始节点的cost
                    Neibour.GetComponent<Node>().gCost = newgCost;
                    // 更新自己到终点的cost
                    Neibour.GetComponent<Node>().hCost = getCost(Neibour, endNode);
                    // 更新父节点为当前节点
                    Neibour.GetComponent<Node>().parent = curNode;
                    // 如果节点是新的，将它加入开集中
                    if (!openSet.Contains(Neibour))
                    {
                        openSet.Add(Neibour);
                    }
                }
            }
            // 找到目标节点
            if ( curNode == endNode)
            {
                isBlocked = false;
                MoveToTarget(startNode, endNode);
                return;
            }
        }
      
    }
    bool isBlocked;
    void MoveToTarget(GameObject startNode, GameObject endNode)
    {
        List<Transform> path = new List<Transform>();
        if (endNode != null)
        {


            //从终点开始把节点加入path，直到起点
            GameObject temp = endNode;
            float i = 1;
            while (temp != startNode)
            {
                i += 0.05f;
                path.Add(temp.transform);
                if (temp.GetComponent<Renderer>()) temp.GetComponent<Renderer>().material.color = Color.red * i;
                temp = temp.GetComponent<Node>().parent;
            }
            // 反转
            path.Reverse();
            enemy.GetComponent<Dog>().i = 0;
            enemy.GetComponent<Dog>().WayPoint = path;
        }

    }

    // 获取两个节点之间的cost
    int getCost(GameObject a, GameObject b)
    {
        int distX = Mathf.RoundToInt(Mathf.Abs(a.transform.position.x - b.transform.position.x));
        int distY = Mathf.RoundToInt(Mathf.Abs(a.transform.position.y - b.transform.position.y));
        //曼哈顿估价法
        return 10 * (distX + distY);

        //欧几里得估价法
        return 10 * (int)Mathf.Sqrt(distX * distX + distY * distY);

        //对角线估价法 判断哪个轴上相差的距离更远
        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distX + 10 * (distY - distX);
        }
    }

}
