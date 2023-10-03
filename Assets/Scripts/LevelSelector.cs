using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public Text buttonTitle;

    public void Awake()
    {
        buttonTitle.text = name.Split(" ")[name.Split(" ").Length - 1].ToString(); 

    }
    public void LoadScene()
    {
        SceneManager.LoadScene(name.ToString()); 

    }

}
