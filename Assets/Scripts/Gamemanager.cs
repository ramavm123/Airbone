using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    static Gamemanager instance;

    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private List<CheckpointLogic> CheckpointSpawn;
    [SerializeField] private List<int> CollectedKeys;

    [SerializeField] private int savedCheckpointIndex = 0;
    //[SerializeField] private int playerMaxLives = 3;
    //private int playerLives = 0;
    private int playerKeys = 0;
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
        playerMaxKeys = KeysGO.Length - 1;
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
        SceneManager.LoadSceneAsync("LevelScene");

    }
    public void OnPlayerKeyCollection(int keyIndex)
    {
        if (!CollectedKeys.Contains(keyIndex))
        {
            CollectedKeys.Add(keyIndex);
        }
        playerKeys += 1;
        if (playerKeys == playerMaxKeys)
        {
            GameObject.FindGameObjectWithTag("Exit").GetComponent<ExitCode>().OnOpen();
        }
    }

    public void OnExit()
    {
        Debug.LogError("OnExitCode");
    }
}
