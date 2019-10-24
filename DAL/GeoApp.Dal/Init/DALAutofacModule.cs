using Autofac;
using GeoApp.DAL.Model;
using GeoApp.DAL.Repository;
using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeoApp.DAL.Init
{


    public class DALAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            // builder.RegisterType<JsonRepository>().As<IRepository<GeoInformation, string>>();


            builder.Register(c =>
            {
                var appRoot = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.LastIndexOf("GeoApp"));
                appRoot = Directory.GetParent(appRoot).Parent.FullName;

                var path = Path.Combine(appRoot, @"DATA\FakeData");

                string[] filePaths = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly);

                return new JsonRepository(filePaths);
            }).As<IRepository<GeoInformation, string>>();
        }

    }
}
