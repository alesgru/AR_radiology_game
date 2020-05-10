
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Show_Random_Slice : MonoBehaviour
{
    //external references
    public GameObject sliding_plane;
    Object[] tex;
    public Material upright_projection_plane_GAMEMODE;

    //variables of this script
    public int rand_slice;
    private Texture2D new_texture;
    projection_plane_texture_changer MyScript;

    // Start is called before the first frame update
    void Start()
    {
        MyScript = sliding_plane.GetComponent<projection_plane_texture_changer>();
        tex = MyScript.textures;
        //print(MyScript.textures.Length);
        //show_random_slice();
        //new_texture = (Texture2D)tex[rand_slice];
        //upright_projection_plane_GAMEMODE.mainTexture = new_texture;


    }
    //selects random slice and loads it as the texture of the upright plane
    public void show_random_slice()
    {
        tex = MyScript.textures;
        System.Random rand = new System.Random();
        // Generate a random index less than the size of the array.  
        if (MyScript.coronal)
        {
            rand_slice = rand.Next(150, tex.Length - 125);
        }
        else if(MyScript.sagittal)
        {
            rand_slice = rand.Next(128, tex.Length - 128);
        }
        else
        {
            rand_slice = rand.Next(0, tex.Length);
        }
        Texture2D new_texture = (Texture2D)tex[rand_slice];
        upright_projection_plane_GAMEMODE.mainTexture = new_texture;
    }

    // Update is called once per frame
    void Next()
    {
        tex = MyScript.textures;
        show_random_slice();
        //projection_plane_texture_changer MyScript = sliding_plane.GetComponent<projection_plane_texture_changer>();
        //tex = MyScript.textures;
        //print(MyScript.textures.Length);
        //new_texture = (Texture2D)tex[rand_slice];
        //upright_projection_plane_GAMEMODE.mainTexture = new_texture;
    }
}
