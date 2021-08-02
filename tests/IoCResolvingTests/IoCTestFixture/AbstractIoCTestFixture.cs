using Autofac;
using AutofacCallerAssemblyContextualBinding.Behavior;
using AutofacCallerAssemblyContextualBinding.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using IContainer = Autofac.IContainer;

namespace IoCResolvingTests.IoCTestFixture
{
    public abstract class AbstractIoCTestFixture : IDisposable
    {
        public static IContainer Container { get; private set; }
        private bool isDisposed;
        public AbstractIoCTestFixture(string[] dllPlugins, AbstractBinderBehavior binderBehavior)
        {
            var builder = new ContainerBuilder();

            CheckIfFilesExists(dllPlugins);

            var assemblies = dllPlugins.Select(dll => Assembly.LoadFrom(dll)).ToArray();
            builder.RegisterModules(assemblies, binderBehavior);

            Container = builder.Build();
        }

        private static void CheckIfFilesExists(IEnumerable<string> dlls)
        {
            var missed = dlls.Where(x => !File.Exists(x)).Select(x => x);
            if (missed.Any())
            {
                throw new FileNotFoundException(string.Join(',', missed.ToList()));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;
            if (disposing)
            {
                Container.Dispose();
            }
            isDisposed = true;
        }

        ~AbstractIoCTestFixture()
            => Dispose(false);
    }
}