#region Copyrights imady
/*
 *Copyright(C) 2020 by imady Technology (Suzhou); All rights reserved.
 *Author:       Frank Shen
 *Date:         
 *Description:   
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomAsyncAwaitDemo : MonoBehaviour
{
    public Text caption;
    public Slider slider;
    public RawImage imageBox;
    public Texture2D texture2d;
    public float process;

    public UnityWebReqeustResourceLoader loader;

    async void Start()
    {
        caption.text = "Start";
        gameObject.AddComponent<UnityWebReqeustResourceLoader>();
        loader = gameObject.GetComponent<UnityWebReqeustResourceLoader>();

        //请确保application.persistentDataPath对应的地址存有以下名称的资源，或者改为你自己的图片资源的名称。
        process = 0;
        var result = await loader.GetTexture2D("/header-1.jpg");
        process++;
        imageBox.texture = result;
        result = await loader.GetTexture2D("/header-2.jpg");
        process++;
        imageBox.texture = result;
        result = await loader.GetTexture2D("/header-3.jpg");
        imageBox.texture = result;
        process++;
        result = await loader.GetTexture2D("/header-4.jpg");
        imageBox.texture = result;
        process++;
        result = await loader.GetTexture2D("/header-5.jpg");
        imageBox.texture = result;
        process++;
        result = await loader.GetTexture2D("/header-6.jpg");
        imageBox.texture = result;
        process++;
        result = await loader.GetTexture2D("/header-7.jpg");
        imageBox.texture = result;
        process++;
        result = await loader.GetTexture2D("/header-8.jpg");
        imageBox.texture = result;
        process++;
        result = await loader.GetTexture2D("/header-9.jpg");
        imageBox.texture = result;
        process++;

    }


    private void Update()
    {
        if (process > 9) return;
        slider.value += 0.0018f;
        if (process>0) caption.text = (int)(process/9 * 100 )+ "%";
    }

}
