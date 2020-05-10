using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class screenspacedshader : MonoBehaviour
{
    public GameObject baseconfigcam;
    public Material _material;
    public static int box_numbers = 3;
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        _material.SetFloat("box_numbers", box_numbers);
        Graphics.Blit(src, dest, _material);
    }
}