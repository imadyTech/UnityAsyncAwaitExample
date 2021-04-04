#region Copyrights imady
/*
 *Copyright(C) 2020 by imady Technology (Suzhou); All rights reserved.
 *Author:       Frank Shen
 *Date:         
 *Description:   
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using SVermeulen.Unity3dAsyncAwaitUtil;

namespace imady.Common
{
    public class ResourceLoadCompletedEventArgs :  EventArgs
    {
        public string ResourceName { get; set; }

        public ResourceLoadCompletedEventArgs(string name)
        {
            ResourceName = name;
        }
    }

    public class AsyncResourceLoader
    {
        public delegate void OnResourceDownloadedEventHandler(object sender, ResourceLoadCompletedEventArgs args);


        #region PUBLIC PROPERTIES
        /// <summary>
        /// 下载的进度。(-1表示当前不在下载过程。)
        /// </summary>
        public float DownloadProgress
        {
            get
            {
                if (isProgressing && requestCache != null)
                    return requestCache.downloadProgress;
                else
                    //UnityWebRequest对象是一次性的，下载完成后就被销毁，所以不能再去访问，否则会出现错误。
                    return -1;
            }
        }

        public event OnResourceDownloadedEventHandler ResourceLoadingCompleted;
        #endregion


        #region PRIVATE PROPERTIES
        /// <summary>
        /// 当前是否在下载的指示器。
        /// </summary>
        private bool isProgressing;

        /// <summary>
        /// 由于UnityWebRequest是使用using方式创建，需要缓存一下以便能够访问。
        /// </summary>
        private UnityWebRequest requestCache;
        #endregion


        #region PUBLIC METHODS
        public async Task<Texture2D> LoadTexture2D(string resourceInfo)
        {
            using (var request = UnityWebRequestTexture.GetTexture(resourceInfo))
            {
                requestCache = request;
                isProgressing = true;

                await request.SendWebRequest();
                if (request.isHttpError || request.isNetworkError)
                {
                    Debug.Log(request.error);
                    throw new InvalidOperationException();
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(request);
                    isProgressing = false;
                    ResourceLoadingCompleted(texture, new ResourceLoadCompletedEventArgs(texture.name));
                    return texture;
                }
            }
        }

        public async Task<AssetBundle> LoadPrefab(string resourceInfo)
        {
            using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(resourceInfo))
            {
                isProgressing = true;
                requestCache = request;

                await request.SendWebRequest();
                if (request.isHttpError || request.isNetworkError)
                {
                    Debug.Log(request.error);
                    throw new InvalidOperationException();
                }
                else
                {
                    var asset = DownloadHandlerAssetBundle.GetContent(request);
                    isProgressing = false;
                    ResourceLoadingCompleted(asset, new ResourceLoadCompletedEventArgs(asset.name));
                    return asset;
                }
            }
        }

        public async Task<AudioClip> LoadAudio(string resourceInfo, AudioType audiotype)
        {
            using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(resourceInfo, audiotype))
            {
                isProgressing = true;
                requestCache = request;

                await request.SendWebRequest();
                if (request.isHttpError || request.isNetworkError)
                {
                    Debug.Log(request.error);
                    throw new InvalidOperationException();
                }
                else
                {
                    var audio = DownloadHandlerAudioClip.GetContent(request);
                    isProgressing = false;
                    ResourceLoadingCompleted(audio, new ResourceLoadCompletedEventArgs(audio.name));
                    return audio;
                }
            }
        }
        #endregion

    }
}
