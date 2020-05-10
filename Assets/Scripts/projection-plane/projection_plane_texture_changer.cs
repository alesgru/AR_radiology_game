using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;

public class projection_plane_texture_changer : MonoBehaviour
{
    //public bool oblique;
    //public bool axial;
    //public bool change;
    public bool coronal;
    public bool sagittal;

    // reference to material applied to both projection planes.
    public Material projectionplanematerial;
    public GameObject AnatomyObject;
    public GameObject UprightViewProjectionPlane;
    public GameObject ProjectionPlane;
    public GameObject feedbackPlane;
    Vector3 midPosition;
    bool currentScene;
    private Vector3 originalScaleUpright;

    // object that holds all textures 
    private Object[] textures_axial, textures_sagittal, textures_coronal;
    public Object[] textures;
    private Object[] textures_oblique_up25, textures_oblique_up14, textures_oblique_down25, textures_oblique_down14;
    public int view; // maps the view to an int 0 - axial, 1 - sag, 2 - cor
    // 30 - oblUp25, 31 - oblUp14, 32 - oblDown25, 33 - oblDown14
    // holds id of current slice

    //public Button ButtonDown, ButtonUp, sag_btn;
    //cor_btn, ax_btn, oblup25_btn, oblup14_btn, obldown25_btn, obldown14_btn;
    //Sagittal.OnClick.AddListener(() => sagittal_foo());

    public int slice;
    public float stepsize;
    bool DownKeyPressed, UpKeyPressed;
    private float dicomSlices = 857;
    public bool check_oblique;
    // sonification variables

    // organ limits
    private float lungsStart, lungsEnd, lungsMin, lungsMax;
    private float heartStart, heartEnd, heartMin, heartMax;
    private float bowelStart, bowelEnd, bowelMin, bowelMax;
    private float bladderStart, bladderEnd, bladderMin, bladderMax;


    // sonification variables
    public AudioClip lungsClip, heartClip, bowelClip, bladderClip;
    public static AudioSource lungsSource, heartSource, bowelSource, bladderSource;

    public static string sceneName;

    Vector3 sizeLimits;
    float angle;
    float stepsize_x, stepsize_y, stepsize_z;

    void view_transform(GameObject gameObject, int ax, int sag, int cor)
    {

        //bool ax_group = ax_btn || oblup25_btn || oblup14_btn || obldown25_btn || obldown14_btn;

        //midPosition = GameObject.Find("Anatomy").transform.position;
        //print("b");
        //print(midPosition);
        slice = textures.Length / 2;
        print("midPosition");
        print(midPosition);
        //gameObject.transform.Translate((gameObject.transform.position.x - midPosition.x) * sag, (gameObject.transform.position.z - midPosition.z) * ax, (gameObject.transform.position.y - midPosition.y) * cor);
        //gameObject.transform.position = new Vector3 (midPosition.z * sag,  midPosition.z * ax,  midPosition.z * cor);
        //gameObject.transform.localPosition = new Vector3(midPosition.z * sag, midPosition.z * ax, midPosition.z * cor);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        //gameObject.transform.position= new Vector3(midPosition.x, midPosition.y, midPosition.z);
        print(gameObject.transform.position);
        stepsize_x = sizeLimits.x / textures.Length;
        stepsize_y = sizeLimits.y / textures.Length;
        //stepsize_z = 7 / textures.Length;
        //print("other");
        //print(stepsize_z);
        //stepsize_z = (sizeLimits.z *19.8f) / textures.Length;
        //print(stepsize_z);
        //print("everything");
        //print(textures.Length);
        //print(sizeLimits.z);
        stepsize_z = sizeLimits.z / textures.Length;
        //print(stepsize_z);
        //
        //print("limits");
        //print(sizeLimits.z);
        //print("stepsize");
        //print(stepsize_z);

    }

