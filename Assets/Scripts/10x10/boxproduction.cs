using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tag;

public class boxproduction : MonoBehaviour
{
    [SerializeField]
    private GameObject GameBox;

    public static int Horizon = 10;
    public static int Vertical = 10;

    public bool[,] Grid= new bool[Horizon,Vertical];

    private void Awake()
    {  
        createBoard();
    }

    private void createBoard()
    {
        int OriginYpos = 4;
        for(int i = 0; i < Horizon; i++)
        {
            for(int j = 0; j < Vertical; j++)
            {
                bool MineExisted = Random.value < 0.1;

                GameBox.GetComponent<boxScript>().mine =MineExisted;

                Instantiate(GameBox, transform.position, Quaternion.identity);

                Grid[i, j] = GameBox.GetComponent<boxScript>().mine;

                transform.position += new Vector3(0, 0.32f, 0);
            }
            transform.position = new Vector3(transform.position.x,OriginYpos, 0);
            transform.position += new Vector3( 0.33f,0, 0);
        }
    }
}
