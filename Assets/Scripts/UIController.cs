using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class UIController : MonoBehaviour
{

    // Start is called before the first frame update
    public static bool isInstructionPanelDismissed = false;

    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject instructionPanel;
    [SerializeField] Sprite muteImage;
    [SerializeField] Sprite playImage;
    [SerializeField] Button muteButton;

    public static Action OnDestroyGameObj;

    public delegate void StartSpawner();
    public static event StartSpawner initSpawner;


    public delegate void SoundController();
    public static event SoundController soundControl;

    //void OnEnable()
    //{
    //    CountDownTimer.timeOut += DisplayTimeOutPanel;
    //    PlayerHealth.playerDeath += DisplayGameOverPanel;
    //    PlayerHealth.updatedUITowerHealth += updateMainTowerHealth;
    //}

    void Start()
    {
        Debug.Log("UIController.DisplayTimeOutPanel: " + isInstructionPanelDismissed);

        if (isInstructionPanelDismissed)
        {
            instructionPanel.SetActive(false);
            initSpawner();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void DisplayWinPanel()
    {
        //Debug.Log("DisplayWinPanel - PlayerHealth._Instance.Health.ToString(): " + PlayerHealth._Instance.Health.ToString());
        OnDestroyGameObj();
        WinPanel.SetActive(true);
        //winScore.text = "Score: " + PlayerHealth._Instance.Health.ToString();
        //EnemySpawner.stopSpawning = false;
        //CountDownTimer.isTimerRunning = false;
    }

    public void DismissInstructionPanelPanel()
    {
        instructionPanel.SetActive(false);
        isInstructionPanelDismissed = true;
        initSpawner();
    }


    //void updateMainTowerHealth()
    //{
    //    towerHealth.text = PlayerHealth._Instance.Health.ToString();
    //}


    public void ToggleSoundImage()
    {
        Sprite image = muteButton.GetComponent<Image>().sprite;
        Debug.Log("image: "+ image.name);
        if (image.name == "Play")
        {
            soundControl?.Invoke();
            muteButton.GetComponent<Image>().sprite = muteImage ;
        }
        else
        {
            soundControl?.Invoke();
            muteButton.GetComponent<Image>().sprite = playImage;
        }
    }

    //void OnDisable()
    //{
    //    CountDownTimer.timeOut -= DisplayTimeOutPanel;
    //    PlayerHealth.playerDeath -= DisplayGameOverPanel;
    //    PlayerHealth.updatedUITowerHealth -= updateMainTowerHealth;
    //}

}
