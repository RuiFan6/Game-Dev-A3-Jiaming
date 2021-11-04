using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{   
    public Sprite insideWall;
    public Sprite insideCorner;
    public Sprite outsideWall;
    public Sprite outsideCorner;
    public Sprite tJunc;
    public Sprite food;
    public Transform prefab;
    public int[,] levelMap;
    public int[] wallNums;
    public GameObject mp;
    public static List<Vector3> walkablePos;
    // Start is called before the first frame update
    void Start()
    {
        levelMap = new int[,] {
                        {1,2,2,2,2,2,2,2,2,2,2,2,2,7}, 
                        {2,5,5,5,5,5,5,5,5,5,5,5,5,4}, 
                        {2,5,3,4,4,3,5,3,4,4,4,3,5,4}, 
                        {2,6,4,0,0,4,5,4,0,0,0,4,5,4}, 
                        {2,5,3,4,4,3,5,3,4,4,4,3,5,3}, 
                        {2,5,5,5,5,5,5,5,5,5,5,5,5,5}, 
                        {2,5,3,4,4,3,5,3,3,5,3,4,4,4}, 
                        {2,5,3,4,4,3,5,4,4,5,3,4,4,3}, 
                        {2,5,5,5,5,5,5,4,4,5,5,5,5,4}, 
                        {1,2,2,2,2,1,5,4,3,4,4,3,0,4}, 
                        {0,0,0,0,0,2,5,4,3,4,4,3,0,3}, 
                        {0,0,0,0,0,2,5,4,4,0,0,0,0,0}, 
                        {0,0,0,0,0,2,5,4,4,0,3,4,4,0}, 
                        {2,2,2,2,2,1,5,3,3,0,4,0,0,0}, 
                        {0,0,0,0,0,0,5,0,0,0,4,0,0,0}
                    };
        wallNums = new int[] {1, 2, 3, 4, 7};
        walkablePos = new List<Vector3>();
        InitiateMap();
        //foreach(Vector3 vvv in walkablePos)
        //{
        //    Debug.Log(vvv);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitiateMap()
    {
        ReadArray();
    }

    void ReadArray()
    {
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                int ii = j - levelMap.GetLength(0);
                int jj = -i + levelMap.GetLength(1);
                PlaceSprite(ii+2, jj, levelMap[i,j], i, j);
                PlaceSprite(ii+2, -jj, levelMap[i,j], i, j);
                PlaceSprite(-ii-1, -jj, levelMap[i,j], i, j);
                PlaceSprite(-ii-1, jj, levelMap[i,j], i, j);
            }
        }
    }

    private void PlaceSprite(int a, int b, int position, int f, int g)
    {
        mp = new GameObject("mp" + "/" + a + "/" + b);
        mp.transform.position = new Vector3(a, b, 0f);
        var s = mp.AddComponent<SpriteRenderer>();
        var ccc = RotateCheck(f,g);
        if (ccc)
        {
            mp.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        }
        if (position == 0)
        {
            walkablePos.Add(new Vector3(a, b, 0f));
        }
        else if (position == 1)
        {
            //Debug.Log("OutCorner");
            //var s = mp.AddComponent<SpriteRenderer>();
            s.sprite = outsideCorner;
        }
        else if (position == 2)
        {
            //Debug.Log("OutWall");
            s.sprite = outsideWall;
        }
        else if (position == 3)
        {
            //Debug.Log("InCorner");
            s.sprite = insideCorner;
        }
        else if (position == 4)
        {
            //Debug.Log("InWall");
            s.sprite = insideWall;
        }
        else if (position == 5)
        {
            //Debug.Log("pellet");
            walkablePos.Add(new Vector3(a, b, 0f));
            s.sprite = food;
        }
        else if (position == 6)
        {   
            //GameObject ps = GameObject.Find("PowerSweet");
            //mp = GameObject.Find("PowerSweet");
            //mp.transform.position = new Vector3(a, b, 0f);
            walkablePos.Add(new Vector3(a, b, 0f));
            Instantiate(prefab, new Vector3(a, b, 0f), Quaternion.identity);
        }
        else if (position == 7)
        {
            //Debug.Log("Tjunction");
            s.sprite = tJunc;
        }
        else
        {
            Debug.Log("only 7 sprites allowed!!!");
        }
    }

    private bool RotateCheck(int m, int n)
    {
        if (m >= 1 && m < (levelMap.GetLength(0) - 1) && n >= 1 && n < (levelMap.GetLength(1) - 1))
        {
            if (Array.Exists(wallNums, element => element == levelMap[m,n]) == true
            && Array.Exists(wallNums, element => element == levelMap[m+1,n]) == true
            && Array.Exists(wallNums, element => element == levelMap[m-1,n]) == true)
            //&& Array.Exists(wallNums, element => element == levelMap[m,n+1]) == false
            //&& Array.Exists(wallNums, element => element == levelMap[m,n-1]) == false)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        else if ((n == 0 || n == (levelMap.GetLength(1) - 1)) && m != 0 && m != (levelMap.GetLength(0) - 1))
        {   
            if (Array.Exists(wallNums, element => element == levelMap[m,n]) == true
            && Array.Exists(wallNums, element => element == levelMap[m+1,n]) == true
            && Array.Exists(wallNums, element => element == levelMap[m-1,n]) == true)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        else if ( n != 0 && n != (levelMap.GetLength(1) - 1))
        {   
            if (m == 0 
            && Array.Exists(wallNums, element => element == levelMap[m,n]) == true
            && Array.Exists(wallNums, element => element == levelMap[m+1,n]) == true)
            {
                return (true);
            }
            if (m == (levelMap.GetLength(0) - 1)
            && Array.Exists(wallNums, element => element == levelMap[m,n]) == true
            && Array.Exists(wallNums, element => element == levelMap[m-1,n]) == true)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        else
            {
                return (false);
            }
    }

}
