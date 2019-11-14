using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMedic : MonoBehaviour
{
    public GameObject rec;
    int timer;
    public int ap = 30;
    int hp; int maxhp = 1000; int maxsp = 600; int sp;
    public List<GameObject> op ;
    void Start()
    {
        hp = maxhp;
        GetComponentInParent<Operator>().maxhp = 800;
        GetComponentInParent<Operator>().maxsp = 600;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OperatorBody")
        {
            op.Add(other.transform.parent.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "OperatorBody")
        {
            op.Remove(other.transform.parent.gameObject);
        }
    }
    
    void Update()
    {
       
        timer++;
        if (timer % (120 / Time.timeScale) < 1 )
        {
            rec.GetComponent<Recover>().ap = ap;

            /*op.Sort((left, right) =>
          {      if (left.GetComponentInParent<Operator>().HP.localScale.x > right.GetComponentInParent<Operator>().HP.localScale.x)
                       return 1;
                   else if (left.GetComponentInParent<Operator>().HP.localScale.x == right.GetComponentInParent<Operator>().HP.localScale.x)
                       return 0;
                   else
                       return -1;      }); */
            for (int i = 0; i < op.Count; i++)
            {
                if (!op[i] || !op[i].GetComponentInParent<Operator>().HP)
                    op.RemoveAt(i);
            }
            op.Sort(
             delegate (GameObject p1, GameObject p2)
          {
                  return p1.GetComponentInParent<Operator>().HP.localScale.x.CompareTo(p2.GetComponentInParent<Operator>().HP.localScale.x);//升序
          }
                   );
            for (int i = 0; i < Mathf.Min(3, op.Count); i++)
            {
                if (op[i] && op[i].GetComponentInParent<Operator>().HP.localScale.x < 1)
                    Instantiate(rec, op[i].transform.position , Quaternion.identity);
            }
            
        }

        if (GetComponentInParent<Operator>().sp == 0) StartCoroutine(RE());

    }

    IEnumerator RE()
    {

        for (int i = 0; i < maxsp; i++)
        {
            GetComponentInParent<Operator>().sp++;
            yield return new WaitForSeconds(0.013f);
        }
    }
}
