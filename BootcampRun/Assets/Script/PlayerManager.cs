using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using TMPro; 

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStart;
    public GameObject startText;

    public static int numberOfCoins;
    public TextMeshProUGUI coinsText;

    public static bool nextLevel;
    public GameObject nextLevelPanel;
    // Start is called before the first frame update
    void Start()
    {
        gameOver=false;
        Time.timeScale=  1;
        isGameStart = false;
        numberOfCoins =0;
        nextLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            Time.timeScale=0;
            gameOverPanel.SetActive(true);
        }
        coinsText.text="Coins:"+numberOfCoins;
        if(SwipeManager.tap)
        {
            isGameStart =true;
            Destroy(startText);
        }        
        if(nextLevel)
        {
            Time.timeScale=0;
            nextLevelPanel.SetActive(true);
        }
    }
}
