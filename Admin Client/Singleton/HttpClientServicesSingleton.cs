﻿using Admin_Client.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Singleton
{
    //nice, sure, pull?
    //But I do.. Just normal one

    public class HttpClientServicesSingleton : HttpClientServices
    {
        private HttpClientServicesSingleton() { }
        private static HttpClientServicesSingleton instance = null;
        public static HttpClientServicesSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HttpClientServicesSingleton();
                }
                return instance;
            }
        }
    }
}
