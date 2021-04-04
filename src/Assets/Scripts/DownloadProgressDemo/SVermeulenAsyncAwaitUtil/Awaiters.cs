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
using UnityEngine;

// TODO: Remove the allocs here, use a static memory pool?

namespace SVermeulen.Unity3dAsyncAwaitUtil
{
    public static class Awaiters
    {
        readonly static WaitForUpdate _waitForUpdate = new WaitForUpdate();
        readonly static WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        readonly static WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

        public static WaitForUpdate NextFrame
        {
            get { return _waitForUpdate; }
        }

        public static WaitForFixedUpdate FixedUpdate
        {
            get { return _waitForFixedUpdate; }
        }

        public static WaitForEndOfFrame EndOfFrame
        {
            get { return _waitForEndOfFrame; }
        }

        public static WaitForSeconds Seconds(float seconds)
        {
            return new WaitForSeconds(seconds);
        }

        public static WaitForSecondsRealtime SecondsRealtime(float seconds)
        {
            return new WaitForSecondsRealtime(seconds);
        }

        public static WaitUntil Until(Func<bool> predicate)
        {
            return new WaitUntil(predicate);
        }

        public static WaitWhile While(Func<bool> predicate)
        {
            return new WaitWhile(predicate);
        }
    }
}