using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CustomAwaiter<T> : INotifyCompletion
{
    public CustomAwaiter<T> GetAwaiter()
    {
        return this;
    }

    bool _isDone;
    Exception _exception;
    Action _continuation;
    T _result;

    public bool IsCompleted
    {
        get { return _isDone; }
    }



    public T GetResult()
    {
        //Assert(_isDone);

        //if (_exception != null)
        //{
        //    ExceptionDispatchInfo.Capture(_exception).Throw();
        //}
        return _result;
    }

    public void Complete(T result, Exception e)
    {
        //Assert(!_isDone);

        _isDone = true;
        _exception = e;
        _result = result;

        // Always trigger the continuation on the unity thread when awaiting on unity yield
        // instructions
        if (_continuation != null)
        {
            //RunOnUnityScheduler(_continuation);
            _continuation();
        }
    }
    void INotifyCompletion.OnCompleted(Action continuation)
    {
        //Assert(_continuation == null);
        //Assert(!_isDone);

        _continuation = continuation;
    }
    //static void Assert(bool condition)
    //{
    //    if (!condition)
    //    {
    //        throw new Exception("Assert hit in UnityAsyncUtil package!");
    //    }
    //}    
    
    //static void RunOnUnityScheduler(Action action)
    //{
    //    if (SynchronizationContext.Current == SyncContextUtil.UnitySynchronizationContext)
    //    {
    //        action();
    //    }
    //    else
    //    {
    //        SyncContextUtil.UnitySynchronizationContext.Post(_ => action(), null);
    //    }
    //}

}





public static class SyncContextUtil
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Install()
    {
        UnitySynchronizationContext = SynchronizationContext.Current;
        UnityThreadId = Thread.CurrentThread.ManagedThreadId;
    }

    public static int UnityThreadId
    {
        get; private set;
    }

    public static SynchronizationContext UnitySynchronizationContext
    {
        get; private set;
    }
}
