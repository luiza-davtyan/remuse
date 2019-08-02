using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Remuse
{
    /// <summary>
    /// For clear activity data
    /// </summary>
    internal class LogOutBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action != "com.mypackagename.ActionLogOut") return;

            //Here, you could just finish the activity, or do whatever you need to do
            var currentActivity = context as Activity;
            if (currentActivity != null)
            {
                currentActivity.Finish();
            }

        }
    }
}