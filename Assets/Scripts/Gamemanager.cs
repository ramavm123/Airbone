using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    static Gamemanager instance;

    

    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private GameObject WinScreenPrefab;
    [SerializeField] private List<CheckpointLogic> CheckpointSpawn;
    [SerializeField] private List<int> CollectedKeys;

     private TextMeshProUGUI KeyCounter;

    [SerializeField] private int savedCheckpointIndex = 0;
    //[SerializeField] private int playerMaxLives = 3;
    //private int playerLives = 0;
    private int playerKeys = 0;
    private int playerDeaths = 0;
    [HideInInspector] public int playerMaxKeys = 0;
    private GameObject cameraFollower;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
    {
        

        GameObject[] CheckpointsGO = GameObject.FindGameObjectsWithTag("Checkpoint");
        GameObject player = null;
        for (int i = 0; i < CheckpointsGO.Length; ++i)
        {
            if (CheckpointsGO[i].GetComponent<AutoSetIndex>().index == savedCheckpointIndex)
            {

                player = Instantiate(PlayerPrefab, CheckpointsGO[i].transform.position, PlayerPrefab.transform.rotation);

                CheckpointsGO[i].GetComponent<CheckpointLogic>().OnSetup();
                
            }
        }


        GameObject[] KeysGO = GameObject.FindGameObjectsWithTag("Key");
        for (int i = 0; i < KeysGO.Length; ++i)
        {
            
            if (CollectedKeys.Contains(KeysGO[i].GetComponent<AutoSetIndex>().index))
            {
                KeysGO[i].SetActive(false);
            }
        }
        playerMaxKeys = KeysGO.Length;
        KeyCounter = GameObject.FindGameObjectWithTag("UIText").GetComponent<TextMeshProUGUI>();
        KeyCounter.text = new string(playerKeys + "/" + "5");
        Debug.Log("Keys Amount:" + playerMaxKeys);
        cameraFollower = GameObject.FindGameObjectWithTag("CameraPoint");

        cameraFollower.GetComponent<PlayerFollower>().SetCamera(player);

        if (playerKeys == playerMaxKeys)
        {
            GameObject.FindGameObjectWithTag("Exit").GetComponent<ExitCode>().OnOpen();
        }

    }




    public void CheckpointSet(int index)
    {
        savedCheckpointIndex = index;
        GameObject[] CheckpointsGO = GameObject.FindGameObjectsWithTag("Checkpoint");
        for (int i = 0; i < CheckpointsGO.Length; ++i)
        {
            if (CheckpointsGO[i].GetComponent<AutoSetIndex>().index != index)
            {
                Debug.Log(i);
                CheckpointsGO[i].GetComponent<CheckpointLogic>().OnReset();
            }
        }
    }


    public void OnPlayerDamage()
    {
        playerDeaths += 1;
        SceneManager.LoadSceneAsync("LevelScene");

    }
    public void OnPlayerKeyCollection(int keyIndex)
    {
        if (!CollectedKeys.Contains(keyIndex))
        {
            CollectedKeys.Add(keyIndex);
        }
        playerKeys += 1;
        KeyCounter.text = new string(playerKeys+"/"+"5");
        if (playerKeys == playerMaxKeys)
        {
            GameObject.FindGameObjectWithTag("Exit").GetComponent<ExitCode>().OnOpen();
        }
    }

    public void OnRespawn()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().CanMove = false;
        StartCoroutine(Respawn());
    }

    public void OnExit()
    {
        Instantiate(WinScreenPrefab);
        WinScreenPrefab.GetComponent<WinscreenManager>().SetUI(playerDeaths);
    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        playerDeaths = 0;
        CollectedKeys = new List<int> { 0 };
        savedCheckpointIndex = 0;
        playerKeys = 0;
        SceneManager.LoadSceneAsync("LevelScene",LoadSceneMode.Single);
    }
}
