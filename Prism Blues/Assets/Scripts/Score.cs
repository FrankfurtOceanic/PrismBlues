using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    float scoreTimer;
    public int scoreVal = 0;
    private static int addedScore = 0;
    static bool increasedPoints = false;
    public float addspeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        scoreTimer = 0f;
        scoreVal = 0;
        addedScore = 0;
        increasedPoints = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (increasedPoints) 
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= addspeed) 
            {
                scoreVal += 1;
                scoreTimer = 0.0f;
            }
            if (scoreVal >= addedScore) 
            {
                increasedPoints = false;
            }
        }
        scoreText.text = "Score: " + scoreVal.ToString();
        
       
    }

    public static void addPoints(int points) 
    {
        addedScore += points;
        increasedPoints = true;
    }
}
