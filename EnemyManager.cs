using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject _worm;
    public GameObject _thug;
    public GameObject _witch;
    public GameObject _armedMan;
    public GameObject _ironShield;
    public GameObject _dog;
    public GameObject _bombWorm;
    public static Transform SpawnPoint;
    public GameObject top;
    int cWorm = 4;
    int cThug = 7;
    int cWitch = 22;
    int cArmedMan = 40;
    int cIronShield = 17;
    int cBombWorm = 23;
    int cDog = 9;
    public Text wormC;
    public Text thugC;
    public Text witchC;
    public Text armedManC;
    public Text ironShieldC;
    public Text dogC;
    public Text bombWormC;
    void Start()
    {

    }

    public void Set(string name)
    {
        switch (name)
        {
            case "worm":
                if (CostManager.cost - cWorm >= 0)
                {
                    CostManager.cost -= cWorm;
                    Instantiate(_worm, SpawnPoint.position, Quaternion.identity);
                    cWorm++;
                    wormC.text = cWorm.ToString();

                }
                break;
            case "thug":
                if (CostManager.cost - cThug >= 0)
                {
                    CostManager.cost -= cThug;
                    Instantiate(_thug, SpawnPoint.position, Quaternion.identity);
                    cThug++;
                    thugC.text = cThug.ToString();

                }
                break;
            case "witch":
                if (CostManager.cost - cWitch >= 0)
                {
                    CostManager.cost -= cWitch;
                    Instantiate(_witch, SpawnPoint.position, Quaternion.identity);
                    cWitch += 6;
                    witchC.text = cWitch.ToString();

                }
                break;
            case "armedMan":

                if (CostManager.cost - cArmedMan >= 0)
                {
                    CostManager.cost -= cArmedMan;
                    Instantiate(_armedMan, SpawnPoint.position, Quaternion.identity);
                    cArmedMan += 5;
                    armedManC.text = cArmedMan.ToString();

                }
                break;
            case "bombWorm":

                if (CostManager.cost - cBombWorm >= 0)
                {
                    CostManager.cost -= cBombWorm;
                    cBombWorm += 5;
                    Instantiate(_bombWorm, SpawnPoint.position, Quaternion.identity);
                    bombWormC.text = cBombWorm.ToString();
                }
                break;
            case "dog":
                if (CostManager.cost - cDog >= 0)
                {
                    CostManager.cost -= cDog;
                    cDog++;
                    dogC.text = cDog.ToString();
                    Instantiate(_dog, SpawnPoint.position, Quaternion.identity);
                }
                break;
            case "ironShield":
                if (CostManager.cost - cIronShield >= 0)
                {
                    CostManager.cost -= cIronShield;
                    cIronShield += 2;
                    ironShieldC.text = cIronShield.ToString();
                    Instantiate(_ironShield, SpawnPoint.position, Quaternion.identity);
                }
                break;
        }

    }
  
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LayerMask sp = 1 << 4;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit m, 100, sp.value);
            if (m.transform && m.transform.tag == "SpawnPoint")
            {
                SpawnPoint = m.transform;
                top.transform.position = m.transform.position + new Vector3(0, 0.3f, 0);

            }


        }


    }
}
