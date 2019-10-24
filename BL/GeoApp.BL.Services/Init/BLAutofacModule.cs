
using Autofac;
using GeoApp.BL.Contracts.IServices;
using GeoApp.BL.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoApp.BL.Services.Init
{
    public class BLAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestService>().As<ITestService>();
            builder.RegisterType<PointsService>().As<IPointsService>();
            builder.RegisterType<RouteMatchingService>().As<IRouteMatchingService>();
        }

    }
}
