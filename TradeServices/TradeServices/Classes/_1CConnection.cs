using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _1C.V8.Data;
using System.Configuration;

namespace TradeServices.Classes
{
    public class _1CConnection
    {
        public static string GetDataBase()
        {
            if (ConfigurationManager.AppSettings["utLocation"] == "server")
                return string.Format(@"Srvr=""{0}"";Ref=""{1}"";", ConfigurationManager.AppSettings["utServer"], ConfigurationManager.AppSettings["utDatabase"]);
            else
            {
                return string.Format(@"File=""{0}""", ConfigurationManager.AppSettings["utDatabase"]);
            }
        }
        //

        public static V8DbConnection CreateAndOpenConnection()
        {
            string versionStr = "Ver8_2";
            V8DbConnection.Version =
                    (ComConnectorVersion)Enum.Parse(typeof(ComConnectorVersion), versionStr, true);
            V8DbConnection connection = new V8DbConnection(GetDataBase(), ConfigurationManager.AppSettings["utUser"], ConfigurationManager.AppSettings["utPassword"]);
            
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                connection = null;
            }
            return connection;
        }

        public static void Close1CConnection(V8DbConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
                V8DbConnection.ClearPool();
                V8DbConnection.PoolCapacity = 5;
                //connection = null;
            }
        }
        public void tt() { }
    }
}