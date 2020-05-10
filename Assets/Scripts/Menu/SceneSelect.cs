using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;

public class SceneSelect : MonoBehaviour
{

    // loads desired scene
    public void Load(string SceneName)
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        sceneSystem.LoadContent(SceneName);
        //MonoBehaviour shaderscript = Camera.main.GetComponent("screenspacedshader") as MonoBehaviour;
        //shaderscript.enabled = true;
        //System.Threading.Thread.Sleep(5000);
        //shaderscript.enabled = false;
        return;
    }

    public void Load(string[] SceneNames)
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        sceneSystem.LoadContent(SceneNames);
        return;

    }

    // unloads desired scene
    public void Unload(string SceneName)
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        sceneSystem.UnloadContent(SceneName);
        return;

    }

    // unloads all listed scenes
    public void Unload(string[] SceneNames)
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        sceneSystem.UnloadContent(SceneNames);
        return;

    }

    // unloads all content scenes
    public void UnloadCurrentScenes()
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        Unload(sceneSystem.ContentSceneNames);
    }

    public void Close()
    {
        Application.Quit();
    }
}
