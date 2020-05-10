using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using TMPro;




public class timer : MonoBehaviour
{
    // Start is called before the first frame update
    //public UnityEngine.TextMesh timertext;
    public TMP_Text timertext;
    private float timelimit;
    private int minutes,seconds,fraction;
    public GameObject FeedbackPlane;
    public present_at_correct_location FeedbackScript;
    private bool GuessTime;
    public static int EndScore = 0;
    public GameObject EndScreen;
    public IMixedRealitySceneSystem sceneSystem;

    void Start()
    {
        timelimit = 90.0f; // 60 seconds.
        minutes = (int)(timelimit / 60f);
        seconds = (int)(timelimit % 60f);
        fraction = (int)((timelimit * 100f) % 100f);

        timertext.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        timertext.color = Color.white;

        FeedbackScript = FeedbackPlane.GetComponent<present_at_correct_location>();

        sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        GuessTime = FeedbackScript.GuessTime;
        if (GuessTime)
            timelimit -= Time.deltaTime; 
       
        
        minutes = (int)(timelimit / 60f);
        seconds = (int)(timelimit % 60f);
        fraction = (int)((timelimit * 100f) % 100f);
        timertext.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);

        if (timelimit < 10)
            timertext.color = Color.red;
            
        if (timelimit <= 0)
        {
            this.enabled = false;
            this.Stop();
            //SceneManager.LoadScene("EndScoreScene");
        }
            
            
        //Should be replaced with game stop when it is implemented
    }

    void Stop()
    {
        //int EndScore = FeedbackScript.SCORE;
        //add the event handler and then also add the variables for panel and text and 
        //check if setactive really works(;
        EndScore = FeedbackScript.SCORE;
        EndScreen.SetActive(true);
        //await sceneSystem.LoadContent("EndScoreScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        //show the panel and then show the score
    }

  

    void Reset()
    {
        timelimit = 25f;
        timertext.color = Color.white;
    }
}
