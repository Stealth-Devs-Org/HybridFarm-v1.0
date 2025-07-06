using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public GameObject attackAlgorithm;
    public GameObject loadingScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnLevelButtonClicked(string level)
    {
        StartCoroutine(LoadingScreen(level));
    }

    IEnumerator LoadingScreen(string level)
    {
        loadingScreen.SetActive(true);

        if (level == "1. Level_1") // No predator attack in level 1
        {
            loadingScreen.SetActive(false);
            SceneManager.LoadScene(level);
        }
        else
        {

            loadingScreen.SetActive(false);
            SceneManager.LoadScene(level);
    
  
        }
        yield break;
    }
}
