using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Attack
{
    public class OperatorManager : MonoBehaviour
    {
        bool directionSet;
        public GameObject reimu;
        public GameObject Reisen;
        public GameObject reisen;
        public GameObject yukari;
        public GameObject marisa;
        public GameObject sakuya;
        public GameObject direction;
        RectTransform uiParent;
        RectTransform black;
        RectTransform cancel;
        GameObject[] remo;
        GameObject[] mele;
        GameObject cam;
        GameObject t;
        Vector3 OriginPos;
        Vector3 TargetPos;
        bool active = true;
        void Start()
        {
            cam = GameObject.Find("Main Camera");
            OriginPos = cam.transform.position;
            uiParent = direction.GetComponent<RectTransform>();
            /*   left = direction.transform.Find("left").GetComponent<RectTransform>();
               right = direction.transform.Find("right").GetComponent<RectTransform>();
               forward = direction.transform.Find("forward").GetComponent<RectTransform>();
               back = direction.transform.Find("back").GetComponent<RectTransform>();
              ok = direction.transform.Find("ok").GetComponent<RectTransform>();
               cancel = direction.transform.Find("cancel").GetComponent<RectTransform>();
           */
            black = direction.transform.Find("black").GetComponent<RectTransform>();

            remo = GameObject.FindGameObjectsWithTag("remote");
            mele = GameObject.FindGameObjectsWithTag("melee");
        }
        public bool showRange;
        RaycastHit m;
        
        private void Update()
        {
            //if (Input.GetMouseButtonDown(1) && active)
            //{
            //    LayerMask sp = 1 << 4;

            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    Physics.Raycast(ray, out RaycastHit m, 100, sp.value);
            //    if (m.transform.tag == "OperatorBody")
            //    {
            //        m.transform.GetComponentInParent<Operator>().RangeShowed = !m.transform.GetComponentInParent<Operator>().RangeShowed;
            //    }

            //}
           
        }


        public void Reset()
        {
            if (GameObject.Find("Info")) GameObject.Find("Info").SetActive(false);
            StartCoroutine(MoveCam(true));
            Time.timeScale = 1;

        }
        IEnumerator MoveCam(bool moveBack)
        {
            if (moveBack)
            {
                for (float i = 0; i < 1; i += 0.1f)
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, OriginPos, i);
                    yield return new WaitForEndOfFrame();
                }
            }

            else
            {
                //if (GameObject.Find("Info")) GameObject.Find("Info").SetActive(false);
                //m.transform.parent.Find("Info").gameObject.SetActive(true);
                // TargetPos = OriginPos + new Vector3(m.transform.position.x, 0, m.transform.position.z + 2);
                //Time.timeScale = 0.1f;

                for (float i = 0; i < 1; i += 0.1f)
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, TargetPos, i);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        public void Set(string name)
        {
            active = false;
            Time.timeScale = 0.1f;

            switch (name)
            {
                case "reisen":
                    Reisen.SetActive(true);
                    t = Reisen;
                    reisen.transform.parent = Reisen.transform;
                    StartCoroutine(S());

                    break;
                case "reimu":
                    reimu.SetActive(true);
                    t = reimu;
                    StartCoroutine(S(false, true));

                    break;
                case "marisa":
                    marisa.SetActive(true);
                    t = marisa;
                    StartCoroutine(S(false, true));

                    break;

            }
        }

        IEnumerator S(bool me = true, bool re = false)
        {
            LayerMask melee = 1 << 8;              //近战位开启
            LayerMask remote = 1 << 9;                  //远程位开启

            for (int i = 0; i < 9999; i++)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit ra);
                if (ra.transform.tag == "melee" || ra.transform.tag == "remote")
                    t.transform.position = ra.transform.position + new Vector3(0, 0.5f, -0.4f);

                //   if (Physics.Raycast(ray, out RaycastHit r, 100, remote.value))
                //    t.transform.position = r.transform.position + new Vector3(0, 0.5f, -0.4f);
                //    if (Physics.Raycast(ray, out RaycastHit m, 100, melee.value))
                //     t.transform.position = m.transform.position + new Vector3(0, 0.25f, -0.4f);


                if (me)
                {
                    foreach (GameObject g in mele)
                        g.GetComponent<Renderer>().material.color = new Color(0.7f, 0.9f, 0.7f);

                    //  g.GetComponent<Renderer>().material.color += new Color(0, Mathf.Sin(Time.unscaledTime) / 20, 0f);
                    if (Input.GetMouseButtonDown(0) && ra.transform.tag == "melee")
                    {
                        foreach (GameObject g in mele)
                            g.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                        foreach (GameObject g in remo)
                            g.GetComponent<Renderer>().material.color = new Color(1, 1, 1);

                        direction.SetActive(true);
                        World2UI(uiParent, black);
                        yield break;
                    }
                }

                if (re)
                {
                    foreach (GameObject g in remo)
                        g.GetComponent<Renderer>().material.color = new Color(0.7f, 1 + Mathf.Sin(Time.unscaledTime) / 5, 0.7f);

                    if (Input.GetMouseButtonDown(0) && ra.transform.tag == "remote")
                    {
                        foreach (GameObject g in mele)
                            g.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                        foreach (GameObject g in remo)
                            g.GetComponent<Renderer>().material.color = new Color(1, 1, 1);

                        direction.SetActive(true);
                        World2UI(uiParent, black);
                        yield break;
                    }
                }
                yield return new WaitForSecondsRealtime(0.013f);
            }

        }


        public void World2UI(RectTransform uiParent, RectTransform black)
        {
            Vector3 spos = Camera.main.WorldToScreenPoint(t.transform.position);  //获取屏幕坐标 跟随t
            Vector2 vv = new Vector2(spos.x, spos.y);
            Vector2 retPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(uiParent, vv, uiParent.GetComponent<Canvas>().worldCamera, out retPos);  //UI相机
            black.anchoredPosition = new Vector2(retPos.x + 0, retPos.y + 0);

        }

        public void SetDirection(string d)
        {
            Range.showRange = true;
            switch (d)
            {
                case "left":
                    t.transform.forward = Vector3.left;
                    break;
                case "right":
                    t.transform.forward = Vector3.right;
                    break;
                case "forward":
                    t.transform.forward = Vector3.forward;
                    break;
                case "back":
                    t.transform.forward = Vector3.back;
                    break;
            }


        }
        GameObject r;
        public GameObject red;
        public void OK()
        {
            r = GameObject.Find(string.Concat(t.transform.GetChild(2).name, "B"));

            GameObject.Find(string.Concat(t.transform.GetChild(2).name, "B")).SetActive(false);
            direction.SetActive(false);
            t.SetActive(false);

            Time.timeScale = 1f;

            t.transform.GetChild(2).gameObject.SetActive(true);
            t.transform.GetChild(2).parent = null;
            active = true;
            Range.showRange = false;

        }
        public void Cancel()
        {
            direction.SetActive(false);
            t.SetActive(false);
            Time.timeScale = 1f;
            Range.showRange = false;
            active = true;
        }

        public void Reborn(float t, string name, GameObject obj)
        {
            red.SetActive(true);
            obj.SetActive(true);

            StartCoroutine(Re(t, name));
        }
        IEnumerator Re(float t, string name)
        {
            Text rebornTime;
            rebornTime = GameObject.Find(name + "Red").transform.Find("Time").GetComponent<Text>();
            for (float i = t; i >= 0; i -= 0.1f)
            {
                //red = GameObject.Find(string.Concat(name, "Red"));
                rebornTime.text = i.ToString("f1");
                yield return new WaitForSeconds(0.1f);
                if (i <= 0)
                {
                    r.SetActive(true);
                    red.SetActive(false);
                }
            }
        }
    }

}