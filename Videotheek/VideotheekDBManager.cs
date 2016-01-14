﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace Videotheek
{
    public class VideotheekDBManager
    {
        private static ConnectionStringSettings conVideoSetting = ConfigurationManager.ConnectionStrings["videotheek"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conVideoSetting.ProviderName);

        public DbConnection GetConnection()
        {
            var conVideo = factory.CreateConnection();
            conVideo.ConnectionString = conVideoSetting.ConnectionString;
            return conVideo;
        }
    }
}
