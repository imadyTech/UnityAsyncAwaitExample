#region Copyrights imady
/*
 *Copyright(C) 2020 by imady Technology (Suzhou); All rights reserved.
 *Author:       Frank Shen
 *Date:         
 *Description:   
 */
#endregion

using System;

public delegate void ResourceLoadedEventHandler<R>(object sender, ResourceLoadedEventArgs<R> e);


public class ResourceLoadedEventArgs<T> : EventArgs
{
    public T Result { get; set; }

    public ResourceLoadedEventArgs(T result)
    {
        Result = result;
    }
}
