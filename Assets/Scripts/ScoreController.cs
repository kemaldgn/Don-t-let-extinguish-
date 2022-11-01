using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    
    public static int scoreNum = 0;

    public static int finalScoreVal;

    public Text score;

    public Text finalScore;
   
   public Text highScore;

    
    void Start()
    {
        
        highScore.text = "Highest Score = "+PlayerPrefs.GetInt("highScore", 0).ToString();
    
    }

 
    void Update()
    {
        score.text = "= "+ scoreNum;
        finalScore.text = "= "+ finalScoreVal;

        if(scoreNum > PlayerPrefs.GetInt("highScore", 0)){
            PlayerPrefs.SetInt("highScore", scoreNum);
            highScore.text = "Highest Score = "+ scoreNum;
        }
       
    }

    public void Reset(){
        PlayerPrefs.DeleteKey("highScore");
    }

}
