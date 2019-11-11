using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Operator : MonoBehaviour
{
    public Transform target;
    int timer;
    bool turned;
    public bool attack;
    public float MagicResis;
    public float AtkSpeed;
    public float PhysicResis;
    public float hp; public float maxhp; public float maxsp = 60; public float sp;
    public RectTransform HP;
    RectTransform MP;
    RectTransform BG;
    RectTransform white;
    RectTransform uiParent;  //移动UI的父物体
    GameObject obj;
    public Transform root;
    Vector3 faceTo;
    public int StopNum;//阻挡数量
    public int maxStop = 1;
    public int OPnum = 0;
    public bool RangeShowed;
    public GameObject atkRange;
    bool autoRecover;
    void Start()
    {
        hp = maxhp;
        GameObject prefab = (GameObject)Resources.Load("Prefabs/OPUI");
        obj = Instantiate(prefab);
        obj.transform.parent = transform;
        uiParent = obj.GetComponent<RectTransform>();
        HP = obj.transform.Find("HPbar").GetComponent<RectTransform>();
        MP = obj.transform.Find("MPbar").GetComponent<RectTransform>();
        BG = obj.transform.Find("BG").GetComponent<RectTransform>();
        white = obj.transform.Find("BGwhite").GetComponent<RectTransform>();
        // foreach (Renderer r in GetComponentsInChildren<Renderer>())
        //  r.material.color = new Color(1, 0, 0);
    }
  
    void Update()
    {
        //  print( transform.TransformPoint(GetComponent<BoxCollider>().center));
        if (hp > maxhp) hp = maxhp;
        if (hp > 0)
        {
            BG.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(-40, -30, 0);
            HP.position = BG.position + new Vector3(0,3,0);
            MP.position = BG.position + new Vector3(0,-3, 0);
            white.position = HP.position;
            HP.localScale = new Vector3(hp / maxhp, 1, 1);
            MP.localScale = new Vector3(sp / maxsp, 1, 1);

            if (white.localScale.x > hp / maxhp)
                white.localScale -= new Vector3(0.01f, 0, 0);
            else {
                white.localScale = HP.localScale;
            }
        }
        if (target)
        {
            // faceTo = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);
            //  root.forward = faceTo;
            //if(target.position.x>transform.position.x)

            //if (transform.position.z > target.position.z)
            //    root.eulerAngles = new Vector3(60, 0, Vector3.Angle(transform.position - target.position, Vector3.right) - 180);
            //else
            //    root.eulerAngles = new Vector3(60, 0, 180 - Vector3.Angle(transform.position - target.position, Vector3.right));
        }

        if (autoRecover)
            hp += rec;
       
    }
    int rec;

    IEnumerator Red()
    {
        for (float i = 1; i > 0; i -= 0.2f)
        {
            GetComponentInChildren<Renderer>().material.color = new Color(1, i, i);
            yield return new WaitForEndOfFrame();
        }
        for (float i = 0; i < 1; i += 0.2f)
        {
            GetComponentInChildren<Renderer>().material.color = new Color(1, i, i);
            yield return new WaitForEndOfFrame();
        }
        GetComponentInChildren<Renderer>().material.color = new Color(1, 1, 1);

    }
    public void AutoRecover(int Amount)
    {
        autoRecover = true;
        rec = Amount;
    }
    public void Damage(float ap, bool isMagic = false)
    {
        StartCoroutine(Red());

        if (isMagic)
        {
            hp -= ap * (1 - MagicResis);
        }
        else
        { hp -= (ap - PhysicResis) + ap * 0.05f; }
        if (hp <= 0)
        {
            StartCoroutine(Sink());
        }
      
    }
    IEnumerator Sink()
    {
        Destroy(obj);
        GetComponentInChildren<Renderer>().material.color = new Color(0, 0, 0);

        for (float i = 0; i < 1; i += 0.03f)
        {
            GetComponentInChildren<SphereCollider>().center += new Vector3(0, -0.1f, 0);
            GetComponentInChildren<Renderer>().material.color = new Color(0, 0, 0, 1 - i);
            yield return new WaitForEndOfFrame();

        }
        Destroy(gameObject);
    }
    public List<GameObject> en;

    int indexMin = 0;
    private int recover;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "melee")
        {
            GameObject o= Instantiate(atkRange,other.transform.position+new Vector3(0,0.251f,0),Quaternion.Euler(90,0,0),transform);
          //  o.transform.SetAsFirstSibling();
        }
        if (other.tag == "remote")
        {
            GameObject o=  Instantiate(atkRange, other.transform.position + new Vector3(0, 0.51f, 0), Quaternion.Euler(90, 0, 0),transform);
           // o.transform.SetAsFirstSibling();
        }
        if (other.tag == "EnemyBody")
        {
            en.Add(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "EnemyBody" && StopNum <= maxStop)
        {

            float min = 100;
            //float min2= 100;
            //int indexMin2;
            for (int i = 0; i < en.Count; i++)
            {
                if (en[i] && en[i].GetComponent<Enemy>().DisRemain < min)
                {
                    min = en[i].GetComponent<Enemy>().DisRemain;
                    indexMin = i;
                }
                //else if (en[i].GetComponent<Enemy>().DisRemain < min2 && en[i].GetComponent<Enemy>().DisRemain != min)
                //{
                //    min2 = en[i].GetComponent<Enemy>().DisRemain;
                //    indexMin2 = i;
                //}
            }
            attack = true;
            target = en[indexMin].transform;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyBody")
        {
            attack = false;
            //target = null;
            en.Remove(other.gameObject);

        }
    }

    IEnumerator TurnToTarget()
    {
        for (float t = 0; t < 1; t += 0.05f)
        {
            root.forward = Vector3.Lerp(transform.forward, faceTo, t);
            yield return new WaitForSeconds(0.026f);
        }

    }

}