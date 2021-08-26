﻿using Autofac;
using DemoLibrary.Logic;
using DemoLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	public static class ContainerConfig
	{
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<PersonProcessor>().As<IPersonProcessor>();
            builder.RegisterType<SqlServerDataAccess>().As<ISqlServerDataAccess>();

            return builder.Build();
        }
    }
}
