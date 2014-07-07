using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenEcnu.UserInterface.Common
{
    public class AntiXssDataAnnotationsModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            foreach (var attribute in attributes.OfType<AntiXssAttribute>())
            {
                yield return new AntiXssDataAnnotationsModelValidator(metadata, context, attribute);
            }
        }
    }
}