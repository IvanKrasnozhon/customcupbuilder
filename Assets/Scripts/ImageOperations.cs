using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ImageOperations
{
    public static Texture2D GetImageByName(string imagePath)
    {
        byte[] bytes = File.ReadAllBytes(imagePath);
        Texture2D resultImage = new Texture2D(1, 1);
        resultImage.LoadImage(bytes);
        return resultImage;
    }

    public static Texture2D TextureToTexture2D(Texture texture)
    {
        if (texture is Texture2D)
        {
            return (Texture2D)texture;
        }
        else
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(texture, renderTexture);

            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTexture;

            Texture2D texture2D = new Texture2D(texture.width, texture.height);
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTexture);

            return texture2D;
        }
    }

    public static string EncodeBase64(string data)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
        return Convert.ToBase64String(bytes);
    }

    public static Texture ConvertTextureFromBase64(string base64TextureData)
    {
        byte[] textureData = Convert.FromBase64String(base64TextureData);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(textureData);
        return texture;
    }

    public static string ConvertTextureToBase64(Texture texture)
    {
        Texture2D texture2D = TextureToTexture2D(texture);

        byte[] textureData = texture2D.EncodeToPNG();
        string base64String = Convert.ToBase64String(textureData);

        return base64String;
    }

    public static Texture2D RotateImage(Texture2D tex, float angleDegrees)
    {
        int width = tex.width;
        int height = tex.height;
        float halfHeight = height * 0.5f;
        float halfWidth = width * 0.5f;

        var texels = tex.GetRawTextureData<Color32>();
        var copy = System.Buffers.ArrayPool<Color32>.Shared.Rent(texels.Length);
        Unity.Collections.NativeArray<Color32>.Copy(texels, copy, texels.Length);

        float phi = Mathf.Deg2Rad * angleDegrees;
        float cosPhi = Mathf.Cos(phi);
        float sinPhi = Mathf.Sin(phi);

        int address = 0;
        for (int newY = 0; newY < height; newY++)
        {
            for (int newX = 0; newX < width; newX++)
            {
                float cX = newX - halfWidth;
                float cY = newY - halfHeight;
                int oldX = Mathf.RoundToInt(cosPhi * cX + sinPhi * cY + halfWidth);
                int oldY = Mathf.RoundToInt(-sinPhi * cX + cosPhi * cY + halfHeight);
                bool InsideImageBounds = (oldX > -1) & (oldX < width)
                                       & (oldY > -1) & (oldY < height);

                texels[address++] = InsideImageBounds ? copy[oldY * width + oldX] : default;
            }
        }

        // No need to reinitialize or SetPixels - data is already in-place.
        tex.Apply(true);

        System.Buffers.ArrayPool<Color32>.Shared.Return(copy);

        return tex;
    }
}