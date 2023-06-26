using UnityEngine;

public class MainMaterialChanger : MonoBehaviour
{
    [SerializeField] public Material currentMainMaterial;
    [SerializeField] private CupCustomizer cupCustomizer;

    public void ReceiveImage(string imagePath)
    {
        cupCustomizer.GetLoadedImage(imagePath, ReceiveMMCallback);
        Debug.Log($"Received MainTexture image {imagePath}");
        Debug.Log($"Current mainTexture {currentMainMaterial.mainTexture.name}");
    }

    public void ReceiveMMCallback(Texture2D texture)
    {
        currentMainMaterial.mainTexture = texture;
        //cupCustomizer.logoChanger.SetLogoMaterial(currentMainMaterial);
        cupCustomizer.stripsChanger.UpdateEmptyStripsMaterial(texture);
    }
}