    void sag_foo()
    {
        sagittal = true;
        coronal = false;
        check_oblique = false;
        textures = textures_sagittal;
        slice = textures.Length / 2;
        // get a specific slice and apply as texture
        Texture2D texture = (Texture2D)textures[slice];
        projectionplanematerial.mainTexture = texture;

        Vector3 rotVector = new Vector3(90, 180, 45);
        Vector3 scaleVector_upright = new Vector3(0.64f, 1f, 1.12f);
        Vector3 scaleVector = new Vector3(0.4f, 0.5f, 0.7f);
        Vector3 angles = new Vector3(90, 0, -90);
        Vector3 position_upright = new Vector3(0.5f, 0.2f, 1.5f);
        Vector3 mini_translation = new Vector3(0, -0.2f, 0);
        setView(angles, scaleVector, scaleVector_upright, rotVector, position_upright, mini_translation, 0, 1, 0);
        //feedbackPlane.transform.localPosition= new Vector3(0,-0.2f,0);


        stepsize = stepsize_x;

        //print(GameObject.Find("Anatomy").transform.position);
        /*midPosition = GameObject.Find("Anatomy").transform.position;
        transform.Translate(transform.position.x - midPosition.x, 0, 0);
        stepsize = sizeLimits.x / textures.Length;*/
    }

    void cor_foo()
    {
        textures = textures_coronal;
        check_oblique = false;
        sagittal = false;
        coronal = true;

        Vector3 rotVector = new Vector3(0, -60, 0);
        Vector3 scaleVector_upright = new Vector3(0.8f, 1f, 1.4f);
        Vector3 scaleVector = new Vector3(0.4f, 0.5f, 0.7f);
        Vector3 angles = new Vector3(90, 270, 90);
        Vector3 position_upright = new Vector3(0.5f, 0.2f, 1.5f);
        Vector3 mini_translation = new Vector3(0, -0.2f, 0);
        setView(angles, scaleVector, scaleVector_upright, rotVector, position_upright, mini_translation, 0, 0, 1);
        stepsize = stepsize_y;
        /*midPosition = GameObject.Find("Anatomy").transform.position;
        transform.Translate(0, 0, transform.position.y - midPosition.y);
        stepsize = sizeLimits.y / textures.Length;*/
    }

    void ax_foo()
    {
        check_oblique = false;
        sagittal = false;
        coronal = false;
        textures = textures_axial;
        //Vector3 rotVector = new Vector3(90, 180, 45);
        Vector3 rotVector = new Vector3(90, 180, 225);

        Vector3 scaleVector_upright = new Vector3(1f, 1f, 1f);
        Vector3 scaleVector = new Vector3(0.43f, 0.43f, 0.43f);
        Vector3 angles = new Vector3(0, 0, 180);
        Vector3 position_upright = new Vector3(0.5f, 0.3f, 1.5f);
        Vector3 mini_translation = new Vector3(0, 0, -0.2f);
        setView(angles, scaleVector, scaleVector_upright, rotVector, position_upright, mini_translation, 1, 0, 0);
        stepsize = stepsize_z;
        /*if (currentScene.name == "EducationalScene") {
            sonification(ref lungsSource, ref lungsClip, lungsStart, lungsEnd, lungsMin, lungsMax);
            sonification(ref heartSource, ref heartClip, heartStart, heartEnd, heartMin, heartMax);
            sonification(ref bowelSource, ref bowelClip, bowelStart, bowelEnd, bowelMin, bowelMax);
            sonification(ref bladderSource, ref bladderClip, bladderStart, bladderEnd, bladderMin, bladderMax);

        }*/

        /*midPosition = GameObject.Find("Anatomy").transform.position;
        transform.Translate(0, transform.position.z - midPosition.z, 0);
        stepsize = sizeLimits.z / textures.Length;*/

    }

    void oblup25_foo()
    {
        sagittal = false;
        coronal = false;
        check_oblique = true;
        textures = textures_oblique_up25;
        Vector3 rotVector = new Vector3(90, 180, 225);
        Vector3 scaleVector_upright = new Vector3(1f, 1f, 1.12f);
        //Vector3 scaleVector = new Vector3(0.5f, 0.5f, 0.56f);
        Vector3 scaleVector = new Vector3(0.43f, 0.43f, 0.48f);
        angle = 26.56f;
        Vector3 angles = new Vector3(angle, 0, 180);
        Vector3 position_upright = new Vector3(0.5f, 0.3f, 1.5f);
        Vector3 mini_translation = new Vector3(0, 0, 0);
        setView(angles, scaleVector, scaleVector_upright, rotVector, position_upright, mini_translation, 1, 0, 0);
        print("stepsize");
        print(stepsize_z);
        stepsize = stepsize_z * 19.8f;
        print(stepsize);
        /*midPosition = GameObject.Find("Anatomy").transform.position;
        transform.Translate(0, transform.position.z - midPosition.z, 0);
        stepsize = sizeLimits.z / textures.Length;*/
    }

