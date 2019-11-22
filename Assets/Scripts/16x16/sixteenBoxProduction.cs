using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sixteenBoxProduction : MonoBehaviour
{
    [SerializeField]
    private GameObject GameBox;

    public static int Horizon = 16;
    public static int Vertical = 16;

    public bool[,] Grid = new bool[Horizon, Vertical];



    private void Awake()
    {
        createBoard();

    }




    void createBoard()
    {

        int OriginYpos = -2;



        for (int i = 0; i < Horizon; i++)
        {
            for (int j = 0; j < Vertical; j++)
            {
                bool MineExisted = Random.value < 0.1;

                GameBox.GetComponent<sixteenBoxScript>().mine = MineExisted;

                Instantiate(GameBox, transform.position, Quaternion.identity);

                Grid[i, j] = GameBox.GetComponent<sixteenBoxScript>().mine;

                transform.position += new Vector3(0, 0.32f, 0);



            }
            transform.position = new Vector3(transform.position.x, OriginYpos, 0);
            transform.position += new Vector3(0.32f, 0, 0);


        }

    }


}
