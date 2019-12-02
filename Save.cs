using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<int> PositionX = new List<int>();
    public List<double> PositionY = new List<double>();
    public List<double> PositionZ = new List<double>();
    public List<int> Types = new List<int>();
    public  int SaveNum;
    //public int[,] blockMap = new int[7, 8]
    //{
    //       {1,4,3,1,4,2,3,1 },
    //       {4,2,4,4,1,2,3,4 },
    //       {1,2,3,1,1,2,3,1 },
    //       {4,2,3,1,1,2,3,4 },
    //       {1,4,3,4,1,2,0,0 },
    //       {4,2,3,1,4,2,0,1 },
    //       {1,4,3,4,1,4,4,1 },
    //   };


    public int shootNum = 0;
    public int score = 0;
}