    void oblup14_foo()
    {
        sagittal = false;
        coronal = false;
        check_oblique = true;
        textures = textures_oblique_up14;
        Vector3 rotVector = new Vector3(90, 180, 225);
        Vector3 scaleVector_upright = new Vector3(1f, 1f, 1.03f);
        //Vector3 scaleVector = new Vector3(0.5f, 0.5f, 0.515f);
        Vector3 scaleVector = new Vector3(0.43f, 0.43f, 0.44f);
        angle = 14.03f;
        Vector3 angles = new Vector3(angle, 0, 180);
        Vector3 position_upright = new Vector3(0.5f, 0.3f, 1.5f);
        Vector3 mini_translation = new Vector3(0, 0, 0);
        setView(angles, scaleVector, scaleVector_upright, rotVector, position_upright, mini_translation, 1, 0, 0);

        stepsize = stepsize_z * 19.8f;
        /*midPosition = GameObject.Find("Anatomy").transform.position;
        transform.Translate(0, transform.position.z - midPosition.z, 0);
        stepsize = sizeLimits.z / textures.Length;*/
    }

    void obldown25_foo()
    {
        sagittal = false;
        coronal = false;
        check_oblique = true;
        textures = textures_oblique_down25;
        print("length");
        print(textures.Length);
        Vector3 rotVector = new Vector3(90, 180, 225);
        Vector3 scaleVector_upright = new Vector3(1f, 1f, 1.12f);
        Vector3 scaleVector = new Vector3(0.43f, 0.43f, 0.48f);
        angle = -26.56f;
        Vector3 angles = new Vector3(angle, 0, 180);
        Vector3 position_upright = new Vector3(0.5f, 0.3f, 1.5f);
        Vector3 mini_translation = new Vector3(0, 0, 0);
        setView(angles, scaleVector, scaleVector_upright, rotVector, position_upright, mini_translation, 1, 0, 0);

        stepsize = stepsize_z * 19.8f;
        /*midPosition = GameObject.Find("Anatomy").transform.position;
        transform.Translate(0, transform.position.z - midPosition.z, 0);
        stepsize = sizeLimits.z / textures.Length;*/
    }

    void obldown14_foo()
    {
        sagittal = false;
        coronal = false;
        check_oblique = true;
        textures = textures_oblique_down14;
        Vector3 rotVector = new Vector3(90, 180, 225);
        Vector3 scaleVector_upright = new Vector3(1f, 1f, 1.03f);
        Vector3 scaleVector = new Vector3(0.43f, 0.43f, 0.44f);
        angle = -14.03f;
        Vector3 angles = new Vector3(angle, 0, 180);
        Vector3 position_upright = new Vector3(0.5f, 0.3f, 1.5f);
        Vector3 mini_translation = new Vector3(0, 0, 0);
        setView(angles, scaleVector, scaleVector_upright, rotVector, position_upright, mini_translation, 1, 0, 0);


        stepsize = stepsize_z * 19.8f;
        /*midPosition = GameObject.Find("Anatomy").transform.position;
        transform.Translate(0, transform.position.z - midPosition.z, 0);
        stepsize = sizeLimits.z / textures.Length;*/
    }

    public void OnDownButtonPressed()
    {
        DownKeyPressed = true;
    }

    public void OnDownButtonRelease()
    {
        DownKeyPressed = false;
    }

    public void OnUpButtonPressed()
    {
        UpKeyPressed = true;
    }

    public void OnUpButtonReleased()
    {
        UpKeyPressed = false;
    }

    private void Awake()
    {
        // Load the images according to the views
        textures_axial = Resources.LoadAll<Texture>("axial/abdomen/");
        textures_sagittal = Resources.LoadAll<Texture>("sagittal/abdomen/");
        textures_coronal = Resources.LoadAll<Texture>("coronal/abdomen/");
        textures_oblique_up25 = Resources.LoadAll<Texture>("oblique/25grad/up/");
        textures_oblique_down25 = Resources.LoadAll<Texture>("oblique/25grad/down/");
        textures_oblique_up14 = Resources.LoadAll<Texture>("oblique/14grad/up/");
        textures_oblique_down14 = Resources.LoadAll<Texture>("oblique/14grad/down/");
        System.Array.Reverse(textures_axial, 0, textures_axial.Length);
        System.Array.Reverse(textures_sagittal, 0, textures_sagittal.Length);
        //System.Array.Reverse(textures_oblique_up25, 0, textures_oblique_up25.Length);
        //System.Array.Reverse(textures_oblique_down25, 0, textures_oblique_down25.Length);
        //System.Array.Reverse(textures_oblique_up14, 0, textures_oblique_up14.Length);
        //System.Array.Reverse(textures_oblique_down14, 0, textures_oblique_down14.Length);

    }

