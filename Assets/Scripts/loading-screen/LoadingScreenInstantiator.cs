using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using UnityEngine.SceneManagement;

public class LoadingScreenInstantiator : MonoBehaviour
{
    // Start is called before the first frame update
    private bool loadloadingscreen;
    private float DoomOSTvolume;
    MonoBehaviour shaderscript;

    public void Awake()
    {
        SceneManager.sceneUnloaded += LoadLoadingScreen;
        SceneManager.sceneLoaded += LoadLongLoadingScreen;
        shaderscript = Camera.main.GetComponent("screenspacedshader") as MonoBehaviour;
        loadloadingscreen = false;
    }

    public void LoadLongLoadingScreen(Scene scene,LoadSceneMode mode)
    {
        projection_plane_texture_changer.sceneName = scene.name;

        if (scene.name == "EducationalScene") DoomOSTvolume = 0.2f;
        else if (scene.name == "GameScene") DoomOSTvolume = 0.5f;
        
        shaderscript.enabled = true;
        loadloadingscreen = true;
        System.Random rnd = new System.Random();
        System.Threading.Thread t = new System.Threading.Thread(() => StopLoadingScreen(rnd.Next(3, 5) * 1000));
        screenspacedshader.box_numbers = rnd.Next(3, 6);
        t.Start();
    }

    public void LoadLoadingScreen(Scene scene)
    {
        DoomOSTvolume = 0.5f;
        shaderscript.enabled = true;
        loadloadingscreen = true;
        System.Random rnd = new System.Random();
        System.Threading.Thread t = new System.Threading.Thread(() => StopLoadingScreen(rnd.Next(1, 3) * 1000));
        screenspacedshader.box_numbers = rnd.Next(3, 6);
        t.Start();
    }

    private void StopLoadingScreen(int milliseconds)
    {
        System.Threading.Thread.Sleep(milliseconds);
        loadloadingscreen = false;
    }
    
    void Update()
    {
        if (loadloadingscreen)
        {
            doomost.DoomOST.volume = 0.5f;
            return;
        }
        shaderscript.enabled = false;
        if (!projection_plane_texture_changer.heartSource) return;
        projection_plane_texture_changer.heartSource.enabled = true;
        projection_plane_texture_changer.bladderSource.enabled = true;
        projection_plane_texture_changer.lungsSource.enabled = true;
        projection_plane_texture_changer.bowelSource.enabled = true;
        doomost.DoomOST.volume = DoomOSTvolume;
    }
}
