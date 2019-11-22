using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tag;

public class boxScript : MonoBehaviour
{
    public bool mine;

    public Sprite[] emptyBoxElement;
    public Sprite mineElement;

    public static int Horizon = 10;
    public static int Vertical = 10;


    private int Xpos;
    private int Ypos;

    private int OriginXpos =3;
    private int OriginYpos = 4;

    private float xMargin=0.33f;
    private float yMargin = 0.32f;

    private boxproduction BoxProduction;

    private bool[,] GameGrid;

    private void Start()
    {
        Xpos =Mathf.RoundToInt( (transform.position.x -OriginXpos)/xMargin);
        Ypos = Mathf.RoundToInt((transform.position.y -OriginYpos)/yMargin);

        BoxProduction = GameObject.FindGameObjectWithTag(Tag.GameBox.boxProduction).GetComponent<boxproduction>();
        GameGrid = BoxProduction.Grid;
    }

    public void loadElement(int adjacentCount)
    {
        if (mine)
        {
            GetComponent<SpriteRenderer>().sprite = mineElement;
        }

        else
        {
            GetComponent<SpriteRenderer>().sprite = emptyBoxElement[adjacentCount];
        }
           
    }

    bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "box";
    }



    private void OnMouseUpAsButton()
    {
        if (isCovered())
        {
            if (mine)
            {
                showAllMine();
                print("You lose!");
            }
            else
            {
                loadElement(adjacentMines(Xpos,Ypos));
                
            }
        }
    }


    void showAllMine()
    {
        GameObject[] boxProduction = GameObject.FindGameObjectsWithTag(Tag.GameBox.box);
         foreach( GameObject Element in boxProduction)
          {
              if (Element.GetComponent<boxScript>().mine)
              {
                Element.GetComponent<boxScript>().loadElement(0);
              }



          }
       
    }
     bool minePosition(int x, int y)
    {   

        
        if (x >= 0 && y >= 0 && x < Horizon && y < Vertical)
        {
            return GameGrid[x, y];

        }
        
        return false;
    }


    int adjacentMines(int x, int y)
    {
        int count = 0;

        if (minePosition(x, y + 1)) { count++; }     // 上
        if (minePosition(x + 1, y + 1)) {count++; }  // 右上
        if (minePosition(x + 1, y)) { count++; }     // 右
        if (minePosition(x + 1, y - 1)) { count++; } //右下
        if (minePosition(x, y - 1)) { count++; }     // 下
        if (minePosition(x - 1, y - 1)) { count++; } //左下
        if (minePosition(x - 1, y)) { count++; }     // 左
        if (minePosition(x - 1, y + 1)) { count++; } // 左上

        return count;
    }
}
