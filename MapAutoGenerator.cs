using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAutoGenerator : MonoBehaviour
{
    public GameObject block;
    public GameObject block1;
    public GameObject block2;
    public int raw;
    public int col;
    Vector3 posRed;
    Vector3 posBlue;
    void Start()
    {
        posRed = new Vector3(Random.Range(0, col), 0.5f, Random.Range(0, raw));
        posBlue = new Vector3(Random.Range(0, col), 0.5f, Random.Range(0, raw));
        Instantiate(block1,posRed, Quaternion.identity);
        Instantiate(block2, posBlue, Quaternion.identity);

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < raw; j++)
            {
                Instantiate(block, new Vector3(i, 0, j), Quaternion.identity);
            }
        }
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < raw; j++)
            {
                if (Random.Range(0, 100) > 55&&new Vector3(i,0.5f,j)!=posBlue && new Vector3(i, 0.5f, j) != posRed)
                    Instantiate(block, new Vector3(i, 0.5f, j), Quaternion.identity);
            }
        }

    }

   
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
