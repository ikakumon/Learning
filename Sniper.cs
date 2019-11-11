using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy
{
    public GameObject[] op;
    public GameObject arrow;
    public Transform tar;
    int timer;
    public int ap=50;
    void Start()
    {
        maxhp = 200;
        PhysicResis = 20;
        magicP = 0;

    }
    void Update()
    {
        timer++;
        //攻击 攻击范围内最后放置的干员
        if (timer % (120 / Time.timeScale) < 1)
        {
            op = GameObject.FindGameObjectsWithTag("Operator");
            for (i = op.Length - 1; i >= 0; i--)
            {
                if ( Vector3.Distance(transform.position, op[i].transform.position) < 5)
                {
                    Instantiate(arrow, transform);
                    tar = op[i].transform;
                    break;
                }
            }
        }
    }
}
