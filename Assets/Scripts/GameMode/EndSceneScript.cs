using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using TMPro;

public class EndSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    private string outputText;
    public TMP_Text EndText;
    public IMixedRealitySceneSystem sceneSystem;
    public GameObject ScenePopulationManager;
    void Start()
    {
        //FeedbackScript = FeedbackPlane.GetComponent<present_at_correct_location>();
        
        int Score = timer.EndScore;
        outputText = string.Format("Your endscore is {0000} (: \n" +
            "Thanks for playing !!", Score);
        EndText.text = outputText;
        sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        ScenePopulationManager.SetActive(false); // sets game scene population inactive
    }

    public void LoadMenu()
    {

        //load menu
        //GameObject.Find("MainMenu").SetActive(true);
        //sceneSystem.UnloadContent("EndScoreScene");
        SceneManager.LoadScene("BaseConfig", LoadSceneMode.Single);   

    }
    public void LoadGame()
    {
        sceneSystem.LoadContent("GameScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
