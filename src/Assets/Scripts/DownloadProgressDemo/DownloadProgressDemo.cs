#region Copyrights imady
/*
 *Copyright(C) 2020 by imady Technology (Suzhou); All rights reserved.
 *Author:       Frank Shen
 *Date:         
 *Description:   
 */
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVermeulen.Unity3dAsyncAwaitUtil;
using imady.Common;
using UnityEngine.UI;
using System.Linq;

public class DownloadProgressDemo : MonoBehaviour
{
    string targetUrl = "此处替换为你的资源下载地址.unity3d";


    AsyncResourceLoader loader;
    // Start is called before the first frame update
    public async void Start()
    {
        //创建loader实例
        loader = new AsyncResourceLoader();
        //下载完成时发送通知
        loader.ResourceLoadingCompleted += OnDownloadCompleted;

        //开始异步下载
        var assetBundle = await loader.LoadPrefab(targetUrl);
        
        //下载完成时，继续执行以下代码：
        UnityEngine.Object[] obj = assetBundle.LoadAllAssets<GameObject>();
        var gameObject = (GameObject)UnityEngine.Object.Instantiate(obj.First());
    }

    // Update is called once per frame
    void Update()
    {
        //如果DownloadProgress为-1则说明下载已经完成了，不要再刷新下载进度。
        if (loader.DownloadProgress < 0) return;
        //在下载过程中可以访问loader，得到下载进度。
        UIManager.ShowPrompt("Downloading......" + ((int)(loader.DownloadProgress * 10000)) / 100f + "%");
    }

    private void OnDisable()
    {
        loader.ResourceLoadingCompleted -= OnDownloadCompleted;
    }

    void OnDownloadCompleted(object sender, ResourceLoadCompletedEventArgs args)
    {
        UIManager.ShowPrompt("Download completed.");
        //UIManager.HidePrompt();
    }
}
