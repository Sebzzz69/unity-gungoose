using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

 {

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Win");
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("Lose");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Testing2"); 
    }
}
