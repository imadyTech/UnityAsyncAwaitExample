#region COPYRIGHTS (c) 2016 Modest Tree Media Inc
/*
MIT License

Copyright (c) 2016 Modest Tree Media Inc

Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in 
the Software without restriction, including without limitation the rights to use, 
copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the 
Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace SVermeulen.Unity3dAsyncAwaitUtil
{
    public static class AwaitExtensions
    {
        public static TaskAwaiter<int> GetAwaiter(this Process process)
        {
            var tcs = new TaskCompletionSource<int>();
            process.EnableRaisingEvents = true;

            process.Exited += (s, e) => tcs.TrySetResult(process.ExitCode);

            if (process.HasExited)
            {
                tcs.TrySetResult(process.ExitCode);
            }

            return tcs.Task.GetAwaiter();
        }

        // Any time you call an async method from sync code, you can either use this wrapper
        // method or you can define your own `async void` method that performs the await
        // on the given Task
        public static async void WrapErrors(this Task task)
        {
            await task;
        }
    }
}