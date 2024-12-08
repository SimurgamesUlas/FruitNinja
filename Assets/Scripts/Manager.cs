using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoretext;
    void Start()
    {
        
    }

    
    void Update()
    {
        GameSCore();
    }

    private void GameSCore(){
        scoretext.text = score.ToString();
    }
    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
