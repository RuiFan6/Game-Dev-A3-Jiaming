using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{   
    public Sprite insideWall;
    public Sprite insideCorner;
    public Sprite outsideWall;
    public Sprite outsideCorner;
    public Sprite tJunc;
    public Sprite food;
    public Sprite powerFood;
    public GameObject piece;
    public int[,] levelMap;
    private int verticalSize;
    private int horizontalSize;
    private int rows;
    private int columns;
    public int[] wallNums; 

    // Start is called before the first frame update
    void Start()
    {
        verticalSize = (int)Camera.main.orthographicSize;
        horizontalSize = verticalSize * (Screen.width / Screen.height);
        columns = 2 * horizontalSize;
        rows = 2 * verticalSize;
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
        InitiateMap();
        //Debug.Log(levelMap);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitiateMap() //Call ArrayToMap(), RotateMap(), and MirrorMap to build the map
    {
        CreateMap();
        RotateMap();
        //MirrorMap();
    }

    void CreateMap() //Find corresponding sprite for each position on the map by the array
    {
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                Debug.Log(levelMap[i,j]);
                PlaceSprite(i, j, levelMap[i,j]);
            }
        }
    }

    private void PlaceSprite(int aa, int bb, int position) //Place sprite depending on array index and array value
    {   
        GameObject mp = new GameObject("mp" + aa + "/" + bb);
        mp.transform.position = new Vector3(bb , 0-aa , 0f);
        var s = mp.AddComponent<SpriteRenderer>();
        if (position == 0)
        {
            Debug.Log("Empty");
        }
        else if (position == 1)
        {
            Debug.Log("OutCorner");
            //var s = mp.AddComponent<SpriteRenderer>();
            s.sprite = outsideCorner;
        }
        else if (position == 2)
        {
            Debug.Log("OutWall");
            s.sprite = outsideWall;
        }
        else if (position == 3)
        {
            Debug.Log("InCorner");
            s.sprite = insideCorner;
        }
        else if (position == 4)
        {
            Debug.Log("InWall");
            s.sprite = insideWall;
        }
        else if (position == 5)
        {
            Debug.Log("pellet");
            s.sprite = food;
        }
        else if (position == 6)
        {
            Debug.Log("Big pellet");
            s.sprite = powerFood;
        }
        else if (position == 7)
        {
            Debug.Log("Tjunction");
            s.sprite = tJunc;
        }
        else
        {
            Debug.Log("only 7 sprites allowed!!!");
        }
    }

    private void RotateMap() //Rotate the sprite depending on its neighbours
    {
        InsideRotate();
        EdgeRotate();
        CornerRotate();
    }

    private void InsideRotate()
    {
        for (int a = 1; a < (levelMap.GetLength(0) - 1); a++)
        {
            for (int b = 1; (b < levelMap.GetLength(1) - 1); b++)
            {
                //Debug.Log("Inside" + levelMap[a,b]);
                RotateIn(a, b);
            }
        }
    }

    private void EdgeRotate()
    {
        for (int e = 0; e < levelMap.GetLength(0); e++)
        {   
            if (e == 0 || e == levelMap.GetLength(0) -1)
            {
                for (int f = 1; (f < levelMap.GetLength(1) - 1); f++)
                {
                    //Debug.Log("--Edge" + levelMap[e,f]);
                    ///Debug.Log("--Edge" + e + " / " + f);
                    piece = GameObject.Find("mp" + e + "/" + f);
                    if (e == 0 && Array.Exists(wallNums, element => element == levelMap[e,f]) == true)
                    {
                        if (Array.Exists(wallNums, element => element == levelMap[e,f-1]) == false && Array.Exists(wallNums, element => element == levelMap[e+1,f]) == true && Array.Exists(wallNums, element => element == levelMap[e,f+1]) == false)
                        {
                            //Debug.Log("Rotate 90 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        }
                        else if (Array.Exists(wallNums, element => element == levelMap[e,f-1]) == false && Array.Exists(wallNums, element => element == levelMap[e+1,f]) == true && Array.Exists(wallNums, element => element == levelMap[e,f+1]) == true)
                        {
                            //Debug.Log("Rotate 180 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                        }
                        else if (Array.Exists(wallNums, element => element == levelMap[e,f-1]) == true && Array.Exists(wallNums, element => element == levelMap[e+1,f]) == true && Array.Exists(wallNums, element => element == levelMap[e,f+1]) == false)
                        {
                            //Debug.Log("Rotate 270 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
                        }
                    }
                    else if (e == levelMap.GetLength(0) -1 && Array.Exists(wallNums, element => element == levelMap[e,f]) == true)
                    {
                        if (Array.Exists(wallNums, element => element == levelMap[e,f-1]) == false && Array.Exists(wallNums, element => element == levelMap[e-1,f]) == true && Array.Exists(wallNums, element => element == levelMap[e,f+1]) == true)
                        {
                            //Debug.Log("Rotate 90 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        }
                        else if (Array.Exists(wallNums, element => element == levelMap[e,f-1]) == true && Array.Exists(wallNums, element => element == levelMap[e-1,f]) == false && Array.Exists(wallNums, element => element == levelMap[e,f+1]) == false)
                        {
                            //Debug.Log("Rotate 270 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
                        }
                        else if (Array.Exists(wallNums, element => element == levelMap[e,f-1]) == false && Array.Exists(wallNums, element => element == levelMap[e-1,f]) == true && Array.Exists(wallNums, element => element == levelMap[e,f+1]) == false)
                        {
                            //Debug.Log("Rotate 90 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        }
                    }
                }
            }
        }

        for (int g = 0; g < levelMap.GetLength(1); g++)
        {   
            if (g == 0 || g == levelMap.GetLength(1) -1)
            {
                for (int h = 1; (h < levelMap.GetLength(0) - 1); h++)
                {
                    //Debug.Log("Edge" + levelMap[h,g]);
                    //Debug.Log("--Edge" + h + " / " + g);
                    piece = GameObject.Find("mp" + h + "/" + g);
                    if(g == 0 && Array.Exists(wallNums, element => element == levelMap[h,g]) == true)
                    {
                        if (Array.Exists(wallNums, element => element == levelMap[h-1,g]) == true && Array.Exists(wallNums, element => element == levelMap[h,g+1]) == false && Array.Exists(wallNums, element => element == levelMap[h+1,g]) == true)
                        {
                            //Debug.Log("Rotate 90 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        }
                        else if (Array.Exists(wallNums, element => element == levelMap[h-1,g]) == false && Array.Exists(wallNums, element => element == levelMap[h,g+1]) == true && Array.Exists(wallNums, element => element == levelMap[h+1,g]) == true)
                        {
                            //Debug.Log("Rotate 180 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                        }
                        else if (Array.Exists(wallNums, element => element == levelMap[h-1,g]) == true && Array.Exists(wallNums, element => element == levelMap[h,g+1]) == false && Array.Exists(wallNums, element => element == levelMap[h+1,g]) == false)
                        {
                            //Debug.Log("Rotate 90 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
                        }
                        else if (Array.Exists(wallNums, element => element == levelMap[h-1,g]) == false && Array.Exists(wallNums, element => element == levelMap[h,g+1]) == false && Array.Exists(wallNums, element => element == levelMap[h+1,g]) == true)
                        {
                            //Debug.Log("Rotate 90 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
                        }
                    }
                    else if(g == levelMap.GetLength(1) -1 && Array.Exists(wallNums, element => element == levelMap[h,g]) == true)
                    {
                        if (Array.Exists(wallNums, element => element == levelMap[h-1,g]) == true && Array.Exists(wallNums, element => element == levelMap[h,g-1]) == false && Array.Exists(wallNums, element => element == levelMap[h+1,g]) == true)
                        {
                            Debug.Log("Rotate 90 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
                        }
                        else if (Array.Exists(wallNums, element => element == levelMap[h-1,g]) == false && Array.Exists(wallNums, element => element == levelMap[h,g-1]) == true && Array.Exists(wallNums, element => element == levelMap[h+1,g]) == true)
                        {
                            Debug.Log("Rotate 270 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        }
                        else if (Array.Exists(wallNums, element => element == levelMap[h-1,g]) == true && Array.Exists(wallNums, element => element == levelMap[h,g-1]) == false && Array.Exists(wallNums, element => element == levelMap[h+1,g]) == false)
                        {
                            Debug.Log("Rotate 90 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
                        }
                        else if (Array.Exists(wallNums, element => element == levelMap[h-1,g]) == false && Array.Exists(wallNums, element => element == levelMap[h,g-1]) == false && Array.Exists(wallNums, element => element == levelMap[h+1,g]) == true)
                        {
                            Debug.Log("Rotate 90 here---------");
                            piece.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
                        }
                    }
                    
                }
            }
        }
    }

    private void CornerRotate()
    {
        for (int c = 0; c < levelMap.GetLength(0); c++)
        {   
            if (c == 0 || c == levelMap.GetLength(0) -1)
            {
                for (int d = 0; d < levelMap.GetLength(1); d++)
                {   
                    if (d == 0 || d == levelMap.GetLength(1) -1)
                    Debug.Log("Inside" + c + "/" + d);
                    RotateOut(c,d);
                }
            }
        }
    }

    private void RotateIn(int x1, int y1)
    {
        //Debug.Log(x1+","+y1+" ready to rotate");
        //Debug.Log(levelMap[x1,y1]);
        //Debug.Log(Array.Exists(wallNums, element => element == levelMap[x1,y1]));
        piece = GameObject.Find("mp" + x1 + "/" + y1);
        if (Array.Exists(wallNums, element => element == levelMap[x1,y1]) == true)
        {
            if ( (Array.Exists(wallNums, element => element == levelMap[x1+1,y1]) == true) && (Array.Exists(wallNums, element => element == levelMap[x1-1,y1]) == true) )
            {
                //Debug.Log("Rotate 90 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            }
            else if ( (Array.Exists(wallNums, element => element == levelMap[x1-1,y1]) == true) && (Array.Exists(wallNums, element => element == levelMap[x1,y1+1]) == true) )
            {
                //Debug.Log("Rotate 270 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
            }
            else if ( (Array.Exists(wallNums, element => element == levelMap[x1+1,y1]) == true) && (Array.Exists(wallNums, element => element == levelMap[x1,y1+1]) == true) )
            {
                //Debug.Log("Rotate 180 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
            }
            else if ( (Array.Exists(wallNums, element => element == levelMap[x1+1,y1]) == true) && (Array.Exists(wallNums, element => element == levelMap[x1,y1-1]) == true) )
            {
                //Debug.Log("Rotate 90 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            }
        }
    }

    private void RotateOut(int x2, int y2)
    {
        //Debug.Log(x2+","+y2+" ready to rotate");
        //Debug.Log(levelMap[x2,y2]);
        //Debug.Log(Array.Exists(wallNums, element => element == levelMap[x2,y2]));
        piece = GameObject.Find("mp" + x2 + "/" + y2);
        if (Array.Exists(wallNums, element => element == levelMap[x2,y2]) == true)
        {
            if ( x2 == 0 && y2 == 0 && (Array.Exists(wallNums, element => element == levelMap[x2+1,y2]) == true) && (Array.Exists(wallNums, element => element == levelMap[x2,y2+1]) == true) )
            {
                //Debug.Log("Rotate 180 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
            }
            else if ( x2 == levelMap.GetLength(0) -1 && y2 == 0 && (Array.Exists(wallNums, element => element == levelMap[x2-1,y2]) == true) && (Array.Exists(wallNums, element => element == levelMap[x2,y2+1]) == true) )
            {
                //Debug.Log("Rotate 90 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            }
            else if ( x2 == 0 && y2 == levelMap.GetLength(1) -1 && (Array.Exists(wallNums, element => element == levelMap[x2,y2-1]) == true) && (Array.Exists(wallNums, element => element == levelMap[x2+1,y2]) == true) )
            {
                //Debug.Log("Rotate 270 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 270.0f, Space.Self);
            }
            
            else if ( x2 == 0 && y2 == 0 && (Array.Exists(wallNums, element => element == levelMap[x2+1,y2]) == true) && (Array.Exists(wallNums, element => element == levelMap[x2,y2+1]) == false) )
            {
                //Debug.Log("Rotate 90 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            }
            else if ( x2 == 0 && y2 == levelMap.GetLength(1) -1 && (Array.Exists(wallNums, element => element == levelMap[x2+1,y2]) == true) && (Array.Exists(wallNums, element => element == levelMap[x2,y2-1]) == false) )
            {
                //Debug.Log("Rotate 90 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            }
            else if ( x2 == levelMap.GetLength(0) -1 && y2 == 0 && (Array.Exists(wallNums, element => element == levelMap[x2-1,y2]) == true) && (Array.Exists(wallNums, element => element == levelMap[x2,y2+1]) == false) )
            {
                //Debug.Log("Rotate 90 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            }
            else if ( x2 == levelMap.GetLength(0) -1 && y2 == levelMap.GetLength(1) && (Array.Exists(wallNums, element => element == levelMap[x2-1,y2]) == true) && (Array.Exists(wallNums, element => element == levelMap[x2,y2-1]) == false) )
            {
                //Debug.Log("Rotate 90 here---------");
                piece.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            }

        }
    }

    
}
