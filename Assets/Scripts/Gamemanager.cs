using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    static Gamemanager instance;

    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private List<CheckpointLogic> CheckpointSpawn;

    [SerializeField] private int savedCheckpointIndex = 0;
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
            if (CheckpointsGO[i].GetComponent<CheckpointLogic>().index == savedCheckpointIndex)
            {
                Debug.Log(savedCheckpointIndex);
                player = Instantiate(PlayerPrefab, CheckpointsGO[i].transform.position, PlayerPrefab.transform.rotation);
            }
        }
        
        cameraFollower = GameObject.FindGameObjectWithTag("CameraPoint");
        
        cameraFollower.GetComponent<PlayerFollower>().SetCamera(player);
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void CheckpointSet(int index)
    {
        savedCheckpointIndex = index;

    }
    public int OnAddCheckpoint(CheckpointLogic NewCheckpoint)
    {
        if (!CheckpointSpawn.Contains(NewCheckpoint))
        {
            CheckpointSpawn.Add(NewCheckpoint);
            return CheckpointSpawn.Count;
        }
        return CheckpointSpawn.IndexOf(NewCheckpoint);
    }


    public void OnPlayerDeath()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    

}
