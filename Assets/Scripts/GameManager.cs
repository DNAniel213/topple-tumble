﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GUIManager gui = null;
    public AdManager ads = null;
    private GameDatabase gameDB = null;
    void Awake()
    {
        gameDB = GetComponent<GameDatabase>();
        gui = GetComponent<GUIManager>();
        ads = GetComponent<AdManager>();
        Global.gameManager = this;

        SceneManager.LoadScene("Splash", LoadSceneMode.Additive);
        StartCoroutine(LoadingScreen());
        
        InitializeGameScene(gameDB.seedTypes[0], gameDB.environmentTypes, gameDB.platformTypes[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadingScreen()
    {

        if(!SceneManager.GetSceneByName("GameScene").isLoaded)
            LoadGameScene();

        yield return new WaitForSeconds(3);
        SceneManager.UnloadSceneAsync("Splash");
    }

    public void  InitializeGameScene(SeedScriptableObject seedtype, EnvironmentScriptableObject[] environmentType, PlatformScriptableObject platformtype)
    {
        Global.seedtype = seedtype;
        Global.platformtype = platformtype;
        Global.environmentType = environmentType;

    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);

    }

    public void ResetScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("GameScene");
    }

    public void Die()
    {
        if(Global.seed.GetComponent<Seed>().hasDied)
            gui.ChangeGUI(11);
        else
            gui.ChangeGUI(13);
    }

    public void Revive()
    {
        gui.ChangeGUI(0);

        Global.seed.GetComponent<Seed>().Revive();
        Global.platform.GetComponent<Platform>().Revive();
    }
}

