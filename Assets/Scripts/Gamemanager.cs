using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{

    public bool gameOver = false;
    public bool gameCompleted = false;

    public bool gamePaused = false;

    public bool callAdGameCompleted = false;

    public bool callAdGameOver = false;

    public bool callAdDoubleXJoin = false;

    public bool callAdGuard = false;

    public bool startTimer = false;
    public TextMeshProUGUI enemyRemaining;

    public bool doubleCharacter = false;

    public bool guard = false;

    public bool guardMethod = false;

    public int coins = 0;
    public GameObject counter;
    public float counterFloat = 15f;

    public GameObject PauseMenu;

    public GameObject GameOverMenu;

    public GameObject GameCompleteMenu;

    public TextMeshProUGUI coinsText;

    public TextMeshProUGUI currentCoinsText;

    public TextMeshProUGUI levelText;

    public Image levelFill;

    public int totalEnemies;

    public AudioSource backgroundMusic;

    public GameObject loading;
    public GameObject[] levels;
    // Start is called before the first frame update
    void Start()
    {
        loading.SetActive(false);
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        GameCompleteMenu.SetActive(false);

        callAdGameCompleted = false;
        callAdGameOver = false;

        Debug.Log(levels.Length);

        if(PlayerPrefs.GetInt("Level", 1) <= levels.Length)
        {
            for(int i = 0; i<levels.Length; i++)
            {
                levels[i].SetActive(false);
            }
            levels[PlayerPrefs.GetInt("Level", 1)-1].SetActive(true);
        }
        else
        {
            for(int i = 0; i<levels.Length; i++)
            {
                levels[i].SetActive(false);
            }
            int rand = Random.Range(0, PlayerPrefs.GetInt("Level", 1)-1);
            levels[rand].SetActive(true);
        }


        Debug.Log(PlayerPrefs.GetInt("Level", 1));

        GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().player = GameObject.FindGameObjectWithTag("Main Player");
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("Sound", 1)==1)
        {
            backgroundMusic.mute = false;
        }
        else
        {
            backgroundMusic.mute = true;
        }
        levelText.text = PlayerPrefs.GetInt("Level", 1)+1+"";
        enemyRemaining.text = GameObject.FindGameObjectsWithTag("Enemy").Length+"";

        // Debug.Log(((totalEnemies - GameObject.FindGameObjectsWithTag("Enemy").Length)/ totalEnemies));

        levelFill.fillAmount = ((totalEnemies - GameObject.FindGameObjectsWithTag("Enemy").Length)/ totalEnemies);

        currentCoinsText.text = PlayerPrefs.GetInt("Coin", 0) + coins + "";

        if(GameObject.FindGameObjectsWithTag("Main Player").Length == 0)
        {
            gameOver = true;
        }

        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            gameCompleted = true;
        }

        if(gameCompleted)
        {
            GameCompleted();
        }

        if(gameOver)
        {
            GameOver();
        }

        if(startTimer)
        {
            counter.SetActive(true);
            counterFloat -= Time.deltaTime;
            if(counterFloat < 0)
            {
                startTimer = false;
            }
            if(guardMethod)
            {
                guard = true;
            }
            else
            {
                guard = false;
            }
            counter.GetComponent<TextMeshProUGUI>().text = Mathf.Round(counterFloat)+"";
        }
        else
        {
            counter.SetActive(false);
            guardMethod = false;
            if(guardMethod)
            {
                guard = true;
            }
            else
            {
                guard = false;
                callAdGuard = false;
            }

            if(doubleCharacter)
            {
                doubleCharacter = false;
                callAdDoubleXJoin = false;
            }
        }

    }

    public void CounterMethodGuard()
    {
        if(!callAdGuard)
        {
            PlayerPrefs.SetInt("Video ad", 5);
           // GameObject.Find("Ad Control").GetComponent<AdControl>().ShowRewardedVideoAd();
            callAdGuard = true;
        }
        
    }

    public void CounterMethodDoubleCharacter()
    {
        if(!callAdDoubleXJoin)
        {
            PlayerPrefs.SetInt("Video ad", 6);
           // GameObject.Find("Ad Control").GetComponent<AdControl>().ShowRewardedVideoAd();
            callAdDoubleXJoin = true;
        }
    }

    public void NextLevel()
    {
        int level = PlayerPrefs.GetInt("Level", 1);
        level +=1;
        PlayerPrefs.SetInt("Level", level);

        int coin = PlayerPrefs.GetInt("Coin", 1);
        coin += coins;
        PlayerPrefs.SetInt("Coin", coin);

        loading.SetActive(true);

        SceneManager.LoadScene(1);
    }

    public void GameCompleted()
    {
        coinsText.text = coins.ToString();
        PauseMenu.SetActive(false);
        GameCompleteMenu.SetActive(true);
        GameOverMenu.SetActive(false);

        if(!callAdGameCompleted)
        {
            PlayerPrefs.SetInt("Video ad", 4);
           // GameObject.Find("Ad Control").GetComponent<AdControl>().ShowRewardedVideoAd();
            callAdGameCompleted = true;
        }

        
    }

    public void GameOver()
    {
        PauseMenu.SetActive(false);
        GameCompleteMenu.SetActive(false);
        GameOverMenu.SetActive(true);

        if(!callAdGameOver)
        {
            PlayerPrefs.SetInt("Video ad", 4);
           // GameObject.Find("Ad Control").GetComponent<AdControl>().ShowRewardedVideoAd();
            callAdGameOver = true;
        }
    }

    public void GamePaused()
    {
        gamePaused = true;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        GameCompleteMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    public void ContinueGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        GameCompleteMenu.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        loading.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        loading.SetActive(true);
        SceneManager.LoadScene(0);
    }

    public void Close()
    {
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        GameCompleteMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void DoubleRewards()
    {
        PlayerPrefs.SetInt("Video ad", 2);
       // GameObject.Find("Ad Control").GetComponent<AdControl>().ShowRewardedVideoAd();
    }

    public void SkipLevel()
    {
        PlayerPrefs.SetInt("Video ad", 1);
       // GameObject.Find("Ad Control").GetComponent<AdControl>().ShowRewardedVideoAd();
    }

}
