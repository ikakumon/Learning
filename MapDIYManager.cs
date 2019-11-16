using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDIYManager : MonoBehaviour
{
    public GameObject block;
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;
    public GameObject block4;
    public GameObject meleeButton;
    public GameObject remoteButton;
    LayerMask sp = 1 << 8;
    public Slider r;
    public Slider g;
    public Slider b;
    public Renderer cube;
    public Text t;
    public InputField input;
    public static int BLOCK=1;
    void Start()
    {
       
       

    }
    public void SetMelee()
    {
        sp = 1 << 8;
        remoteButton.SetActive(true);
        meleeButton.SetActive(false);
    }
    public void SetRemote()
    {
        sp = 1 << 9;
        meleeButton.SetActive(true);
        remoteButton.SetActive(false);
    }
    public void SetBlock(int n)
    {
        BLOCK = n;
    }
    public void MoveCamera( bool move)
    {
         moving=move;
    }
    bool moving=false;
    float rvalue;
    float gvalue;
    float bvalue;
    float result;

    void Update()
    {
        //if (moving)
        //Camera.main.transform.position += new Vector3(0, 0.1f, 0);

        //if (rvalue != r.value|| gvalue != g.value || bvalue != b.value)//相当于On value changed
        //{
        //    cube.material.color = new Color(r.value, g.value, b.value, 1);
        //    rvalue = r.value;
        //    gvalue = g.value;
        //    bvalue = b.value;
        //    input.text = (r.value * 255).ToString();

        //}
        //r.value = float.Parse(input.text) / 255;

        // float.TryParse(t.text, out result);
        //Camera.main.fieldOfView = 29 + r.value * 10;

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit m, 100, sp.value);
            if (m.transform && m.transform.childCount==0)
            {
                switch (BLOCK)
                {
                    case 1:
                        Instantiate(block, m.transform.position, Quaternion.identity,m.transform);
                        break;
                    case 2:
                        Instantiate(block1, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 3:
                        Instantiate(block2, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 4:
                        Instantiate(block3, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 5:
                        Instantiate(block4, m.transform.position, Quaternion.identity, m.transform);
                        break;

                }
            }
            else if (m.transform && m.transform.childCount != 0)
            {
                Destroy(m.transform.GetChild(0).gameObject);
                switch (BLOCK)
                {
                    case 0:break;
                    case 1:
                        Instantiate(block, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 2:
                        Instantiate(block1, m.transform.position, Quaternion.identity,m.transform);
                        break;
                    case 3:
                        Instantiate(block2, m.transform.position, Quaternion.identity,m.transform);
                        break;
                    case 4:
                        Instantiate(block3, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 5:
                        Instantiate(block4, m.transform.position, Quaternion.identity, m.transform);
                        break;
                }
            }
        }
        if (Input.GetMouseButton(1))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit m, 100, sp.value);
            if (m.transform && m.transform.childCount == 0)
            {
                switch (BLOCK)
                {
                    case 1:
                        Instantiate(block, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 2:
                        Instantiate(block1, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 3:
                        Instantiate(block2, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 4:
                        Instantiate(block3, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 5:
                        Instantiate(block4, m.transform.position, Quaternion.identity, m.transform);
                        break;

                }
            }
            else if (m.transform && m.transform.childCount != 0)
            {
                Destroy(m.transform.GetChild(0).gameObject);
                switch (BLOCK)
                {
                    case 0: break;

                    case 1:
                        Instantiate(block, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 2:
                        Instantiate(block1, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 3:
                        Instantiate(block2, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 4:
                        Instantiate(block3, m.transform.position, Quaternion.identity, m.transform);
                        break;
                    case 5:
                        Instantiate(block4, m.transform.position, Quaternion.identity, m.transform);
                        break;

                }
            }
        }
    
    }
}
