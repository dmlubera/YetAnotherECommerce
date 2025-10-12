using System;
using System.Linq;
using System.Reflection;
using AutoFixture;
using AutoFixture.Xunit2;

namespace YetAnotherECommerce.Modules.Identity.Core.UnitTests.Customizations;

public class FixtureCustomizationAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
        var customizerType = typeof(IFixtureCustomizer<>).MakeGenericType(parameter.ParameterType);
        var customizer = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => customizerType.IsAssignableFrom(type))
            .Select(Activator.CreateInstance)
            .Cast<ICustomization>()
            .FirstOrDefault();

        return customizer ??
               throw new ArgumentException($"There is no fixture customization defined for `{parameter.ParameterType}` type.");
    }
}