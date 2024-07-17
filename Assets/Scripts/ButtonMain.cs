using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMain : MonoBehaviour
{
    public void StartGame(string Name)
    {
        SceneManager.LoadScene(Name);

    }

}
