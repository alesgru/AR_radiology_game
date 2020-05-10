using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


// put the right references in..

public class present_at_correct_location : MonoBehaviour
{   //other GameObjects
    public GameObject sliding_plane;
    public GameObject static_plane;

    //Materials
    public Material feedback_material;
    public Material TransparentMaterial;

    //variables set in other scripts
    int rand_slice;
    int selected_slice;
    private Object[] tex;
    float stepsize;
    projection_plane_texture_changer ChangerScript;
    Show_Random_Slice RandomSliceScript;
    Material sliding_plane_material;

    //variables set in this script
    private Texture2D new_texture;
    int correct_slice;
    public int SCORE;
    Renderer thisRend;
    public TMP_Text scoreText;
    public bool GuessTime;
    bool disable_confirm = false;
    Color startColor;



    // Start is called before the first frame update
    void Start()
    {
        //get variables from other scripts


        ChangerScript = sliding_plane.GetComponent<projection_plane_texture_changer>();
        RandomSliceScript = static_plane.GetComponent<Show_Random_Slice>();
        tex = ChangerScript.textures;
        stepsize = ChangerScript.stepsize;
        sliding_plane_material = sliding_plane.GetComponent<Renderer>().material;
        startColor = sliding_plane_material.color;

        scoreText.text = string.Format("Score: {0000}", SCORE);
        scoreText.color = Color.white;

        //Initialize
        Reset();
        //thisRend = this.GetComponent<Renderer>();
        //thisRend.material = TransparentMaterial;

    }

    //applies transparent material to Feedback plane and resets its position to the middle
    public void Reset()
    {
        //transform.Translate(0, transform.position.z - ChangerScript.AnatomyObject.transform.position.z, 0);
        transform.localPosition = new Vector3(0, 0, 0);

        thisRend = this.GetComponent<Renderer>();
        thisRend.material = TransparentMaterial;
        GuessTime = true;
        disable_confirm = false;
        sliding_plane_material.color = startColor;
    }

    // calculates Score and applies correct texture to Feedbackplane and places it correctly in the model
    public void Confirm()
    {
        if (disable_confirm) return;
        GuessTime = false;
        // get the correct number and the guessed number of the slice
        selected_slice = ChangerScript.slice;
        correct_slice = RandomSliceScript.rand_slice;

        //Print
        print("selected vs corrected");
        print(selected_slice);
        print(correct_slice);

        //Score
        int difference = System.Math.Abs(selected_slice - correct_slice);
        SCORE += 100 * (difference) / tex.Length;
        scoreText.text = string.Format("Score: {0000}", SCORE);

        if (ChangerScript.check_oblique)
        {
            transform.localPosition = new Vector3(0, (transform.localPosition.y - stepsize * (correct_slice - tex.Length / 2)), 0);
        }
        else
        {
            this.transform.Translate(0, -stepsize * (correct_slice - tex.Length / 2), 0);
        }

        //translate to correct position (DOUBLECHECK)
        //this.transform.Translate(0, stepsize * (correct_slice - (tex.Length / 2)), 0);

        //apply material
        //Texture2D texture = (Texture2D)tex[correct_slice];
        //feedback_material.mainTexture = texture;
        thisRend.material = feedback_material;

        // ensures that confirm button can not be pressed multiple times for same random slice
        disable_confirm = true;

        //color the sliding plane in depending on the difference
        if (difference > 300)
            sliding_plane_material.color = Color.red;
        else if (difference > 100)
            sliding_plane_material.color = Color.yellow;
        else if (difference > 0)
            sliding_plane_material.color = Color.green;

        //texture change
        //update location and texture
    }
}
