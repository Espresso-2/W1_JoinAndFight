using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI coins;

    public GameObject notEnoughCoins;

    public GameObject squadLevel2GameObject;
    public GameObject squadLevel3GameObject;
    public GameObject squadLevel4GameObject;
    public GameObject squadLevel5GameObject;

    public GameObject healthLevel2GameObject;
    public GameObject healthLevel3GameObject;
    public GameObject healthLevel4GameObject;
    public GameObject healthLevel5GameObject;

    public GameObject settingsMenu;

    public Toggle soundToggleButton;
    public AudioSource backgroundMusic;

    public GameObject loading;
    // Start is called before the first frame update
    void Start()
    {
        loading.SetActive(false);
        if(PlayerPrefs.GetInt("Health", 1) == 1)
        {
            healthLevel2GameObject.SetActive(true);
            healthLevel3GameObject.SetActive(false);
            healthLevel4GameObject.SetActive(false);
            healthLevel5GameObject.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("Health", 1) == 2)
        {
            healthLevel2GameObject.SetActive(false);
            healthLevel3GameObject.SetActive(true);
            healthLevel4GameObject.SetActive(false);
            healthLevel5GameObject.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("Health", 1) == 3)
        {
            healthLevel2GameObject.SetActive(false);
            healthLevel3GameObject.SetActive(false);
            healthLevel4GameObject.SetActive(true);
            healthLevel5GameObject.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("Health", 1) == 4)
        {
            healthLevel2GameObject.SetActive(false);
            healthLevel3GameObject.SetActive(false);
            healthLevel4GameObject.SetActive(false);
            healthLevel5GameObject.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("Health", 1) == 5)
        {
            healthLevel2GameObject.SetActive(false);
            healthLevel3GameObject.SetActive(false);
            healthLevel4GameObject.SetActive(false);
            healthLevel5GameObject.SetActive(false);
        }


        if(PlayerPrefs.GetInt("Squad", 1) == 1)
        {
            squadLevel2GameObject.SetActive(true);
            squadLevel3GameObject.SetActive(false);
            squadLevel4GameObject.SetActive(false);
            squadLevel5GameObject.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("Squad", 1) == 2)
        {
            squadLevel2GameObject.SetActive(false);
            squadLevel3GameObject.SetActive(true);
            squadLevel4GameObject.SetActive(false);
            squadLevel5GameObject.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("Squad", 1) == 3)
        {
            squadLevel2GameObject.SetActive(false);
            squadLevel3GameObject.SetActive(false);
            squadLevel4GameObject.SetActive(true);
            squadLevel5GameObject.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("Squad", 1) == 4)
        {
            squadLevel2GameObject.SetActive(false);
            squadLevel3GameObject.SetActive(false);
            squadLevel4GameObject.SetActive(false);
            squadLevel5GameObject.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("Squad", 1) == 5)
        {
            squadLevel2GameObject.SetActive(false);
            squadLevel3GameObject.SetActive(false);
            squadLevel4GameObject.SetActive(false);
            squadLevel5GameObject.SetActive(false);
        }

        // for(int i=1; i<)
    }

    // Update is called once per frame
    void Update()
    {
        coins.text = PlayerPrefs.GetInt("Coin", 0).ToString();
        if(PlayerPrefs.GetInt("Sound", 1)==1)
        {
            backgroundMusic.mute = false;
        }
        else
        {
            backgroundMusic.mute = true;
        }

    }

    public void Play()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void SquadLevel2()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        if(coin > 100)
        {
            PlayerPrefs.SetInt("Squad", 2);
            coin -= 100;
            PlayerPrefs.SetInt("Coin", coin);
            squadLevel2GameObject.SetActive(false);
            squadLevel3GameObject.SetActive(true);
            squadLevel4GameObject.SetActive(false);
            squadLevel5GameObject.SetActive(false);
        }
        else
        {
            notEnoughCoins.SetActive(true);
        }
    }

    public void HealthLevel2()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        if(coin > 100)
        {
            PlayerPrefs.SetInt("Health", 2);
            coin -= 100;
            PlayerPrefs.SetInt("Coin", coin);
            healthLevel2GameObject.SetActive(false);
            healthLevel3GameObject.SetActive(true);
            healthLevel4GameObject.SetActive(false);
            healthLevel5GameObject.SetActive(false);
        }
        else
        {
            notEnoughCoins.SetActive(true);
        }
    }

    public void SquadLevel3()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        if(coin > 600)
        {
            PlayerPrefs.SetInt("Squad", 3);
            coin -= 600;
            PlayerPrefs.SetInt("Coin", coin);
            squadLevel2GameObject.SetActive(false);
            squadLevel3GameObject.SetActive(false);
            squadLevel4GameObject.SetActive(true);
            squadLevel5GameObject.SetActive(false);
        }
        else
        {
            notEnoughCoins.SetActive(true);
        }
    }

    public void HealthLevel3()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        if(coin > 600)
        {
            PlayerPrefs.SetInt("Health", 3);
            coin -= 600;
            PlayerPrefs.SetInt("Coin", coin);
            healthLevel2GameObject.SetActive(false);
            healthLevel3GameObject.SetActive(false);
            healthLevel4GameObject.SetActive(true);
            healthLevel5GameObject.SetActive(false);
        }
        else
        {
            notEnoughCoins.SetActive(true);
        }
    }

    public void SquadLevel4()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        if(coin > 800)
        {
            PlayerPrefs.SetInt("Squad", 4);
            coin -= 800;
            PlayerPrefs.SetInt("Coin", coin);
            squadLevel2GameObject.SetActive(false);
            squadLevel3GameObject.SetActive(false);
            squadLevel4GameObject.SetActive(false);
            squadLevel5GameObject.SetActive(true);
        }
        else
        {
            notEnoughCoins.SetActive(true);
        }
    }

    public void HealthLevel4()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        if(coin > 800)
        {
            PlayerPrefs.SetInt("Health", 4);
            coin -= 800;
            PlayerPrefs.SetInt("Coin", coin);
            healthLevel2GameObject.SetActive(false);
            healthLevel3GameObject.SetActive(false);
            healthLevel4GameObject.SetActive(false);
            healthLevel5GameObject.SetActive(true);
        }
        else
        {
            notEnoughCoins.SetActive(true);
        }
    }

    public void SquadLevel5()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        if(coin > 1000)
        {
            PlayerPrefs.SetInt("Squad", 5);
            coin -= 1000;
            PlayerPrefs.SetInt("Coin", coin);
            squadLevel2GameObject.SetActive(false);
            squadLevel3GameObject.SetActive(false);
            squadLevel4GameObject.SetActive(false);
            squadLevel5GameObject.SetActive(false);
        }
        else
        {
            notEnoughCoins.SetActive(true);
        }
    }

    public void HealthLevel5()
    {
        int coin = PlayerPrefs.GetInt("Coin", 0);
        if(coin > 1000)
        {
            PlayerPrefs.SetInt("Health", 5);
            coin -= 1000;
            PlayerPrefs.SetInt("Coin", coin);
            healthLevel2GameObject.SetActive(false);
            healthLevel3GameObject.SetActive(false);
            healthLevel4GameObject.SetActive(false);
            healthLevel5GameObject.SetActive(false);
        }
        else
        {
            notEnoughCoins.SetActive(true);
        }
    }

    public void CloseNotEnoughCoins()
    {
        notEnoughCoins.SetActive(false);
    }

    public void OpeCoinShop()
    {
        notEnoughCoins.SetActive(true);
    }

    public void CoinAds()
    {
        PlayerPrefs.SetInt("Video ad", 3);
       // GameObject.Find("Ad Control").GetComponent<AdControl>().ShowRewardedVideoAd();
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
    }

    public void Close()
    {
        settingsMenu.SetActive(false);
    }

    public void SoundToggle()
    {
        if(PlayerPrefs.GetInt("Sound", 0) == 0)
        {
            PlayerPrefs.SetInt("Sound", 1);
            soundToggleButton.isOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
            soundToggleButton.isOn = false;
        }
    }
}
