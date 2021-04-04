using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Networking;
using UnityEngine.UI;


/// <summary>
/// 这是学习unity coroutine过程中的试验代码。请忽略。
/// </summary>
public class YieldReturnTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string response = string.Empty;

        //#1
        //var result = StartCoroutine(UnityWebRequestGet("http://www.baidu.com"));
        //发现UnitywebRequest是一次性的，当方法执行完毕就被清空成null




        //#2
        //var result = new Action(
        //    async () =>
        //    {
        //        textBox.text = await HttpClientGet("http://baidu.com/images/header-2.jpg");
        //    }) ;
        //result.Invoke();


        //#3
        //yield return试验 - 不成功
        var result = StartCoroutine(Test3());//
        textBox.text = "This is the original text... waiting...!!!";

        //#4
        //YieldHttpClient client;
        //string receiver="";
        //var result = StartCoroutine(receiver = Test2().Current);

        //#5
        //不使用StartCoroutine, 语法上没有问题，但是Test1并不执行。
        //client = Test1().Current;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Text textBox;
    public Text timerBox;
    IEnumerator Test()
    {
        HttpClient client = new HttpClient();
        string response = string.Empty;

        //yield return 后面跟的Action根本不执行
        yield return new Action(
            async () =>
            response = await HttpClientGet("http://www.baidu.com"));

        textBox.text = response;
    }
    IEnumerator Test1()
    {
        string response = string.Empty;

        yield return new UnityYieldTest(2000);
        textBox.text = "YieldTest1";

    }

    IEnumerator<string> Test2()
    {
        string response = string.Empty;

        yield return "YieldTest2";
    }


    IEnumerator<WaitForSeconds> Test3()
    {
        var testObject = new WaitForSeconds(1);
        yield return testObject;
        timerBox.text = "5";
        yield return testObject;
        timerBox.text = "4";
        yield return testObject;
        timerBox.text = "3";
        yield return testObject;
        timerBox.text = "2";
        yield return testObject;
        timerBox.text = "1";
        yield return testObject;
        timerBox.text = "0";
        yield return testObject;
        textBox.text = "BINGO!!!";

    }

    public UnityWebRequest webRequest;
    public IEnumerator UnityWebRequestGet(string url)
    {
        UnityWebRequest webRequest = new UnityWebRequest();
        webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();

        if (webRequest.isHttpError || webRequest.isNetworkError)
            Debug.Log(webRequest.error);
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
        }
    }


    public async Task<string> HttpClientGet(string urlPath)
    {
        HttpClientHandler handler = new HttpClientHandler();
        HttpClient httpClient = new HttpClient();
        //异步请求
        HttpResponseMessage response = await httpClient.GetAsync(urlPath);
        //确保响应正常，如果响应不正常，则抛出异常
        //response.EnsureSuccessStatusCode();
        //异步读取数据
        string resultStr = await response.Content.ReadAsStringAsync();
        return resultStr;
    }

}


public class UnityYieldTest : YieldInstruction
{
    public UnityYieldTest(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }
}
