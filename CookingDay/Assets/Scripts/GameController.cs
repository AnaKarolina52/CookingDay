using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

   public GameObject gameOver;

   public GameObject WinnerPanel;

   public static GameController instance;


   //audiocontroller

   public AudioClip sfxWinGame;
   public AudioClip sfxLoseGame;
   public AudioController audioController;

    // Start is called before the first frame update
   public void Start()
    {
        instance = this;
        WinnerPanel.SetActive(false); 
       
    }


    public void ShowGameOver()
    {
        audioController.PlaySFX(sfxLoseGame);
        gameOver.SetActive(true);
    }


    public void RestartGame()

    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OnQuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void GameCompleted()
    {
        audioController.PlaySFX(sfxWinGame);
        WinnerPanel.SetActive(true);

    }

}
