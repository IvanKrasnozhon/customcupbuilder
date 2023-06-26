using UnityEngine;

public class LogoChanger : MonoBehaviour
{
    [SerializeField] private Cup[] cups;// All cups on scene
    [SerializeField] private MainMaterialChanger mainMaterialChanger;//Main material changer(using in changeLogoSides)
    [SerializeField] private Material logoMaterial;//material for logos
    public bool isLogoOnBothSides = true;// if true = on both sides
    private bool isLogoEnabled = false;
    private Texture2D transparentTexture;

    private void Start()
    {
        transparentTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        transparentTexture.SetPixel(0, 0, new Color(0, 0, 0, 0));
        transparentTexture.Apply();
        logoMaterial.mainTexture = transparentTexture;
    }

    //loads new logo texture from JS in web page
    public void SetNewTextureFromJS(string textureDataUrl)
    {
        byte[] imageData = System.Convert.FromBase64String(textureDataUrl.Substring(textureDataUrl.IndexOf(',') + 1));
        Texture2D newTexture = new Texture2D(1, 1);
        newTexture.LoadImage(imageData);
        SetNewTexture(newTexture);
    }

    //sets new texture to logo material
    private void SetNewTexture(Texture2D newTexture)
    {
        isLogoEnabled = true;
        logoMaterial.mainTexture = newTexture;
        logoMaterial.mainTexture.wrapMode = TextureWrapMode.Clamp;
        foreach (Cup go in cups)
        {
            go.logos[0].GetComponent<Renderer>().material = logoMaterial;
            if (isLogoOnBothSides)
            {
                go.logos[1].GetComponent<Renderer>().material = logoMaterial;
            }
        }
    }

    // sets logo on one or both sides where 1 is on both sides and 0 if only on one side
    public void ChangeLogoSides(int _isOnBothSides)
    {
        if(!isLogoEnabled) return;
        isLogoOnBothSides = _isOnBothSides == 1 ? true : false;
        switch (isLogoOnBothSides)
        {
            case true:
                foreach (Cup go in cups)
                {
                    go.logos[1].GetComponent<Renderer>().material.mainTexture = logoMaterial.mainTexture;
                }
                break;
            case false:
                foreach (Cup go in cups)
                {
                    go.logos[1].GetComponent<Renderer>().material.mainTexture = transparentTexture;
                }
                break;
        }

    }

    public void ChangeLogoSides(bool _isOnBothSides)
    {
        if(!isLogoEnabled) return;
        isLogoOnBothSides = _isOnBothSides;
        switch (isLogoOnBothSides)
        {
            case true:
                foreach (Cup go in cups)
                {
                    go.logos[0].GetComponent<Renderer>().material.mainTexture = logoMaterial.mainTexture;
                    go.logos[1].GetComponent<Renderer>().material.mainTexture = logoMaterial.mainTexture;
                }
                break;
            case false:
                foreach (Cup go in cups)
                {
                    go.logos[1].GetComponent<Renderer>().material.mainTexture = logoMaterial.mainTexture;
                    go.logos[1].GetComponent<Renderer>().material.mainTexture = mainMaterialChanger.currentMainMaterial.mainTexture;
                }
                break;
        }
    }

    public void SetScale(float scale)
    {
        if(!isLogoEnabled) return;
        logoMaterial.SetFloat("_Scale", scale);
        foreach (Cup go in cups)
        {
            go.logos[0].GetComponent<Renderer>().material.SetFloat("_Scale", scale);
            go.logos[1].GetComponent<Renderer>().material.SetFloat("_Scale", scale);
        }
    }

    public void RotateTexture(float angle)
    {
        if(!isLogoEnabled) return;
        logoMaterial.SetFloat("_RotationAngle", angle);
        foreach (Cup go in cups)
        {
            go.logos[0].GetComponent<Renderer>().material.SetFloat("_RotationAngle", angle);
            go.logos[1].GetComponent<Renderer>().material.SetFloat("_RotationAngle", angle);
        }
    }

    public void SetLogoMaterial(Material material)
    {
        if (!isLogoOnBothSides)
        {
            foreach (Cup go in cups)
            {
                go.logos[1].GetComponent<Renderer>().material = material;
            }
        }
    }

    public void SetEmptyLogo()
    {
        isLogoEnabled = false;
        
        logoMaterial.mainTexture = transparentTexture;
        foreach (Cup go in cups)
        {
            go.logos[0].GetComponent<Renderer>().material = logoMaterial;
            go.logos[1].GetComponent<Renderer>().material = logoMaterial;
        }
        Debug.Log("Logo cleared!");
    }
}