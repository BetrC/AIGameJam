using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class WebDownloader : MonoBehaviour
{
    public static WebDownloader Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void DownloadImage(string imageUrl, string fileName, Action<Texture2D> onSuccess,
        Action<Exception> onFailure = null, bool reDownload = false)
    {
        if (!reDownload)
        {
            Texture2D texture = GetImage(fileName);

            if (texture != null)
            {
                onSuccess?.Invoke(texture);
                return;
            }
        }

        StartCoroutine(IE_ImageDownloader(imageUrl, texture =>
        {
            SaveImage(texture, fileName);
            onSuccess?.Invoke(texture);
        }, onFailure));
    }

    public void GetText(string url, Action<string> onSuccess, Action<Exception> onFailure = null)
    {
        StartCoroutine(IE_TextDownloader(url, onSuccess, onFailure));
    }

    private IEnumerator IE_ImageDownloader(string imageUrl, Action<Texture2D> onSuccess,
        Action<Exception> onFailure = null)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            DownloadHandlerTexture textureDownloadHandler = (DownloadHandlerTexture)request.downloadHandler;
            Texture2D texture = textureDownloadHandler.texture;

            if (texture == null)
            {
                onFailure?.Invoke(new Exception("image not available"));
                yield break;
            }

            onSuccess?.Invoke(texture);
            yield break;
        }

        onFailure?.Invoke(new Exception(request.error));
    }

    private static void SaveImage(Texture2D image, string filename)
    {
        string savePath = Application.persistentDataPath;
        try
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            File.WriteAllBytes(savePath + filename, image.GetRawTextureData());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private static Texture2D GetImage(string fileName)
    {
        string savePath = Application.persistentDataPath + "/";

        try
        {
            if (File.Exists(savePath + fileName))
            {
                byte[] bytes = File.ReadAllBytes(savePath + fileName);
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadRawTextureData(bytes);
                return texture;
            }

            return null;
        }
        catch (Exception e)
        {
            return null;
        }
    }


    private IEnumerator IE_TextDownloader(string url, Action<string> onSuccess,
        Action<Exception> onFailure = null)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string text = request.downloadHandler.text;
            onSuccess?.Invoke(text);
            yield break;
        }

        onFailure?.Invoke(new Exception(request.error));
    }
}