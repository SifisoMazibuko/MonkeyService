﻿using System;
using System.Collections.Generic;
using System.Net;
using MonkeyService.Droid.Service;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(MonkeyWebService))]
namespace MonkeyService.Droid.Service
{
    public class MonkeyWebService : IMonkeyService
    {
        //Json to Consume data from
        private const string weburi = "https://raw.githubusercontent.com/jamesmontemagno/MonkeysApp-AppIndexing/master/MonkeysApp/monkeydata.json";

        public MonkeyWebService()
        {
        }

        public List<Monkey> GetService(string weburi)
        {
            WebClient client = new WebClient();

            var response = client.DownloadData(weburi);

            var json = System.Text.Encoding.UTF8.GetString(response);
            var monkey = JsonConvert.DeserializeObject<List<Monkey>>(json);

            return monkey;
        }
    }
}