using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CupCustomizer : MonoBehaviour
{
    [SerializeField] public StripsChanger stripsChanger;
    [SerializeField] public MainMaterialChanger mainMaterialChanger;

    private IEnumerator LoadImage(string imagePath, Action<Texture2D> callback)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imagePath))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                callback.Invoke(texture);
                Debug.Log("Загружена текстура изображения");
            }
            else
            {
                Debug.Log("Не удалось загрузить изображение: " + www.error);
            }
        }
    }

    public void GetLoadedImage(string imagePath, Action<Texture2D> callback)
    {
        StartCoroutine(LoadImage(imagePath, callback));
    }

}
