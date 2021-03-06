﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.Messaging;

// http://go.microsoft.com/fwlink/?LinkId=290986&clcid=0x409

namespace KidsList
{
    internal class KidsListPush
    {
      
        
        public async static void UploadChannel()
        {
            var channel = await Windows.Networking.PushNotifications.PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            try
            {
                await App.KidsListClient.GetPush().RegisterNativeAsync(channel.Uri);
            }
            catch (Exception exception)
            {
                HandleRegisterException(exception);
            }
        }

        private static void HandleRegisterException(Exception exception)
        {

        }
        
    }
}
