#region Copyrights imady
/*
 *Copyright(C) 2020 by imady Technology (Suzhou); All rights reserved.
 *Author:       Frank Shen
 *Date:         2020-11-03
 *Description:  Testing the GetWaiter/INotifyCompletion 
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class UnityWebReqeustResourceLoader : MonoBehaviour
{
    public UnityWebReqeustResourceLoader()
    {

    }


    public CustomAwaiter<Texture2D> GetTexture2D(string url)
    {
        var awaiter = new CustomAwaiter<Texture2D>();
        StartCoroutine(LoadTexture2D(url, awaiter));
        return awaiter;
    }




    public event ResourceLoadedEventHandler<Texture2D> TextureLoaded;
    public event ResourceLoadedEventHandler<AssetBundle> AssetBundleLoaded;
    public event ResourceLoadedEventHandler<AudioClip> AudioLoaded;

    private IEnumerator LoadTexture2D(string resourceInfo, CustomAwaiter<Texture2D> awaiter)
    {

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(Application.persistentDataPath + "/" + resourceInfo); ;
        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            var texture = DownloadHandlerTexture.GetContent(request);
            awaiter.Complete(texture, null);
            //TextureLoaded(resourceInfo, new ResourceLoadedEventArgs<Texture2D>(texture));
        }
    }
}

