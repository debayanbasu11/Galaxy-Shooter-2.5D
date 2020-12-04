using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public int score;
    public GameObject titleScreen;

    public void UpdateLives(int currentLives){
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore(){
        score += 10;

        scoreText.text = "Score: "+ score;
    }

    public void ShowTitleScreen(){
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen(){
        titleScreen.SetActive(false);
        scoreText.text = "Score: 00";
    }

}
