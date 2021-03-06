﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public List<PARENT> GetAllParents()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            var parents = from p in dc.PARENTs
                          select p;

            return parents.ToList();
        }


        public List<CHILDREN> GetAllChildren()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            var children = from c in dc.CHILDRENs
                          select c;

            return children.ToList();
        }

        public List<LIST> GetAllTasks()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();

            var Tasks =
                from t in dc.LISTs
                where t.ID[1]
                select t;
                           
                           
                           ;


            return Tasks.ToList();
        }
    }
}