    private void setView(Vector3 angles, Vector3 scaleVector_moving, Vector3 scaleVector_upright, Vector3 rotVector, Vector3 position_upright, Vector3 mini_translation, int ax, int sag, int cor)
    {
        // rotate, translate, scale the projection plane
        ProjectionPlane.transform.localRotation = Quaternion.Euler(angles);

        //GameObject.Find("Anatomy").transform.position = new Vector3(0,0,1.5f);

        view_transform(ProjectionPlane, ax, sag, cor);
        ProjectionPlane.transform.localScale = scaleVector_moving;
        ProjectionPlane.transform.localPosition = mini_translation;
        if (currentScene == true)
        {
            feedbackPlane.transform.localRotation = Quaternion.Euler(angles);
            feedbackPlane.transform.localScale = scaleVector_moving;
            feedbackPlane.transform.localPosition = mini_translation;
            //view_transform(feedbackPlane, ax, sag, cor);
            Show_Random_Slice show_Random_Slice = UprightViewProjectionPlane.GetComponent<Show_Random_Slice>();
            show_Random_Slice.show_random_slice();

        }

        // rotate and translate the whole anatomy
        GameObject.Find("Anatomy").transform.rotation = Quaternion.Euler(rotVector);
        //GameObject.Find("Anatomy").transform.localScale = new Vector3(0.07f,0.07f,0.07f);
        //GameObject.Find("Anatomy").transform.position = new Vector3(0, 0, 1.5f);
        //GameObject.Find("UprightViewProjectionPlane").transform.localPosition = new Vector3(0, 0.5f, 1.5f);
        //GameObject.Find("UprightViewProjectionPlane").transform.localPosition = position_upright;
        GameObject.Find("UprightViewProjectionPlane").transform.rotation = Quaternion.Euler(new Vector3(90, 180, 0));
        GameObject.Find("UprightViewProjectionPlane").transform.localScale = Vector3.Scale(originalScaleUpright, scaleVector_upright);
    }

