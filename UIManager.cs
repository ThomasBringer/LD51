using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highscoreText;

    [SerializeField] GameObject pressStart;
    [SerializeField] GameObject gameOver;

    void Start(){
        pressStart.SetActive(GameManager.gm.firstGame);
        gameOver.SetActive(!GameManager.gm.firstGame);

        scoreText.text = "Score:\n"+FillScoreText(GameManager.gm.score);
        highscoreText.text = "HighScore:\n"+FillScoreText(GameManager.gm.highScore);
    }

    string FillScoreText(int score){
        string scoreString = score.ToString();
        int scoreLength = scoreString.Length;

        string txt = "";

        for (int i = 0; i < 4-scoreLength; i++)
            txt = txt + "0";

        txt = txt + scoreString;
        return txt;
        // if(scoreLength < 4){
            
        //     for (var i = 0; i <= 5; i++) result = $"{result}{i}";
        // }

        // return (scoreLength < 4 ? "0"*(4-scoreLength) : "") + scoreString;
    }
}
