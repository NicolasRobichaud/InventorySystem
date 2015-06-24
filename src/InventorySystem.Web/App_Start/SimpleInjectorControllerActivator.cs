using Microsoft.AspNet.Mvc;
using SimpleInjector;
using System;
using System.Diagnostics;

namespace InventorySystem.Web.App_Start
{
    internal sealed class SimpleInjectorControllerActivator : IControllerActivator
    {
        private readonly Container container;
        public SimpleInjectorControllerActivator(Container container)
        {
            this.container = container;
        }

        [DebuggerStepThrough]
        public object Create(ActionContext context, Type controllerType)
        {
            return container.GetInstance(controllerType);
        }
    }
}
