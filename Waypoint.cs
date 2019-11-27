using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attack;
public class Waypoint : MonoBehaviour
{
    //public GameObject block1;
    //public GameObject block2;
    //public GameObject block3;
    //public GameObject block4;
    public GameObject arrow;
    public GameObject change;
    public GameObject done;
    public GameObject Way;
    public List<GameObject> WayList;
    public static bool isChanging;
    int n;
    //int[,] a = new int[7, 8]

    //        {1,4,3,1,4,2,3,1 },
    //        {4,2,4,4,1,2,3,4 },
    //        {1,2,3,1,1,2,3,1 },
    //        {4,2,3,1,1,2,3,4 },
    //        {1,4,3,4,1,2,0,0 },
    //        {4,2,3,1,4,2,0,1 },
    //        {1,4,3,4,1,4,4,1 },
    //    };
    //void Start()
    //{
    //    print(a.GetLength(0));
    //    print(a.GetLength(1));
    //    GenerateMap(a);
    //}
    public void ChangeRoute()
    {
       // n=int.Parse( EnemyManager.SpawnPoint.name.Substring(EnemyManager.SpawnPoint.name.Length - 1, 1));
        change.SetActive(false);
        done.SetActive(true);
        isChanging = true;
        //switch (n)
        //{
        //    case 1:
        //        waypoint1.transform.DetachChildren();
        //        break;
        //    case 2:
        //        waypoint2. transform.DetachChildren();
        //        break;
        //}
        EnemyManager.SpawnPoint.transform.DetachChildren();

        foreach (var way in WayList)
        {
            Destroy(way);
        }
        WayList.Clear();

    }
    public void Done()
    {
        isChanging = false;
        change.SetActive(true);
        done.SetActive(false);
    }
    private void Update()
    {
        if (isChanging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LayerMask sp = 1 << 8;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit m, 100, sp.value);
                if (m.transform && m.transform.tag == "melee")
                {
                    m.transform.parent = EnemyManager.SpawnPoint.transform;

                    //switch (n)
                    //{
                    //    case 1:
                    //        m.transform.parent = waypoint1.transform;
                    //        break;
                    //    case 2:
                    //        m.transform.parent = waypoint2.transform;
                    //        break;
                    //}

                    int a = EnemyManager.SpawnPoint.childCount-1;
                    if (a >= 1)
                    {
                        Vector3 end = EnemyManager.SpawnPoint.GetChild(a).transform.position;
                        Vector3 former = EnemyManager.SpawnPoint.GetChild(a - 1).transform.position;

                        for (float i = 0; i < Vector3.Distance(end, former); i += 0.2f)
                        {
                            Vector3 pos = Vector3.Lerp(end, former, i / Vector3.Distance(end, former)) + new Vector3(0, 0.45f, 0);
                            WayList.Add(Instantiate(Way, pos, Quaternion.Euler(90, 0, 0)));
                        }
                    }
                }
            }

        }
    }
    public void OnDrawGizmosSelected()

        {
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
                // Debug.DrawLine(gos[i].position, gos[i + 1].position, Color.yellow);
            }
        }
    void DrawRoute() {
        Instantiate(arrow);
    }

    //void GenerateMap(int[,] a)
    //{
    //    for (int i = 0; i < a.GetLength(0); i++)
    //    {
    //        for (int j = 0; j < a.GetLength(1); j++)
    //        {
    //            switch (a[i, j])
    //            {
    //                case 1:
    //                    Instantiate(block1, new Vector3(j * 1.7f - 6, 0, 2 - i * 1.3f), Quaternion.identity);
    //                    break;
    //                case 2:
    //                    Instantiate(block2, new Vector3(j * 1.7f - 6, 0, 2 - i * 1.3f), Quaternion.identity);
    //                    break;
    //                case 3:
    //                    Instantiate(block3, new Vector3(j * 1.7f - 6, 0, 2 - i * 1.3f), Quaternion.identity);
    //                    break;
    //                case 4:
    //                    Instantiate(block4, new Vector3(j * 1.7f - 6, 0, 2 - i * 1.3f), Quaternion.identity);
    //                    break;
    //                default:
    //                   
    //            }
    //        }
    //    }

    //}

}
