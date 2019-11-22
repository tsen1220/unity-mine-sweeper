using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }


    public void EnterTenGame()
    {
        SceneManager.LoadScene(1);
    }
    public void EnterSixteenGame()
    {
        SceneManager.LoadScene(2);
    }
    public void EnterThirtyGame()
    {
        SceneManager.LoadScene(3);
    }

}
