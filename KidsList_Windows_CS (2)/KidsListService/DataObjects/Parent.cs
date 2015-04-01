using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service;

namespace KidsListService.DataObjects
{
    public class Parent : EntityData
    {
        public int  ID { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONENUMBER { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
    }
}