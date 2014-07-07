using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OpenEcnu.UserInterface.Common
{
    public class AntiXssDataAnnotationsModelValidator : DataAnnotationsModelValidator
    {
        public AntiXssDataAnnotationsModelValidator(ModelMetadata metadata, ControllerContext context,
            AntiXssAttribute attribute):base(metadata,context,attribute)
        {
            
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            var validationContext = new ValidationContext(container ?? base.Metadata.Model, null, null);
            validationContext.DisplayName = base.Metadata.DisplayName;
            validationContext.MemberName = base.Metadata.PropertyName;
            ValidationResult validationResult = this.Attribute.GetValidationResult(base.Metadata.Model,
                validationContext);
            yield break;
        }
    }
}