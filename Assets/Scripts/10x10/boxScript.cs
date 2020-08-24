using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private Queue<int> breadthSearch = new Queue<int>();

    private boxproduction BoxProduction;

    private Text Condition;

    private bool[,] GameGrid;

    private void Start()
    {
        Xpos =Mathf.RoundToInt( (transform.position.x -OriginXpos)/xMargin);
        Ypos = Mathf.RoundToInt((transform.position.y -OriginYpos)/yMargin);
        BoxProduction = GameObject.FindGameObjectWithTag(Tag.GameBox.boxProduction).GetComponent<boxproduction>();
        Condition = GameObject.FindGameObjectWithTag(Tag.UI.Condition).GetComponent<Text>();
        GameGrid = BoxProduction.Grid;
    }

    private void Update()
    {
        if (isFinished())
        {
            print("You Win!");
            GameObject[] boxProduction = GameObject.FindGameObjectsWithTag(Tag.GameBox.box);
            foreach (GameObject Element in boxProduction)
            {
                Element.GetComponent<boxScript>().enabled = false;
                Element.GetComponent<BoxCollider2D>().enabled = false;
            }
            Condition.text = "You Win!";
        }
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

    private bool isCovered()
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
                GameObject[] boxProduction = GameObject.FindGameObjectsWithTag(Tag.GameBox.box);
                foreach (GameObject Element in boxProduction)
                {
                    Element.GetComponent<boxScript>().enabled=false;
                    Element.GetComponent<BoxCollider2D>().enabled = false;
                }
                Condition.text = "You Lose!";
            }
            else
            {
                breadthFirstSearch(Xpos,Ypos);
                searchField();
                loadElement(adjacentMines(Xpos,Ypos));
            }
        }
    }


    private void showAllMine()
    {
        GameObject[] boxProduction = GameObject.FindGameObjectsWithTag(Tag.GameBox.box);
        foreach (GameObject Element in boxProduction)
        {
            if (Element.GetComponent<boxScript>().mine)
            {
                Element.GetComponent<boxScript>().loadElement(0);
            }
        }
    }

    private bool minePosition(int x, int y)
    {   
        if (x >= 0 && y >= 0 && x < Horizon && y < Vertical)
        {
            return GameGrid[x, y];
        }
        
        return false;
    }


    private int adjacentMines(int x, int y)
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

    private void breadthFirstSearch(int x,int y)
    {
        int Xborder = -1;
        int Yborder = -1;

        while (Yborder < 2)
        {
            if (Xborder < 2)
            {
                if (Xborder !=0 || Yborder != 0)
                {
                    if(x+Xborder >= 0 && y+Yborder>= 0)
                    {
                    breadthSearch.Enqueue(x + Xborder);
                    breadthSearch.Enqueue(y + Yborder);
                    }
                }
                Xborder++;
            }
            else
            {
                Yborder++;
                Xborder = -1;
            }
        }
    }

    private void searchField()
    {
        int X = breadthSearch.Dequeue();
        int Y = breadthSearch.Dequeue();
        if (!minePosition(X,Y))
        {
            GameObject[] boxProduction = GameObject.FindGameObjectsWithTag(Tag.GameBox.box);

            foreach (GameObject Element in boxProduction)
            {
                int XPosElement = Mathf.RoundToInt((Element.transform.position.x - OriginXpos) / xMargin);
                int YPosElement = Mathf.RoundToInt((Element.transform.position.y - OriginYpos) / yMargin);

                if (XPosElement  == X && YPosElement == Y)
                {
                    Element.GetComponent<boxScript>().loadElement(adjacentMines(X,Y));
                }
            }
            breadthFirstSearch(X, Y);
            searchField();
        }
    }

    private bool isFinished()
    {
        GameObject[] boxProduction = GameObject.FindGameObjectsWithTag(Tag.GameBox.box);
        foreach (GameObject Element in boxProduction)
        {
            if (!Element.GetComponent<boxScript>().mine && Element.GetComponent<SpriteRenderer>().sprite.texture.name == "box")
            {
                return false;
            }
        }
        return true;
    }
}
