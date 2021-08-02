# AutofacCallerAssemblyContextualBinding
It is a simple project, based od Autofac for creating a contextual binding when you work in plugin architecture and you have common or dedicated behavior for some actions
It bases on the Convention Over Configuration strategy, so if something is defined in the plugin - use it, if not - use the common one

##Example
Let's imagine the project which has three plugins
* PluginA
** IBehavior -> PluginABehavior 
* PluginB
** IBehavior -> PluginBBehavior 
* PluginC
and common part
* Common
** IBehavior -> CommonBehavior 
Each plugin is able to start an action. Action method takes as an argument that should be resolved base on context. If a plugin has its own class which implements behavior interface - use class from plugin, other case use the common one.
In such a situation
* PluginA - inject PluginABehavior
* PluginA - inject PluginBBehavior
* PluginC - inject CommonBehavior 

#Code
##Registration
It is necessary two step registration 
* Module registration - to inject binder behavior (it could be modificable by implementing AbstractBinderBehavior)
* Container builder - the binding is here, and it based on binder behavior
```csharp
var builder = new ContainerBuilder();
var assemblies = loadAssemblies();

//register modules to inject the BinderBehavior
builder.RegisterModules(assemblies, binderBehavior);

//binding based on binder behavior
Container = builder.Build();
```
Next each plugin should have a module that should inherit from AbstractModule and should have the bining suitable for the module

```csharp
public class ModulesB : AbstractModule
    {
        public override void Binder()
        {
            Bind<ITestContract, TestContractB>();
            Bind<IConvertor, TestContractBConverter>();
        }
    }
 ```