    void Start()
    {
        slice = textures.Length / 2;

        // view = 1;
        IMixedRealitySceneSystem check = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        sizeLimits = AnatomyObject.GetComponent<BoxCollider>().bounds.size;
        midPosition = AnatomyObject.transform.position;
        currentScene = check.IsContentLoaded("GameScene");
        originalScaleUpright = GameObject.Find("UprightViewProjectionPlane").transform.localScale;

        ax_foo();
        // get a specific slice and apply as texture
        Texture2D texture = (Texture2D)textures[slice];
        projectionplanematerial.mainTexture = texture;

        DownKeyPressed = false;
        UpKeyPressed = false;

        if (sceneName != "EducationalScene") return;

        AudioSource[] audios = GetComponents<AudioSource>();
        lungsSource = audios[0];
        heartSource = audios[1];
        bowelSource = audios[2];
        bladderSource = audios[3];

        heartSource.enabled = false;
        bladderSource.enabled = false;
        lungsSource.enabled = false;
        bowelSource.enabled = false;

        // define organ limits
        // Lungs (20 - 340) Heart(170 - 290) Bowel(350 - 700) Bladder(695 - 775)
        lungsStart = textures.Length - textures.Length * (20 / dicomSlices);
        lungsEnd = textures.Length - textures.Length * (340 / dicomSlices);
        lungsMin = (float)(lungsStart + 0.3 * (lungsEnd - lungsStart));
        lungsMax = (float)(lungsMin + 0.4 * (lungsEnd - lungsStart));

        heartStart = textures.Length - textures.Length * (170 / dicomSlices);
        heartEnd = textures.Length - textures.Length * (290 / dicomSlices);
        heartMin = (float)(heartStart + 0.3 * (heartEnd - heartStart));
        heartMax = (float)(heartMin + 0.4 * (heartEnd - heartStart));

        bowelStart = textures.Length - textures.Length * (350 / dicomSlices);
        bowelEnd = textures.Length - textures.Length * (700 / dicomSlices);
        bowelMin = (float)(bowelStart + 0.3 * (bowelEnd - bowelStart));
        bowelMax = (float)(bowelMin + 0.4 * (bowelEnd - bowelStart));

        bladderStart = textures.Length - textures.Length * (695 / dicomSlices);
        bladderEnd = textures.Length - textures.Length * (775 / dicomSlices);
        bladderMin = (float)(bladderStart + 0.3 * (bladderEnd - bladderStart));
        bladderMax = (float)(bladderMin + 0.4 * (bladderEnd - bladderStart));

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow) || UpKeyPressed)
        {
            
            if (slice == textures.Length - 1)
            {
                print("start");
                print(transform.localPosition);
                return;
            }
            // change slice ID -> each step 20 slices up
            slice++;
            // movement of the plane 
            if (check_oblique)
            {
                //this.transform.Translate(-stepsize, 0, 0, Space.World);

                //transform.localPosition= new Vector3(0, (transform.localPosition.y - stepsize), 0);
                transform.localPosition = new Vector3(0, (transform.localPosition.y - stepsize), 0);
            }
            else
            {
                this.transform.Translate(0, -stepsize, 0, Space.Self);
            }
            //this.transform.Translate(0, -stepsize, 0, Space.Self);
            //this.transform.Translate(0, -stepsize, 0);
            print(slice);


        }
        if (Input.GetKey(KeyCode.DownArrow) || DownKeyPressed)
        {
            
            if (slice == 0)
            {
                print("end");
                print(transform.localPosition);
                return;
            }
            slice--;
            if (check_oblique)
            {
                //this.transform.Translate(+stepsize, 0, 0, Space.World);
                transform.localPosition = new Vector3(0, (transform.localPosition.y + stepsize), 0);
            }
            else
            {
                this.transform.Translate(0, +stepsize, 0, Space.Self);
            }
            //movement of the plane
            //this.transform.Translate(+stepsize, 0, 0, Space.World);
            //this.transform.Translate(0, +stepsize, 0, Space.Self);
            //this.transform.Translate(0, +stepsize, 0);
        }
        //sizeLimits = AnatomyObject.GetComponent<BoxCollider>().bounds.size;
        //Vector3 midPosition = AnatomyObject.transform.position;



      
        Texture texture = (Texture2D)textures[slice];
        projectionplanematerial.mainTexture = texture;


        // SONIFICATION
        /*if (view==0)
        {
            sonification(ref lungsSource, ref lungsClip, lungsStart, lungsEnd, lungsMin, lungsMax);
            sonification(ref heartSource, ref heartClip, heartStart, heartEnd, heartMin, heartMax);
            sonification(ref bowelSource, ref bowelClip, bowelStart, bowelEnd, bowelMin, bowelMax);
            sonification(ref bladderSource, ref bladderClip, bladderStart, bladderEnd, bladderMin, bladderMax);
        }*/


        projectionplanematerial.mainTexture = texture;

        if (sceneName != "EducationalScene") return;

        sonification(ref lungsSource, ref lungsClip, lungsStart, lungsEnd, lungsMin, lungsMax);
        sonification(ref heartSource, ref heartClip, heartStart, heartEnd, heartMin, heartMax);
        sonification(ref bowelSource, ref bowelClip, bowelStart, bowelEnd, bowelMin, bowelMax);
        sonification(ref bladderSource, ref bladderClip, bladderStart, bladderEnd, bladderMin, bladderMax);
    }

    void sonification(ref AudioSource orgSource, ref AudioClip orgClip, float orgStart, float orgEnd, float orgMin, float orgMax)
    {
        if (slice > orgStart & slice < orgEnd)
        {
            // map position with volume
            if (slice > orgStart & slice < orgMin)
            {
                orgSource.volume = (slice - orgStart) / (orgMin - orgStart);
            }

            if (slice > orgMin & slice < orgMax)
            {
                orgSource.volume = 1;
            }

            if (slice > orgMax & slice < orgEnd)
            {
                orgSource.volume = 1 - (slice - orgMax) / (orgEnd - orgMax);
            }

            if (!orgSource.isPlaying)
            {
                orgSource.clip = orgClip;
                orgSource.loop = true;
                orgSource.Play();
            }

        }
        else
        {
            if (orgSource.isPlaying)
            {
                orgSource.loop = false;
                orgSource.Stop();
            }

        }
    }
}
    