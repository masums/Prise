﻿#if NETCORE3_0 || NETCORE3_1
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Prise.Infrastructure;

namespace Prise
{
    public class DefaultAssemblyLoaderWithNativeResolver<T> : DisposableAssemblyUnLoader, IPluginAssemblyLoader<T>
    {
        private readonly IPluginLogger<T> logger;
        private readonly IAssemblyLoadOptions<T> options;
        private readonly IHostFrameworkProvider hostFrameworkProvider;
        private readonly IHostTypesProvider<T> hostTypesProvider;
        private readonly IDowngradableDependenciesProvider<T> downgradableDependenciesProvider;
        private readonly IRemoteTypesProvider<T> remoteTypesProvider;
        private readonly IDependencyPathProvider<T> dependencyPathProvider;
        private readonly IProbingPathsProvider<T> probingPathsProvider;
        private readonly IRuntimePlatformContext runtimePlatformContext;
        private readonly IDepsFileProvider<T> depsFileProvider;
        private readonly IPluginDependencyResolver<T> pluginDependencyResolver;
        private readonly INativeAssemblyUnloader nativeAssemblyUnloader;
        private readonly IAssemblyLoadStrategyProvider assemblyLoadStrategyProvider;


        public DefaultAssemblyLoaderWithNativeResolver(
            IPluginLogger<T> logger,
            IAssemblyLoadOptions<T> options,
            IHostFrameworkProvider hostFrameworkProvider,
            IHostTypesProvider<T> hostTypesProvider,
            IDowngradableDependenciesProvider<T> downgradableDependenciesProvider,
            IRemoteTypesProvider<T> remoteTypesProvider,
            IDependencyPathProvider<T> dependencyPathProvider,
            IProbingPathsProvider<T> probingPathsProvider,
            IRuntimePlatformContext runtimePlatformContext,
            IDepsFileProvider<T> depsFileProvider,
            IPluginDependencyResolver<T> pluginDependencyResolver,
            INativeAssemblyUnloader nativeAssemblyUnloader,
            IAssemblyLoadStrategyProvider assemblyLoadStrategyProvider) : base()
        {
            this.logger = logger;
            this.options = options;
            this.hostFrameworkProvider = hostFrameworkProvider;
            this.hostTypesProvider = hostTypesProvider;
            this.downgradableDependenciesProvider = downgradableDependenciesProvider;
            this.remoteTypesProvider = remoteTypesProvider;
            this.dependencyPathProvider = dependencyPathProvider;
            this.probingPathsProvider = probingPathsProvider;
            this.runtimePlatformContext = runtimePlatformContext;
            this.depsFileProvider = depsFileProvider;
            this.pluginDependencyResolver = pluginDependencyResolver;
            this.nativeAssemblyUnloader = nativeAssemblyUnloader;
            this.assemblyLoadStrategyProvider = assemblyLoadStrategyProvider;
        }

        public virtual Assembly Load(IPluginLoadContext pluginLoadContext)
        {
            var loadContext = new DefaultAssemblyLoadContextWithNativeResolver<T>(
                this.logger,
                this.options,
                this.hostFrameworkProvider,
                this.hostTypesProvider,
                this.downgradableDependenciesProvider,
                this.remoteTypesProvider,
                this.dependencyPathProvider,
                this.probingPathsProvider,
                this.runtimePlatformContext,
                this.depsFileProvider,
                this.pluginDependencyResolver,
                this.nativeAssemblyUnloader,
                this.assemblyLoadStrategyProvider
            );

            var loadedPluginKey = new LoadedPluginKey(pluginLoadContext);
            this.loadContexts[loadedPluginKey] = loadContext;
            this.loadContextReferences[loadedPluginKey] = new System.WeakReference(loadContext);

            return loadContext.LoadPluginAssembly(pluginLoadContext);
        }

        public virtual Task<Assembly> LoadAsync(IPluginLoadContext pluginLoadContext)
        {
            var loadContext = new DefaultAssemblyLoadContextWithNativeResolver<T>(
                this.logger,
                this.options,
                this.hostFrameworkProvider,
                this.hostTypesProvider,
                this.downgradableDependenciesProvider,
                this.remoteTypesProvider,
                this.dependencyPathProvider,
                this.probingPathsProvider,
                this.runtimePlatformContext,
                this.depsFileProvider,
                this.pluginDependencyResolver,
                this.nativeAssemblyUnloader,
                this.assemblyLoadStrategyProvider
            );

            var loadedPluginKey = new LoadedPluginKey(pluginLoadContext);
            this.loadContexts[loadedPluginKey] = loadContext;
            this.loadContextReferences[loadedPluginKey] = new System.WeakReference(loadContext);

            return loadContext.LoadPluginAssemblyAsync(pluginLoadContext);
        }
    }
}
#endif