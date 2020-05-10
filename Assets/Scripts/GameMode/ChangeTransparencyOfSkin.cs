using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTransparencyOfSkin : MonoBehaviour
{
    public Button SkinButton;
    public Text ButtonText;
    bool SkinAdded;
    Renderer thisRend;
    public Material GreyMaterial;
    public Material TransparentMaterial;
    // Start is called before the first frame update
    void Start()
    {
        ButtonText.text = "add skin";
        SkinButton.onClick.AddListener(ChangeTransparency);
        SkinAdded = false;
        thisRend = this.GetComponent<Renderer>();
        thisRend.material = TransparentMaterial;
    }
    void ChangeTransparency()
    {
        if (SkinAdded)
        {
            this.thisRend.material = TransparentMaterial;
            ButtonText.text = "add skin";
            SkinAdded = false;

        }
        else
        {
            this.thisRend.material = GreyMaterial;
            ButtonText.text = "remove skin";
            SkinAdded = true;
        }
    }

    // Update is called once per frame
   
}
