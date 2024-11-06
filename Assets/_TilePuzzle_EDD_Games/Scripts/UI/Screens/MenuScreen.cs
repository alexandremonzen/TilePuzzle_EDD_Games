using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : BaseScreen
{
    //prov
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
