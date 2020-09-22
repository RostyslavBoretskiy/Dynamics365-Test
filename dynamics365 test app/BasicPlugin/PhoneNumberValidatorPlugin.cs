using System;
using Microsoft.Xrm.Sdk;

namespace BasicPlugin
{
    public class PhoneNumberValidatorPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                try
                {
                    Entity entity = (Entity)context.InputParameters["Target"];
                    if (entity.LogicalName == "account")
                    {
                        if (entity.Attributes.Contains("telephone1") && !string.IsNullOrEmpty(entity.Attributes["telephone1"].ToString()))
                        {
                            entity.Attributes["cr98e_is_validated"] = true;
                        }
                        else
                        {
                            entity.Attributes["cr98e_is_validated"] = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("PhoneNumberValidatorPlugin: " + ex.Message);
                }
            }
        }
    }
}