using UnityEngine;

public class StripsChanger : MonoBehaviour
{

    [SerializeField] private Material botStripMaterial;
    [SerializeField] private Material topStripMaterial;
    [SerializeField] private Material currentBotStripMaterial;
    [SerializeField] private Material currentTopStripMaterial;
    [SerializeField] private Material emptyTopStrip;
    [SerializeField] private Material emptyBotStrip;
    [SerializeField] private Cup[] cups;// All cups on scene
    [SerializeField] private CupCustomizer cupCustomizer;

    private bool isBotStripEmpty = false;
    private bool isTopStripEmpty = false;

    private void Start()
    {
        currentBotStripMaterial = botStripMaterial;
        currentTopStripMaterial = topStripMaterial;
    }

    public void SetCurrentBotStripeToEmpty()
    {
        isBotStripEmpty = true;
        currentBotStripMaterial = emptyBotStrip;
        currentBotStripMaterial.mainTexture = cupCustomizer.mainMaterialChanger.currentMainMaterial.mainTexture;
        foreach (Cup go in cups)
        {
            go.botStrip.GetComponent<Renderer>().material = emptyBotStrip;
        }
        Debug.Log($"Epty BS: {currentBotStripMaterial}");
    }

    public void SetCurrentTopStripeToEmpty()
    {
        isTopStripEmpty = true;
        currentTopStripMaterial = emptyTopStrip;
        currentTopStripMaterial.mainTexture = cupCustomizer.mainMaterialChanger.currentMainMaterial.mainTexture;
        foreach (Cup go in cups)
        {
            go.topStrip.GetComponent<Renderer>().material = emptyTopStrip;
        }
        Debug.Log($"Epty TS: {currentTopStripMaterial}");
    }

    public void ReceiveTopStripeImage(string imagePath)
    {
        isTopStripEmpty = false;
        currentTopStripMaterial = topStripMaterial;
        cupCustomizer.GetLoadedImage(imagePath, ReceiveTSCallback);
        Debug.Log($"Received {imagePath}");
    }

    public void ReceiveBotStripeImage(string imagePath)
    {
        isBotStripEmpty = false;
        currentBotStripMaterial = botStripMaterial;
        cupCustomizer.GetLoadedImage(imagePath, ReceiveBSCallback);
        Debug.Log($"Received {imagePath}");
    }

    public void ReceiveTSCallback(Texture2D texture)
    {
        topStripMaterial.mainTexture = texture;
        currentTopStripMaterial = topStripMaterial;
        foreach (Cup go in cups)
        {
            go.topStrip.GetComponent<Renderer>().material = topStripMaterial;
        }
    }

    public void ReceiveBSCallback(Texture2D texture)
    {
        botStripMaterial.mainTexture = texture;
        currentBotStripMaterial = botStripMaterial;
        foreach (Cup go in cups)
        {
            go.botStrip.GetComponent<Renderer>().material = botStripMaterial;
        }
    }

    public void UpdateEmptyStripsMaterial(Texture texture)
    {
        Debug.Log($"isBotStripEmpty={isBotStripEmpty}");
        Debug.Log($"isTopStripEmpty={isTopStripEmpty}");
        if(!isBotStripEmpty && !isTopStripEmpty) return;
        if(isBotStripEmpty) currentBotStripMaterial.mainTexture = texture;
        if(isTopStripEmpty) currentTopStripMaterial.mainTexture = texture;
        foreach (Cup go in cups)
        {
            if(isBotStripEmpty) go.botStrip.GetComponent<Renderer>().material = currentBotStripMaterial;
            if(isTopStripEmpty) go.topStrip.GetComponent<Renderer>().material = currentTopStripMaterial;
        }
    }
}