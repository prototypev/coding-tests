namespace UserRegistration.Core.Entities
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base definition for a persisted entity.
    /// </summary>
    public abstract class Entity
    {
        #region Public Methods and Operators

        /// <summary>
        /// Validates the property values of this instance against their ValidationAttributes.
        /// </summary>
        public void Validate()
        {
            // Calling Validator.ValidateObject() doesn't seem to use the DisplayName of the property. So instead we need to manually iterate through all the properties and validate them individually.
            ValidationContext validationContext = new ValidationContext(this);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this);
            foreach (PropertyDescriptor propertyDescriptor in properties)
            {
                validationContext.MemberName = propertyDescriptor.Name;
                validationContext.DisplayName = propertyDescriptor.DisplayName;

                object propertyValue = propertyDescriptor.GetValue(this);
                Validator.ValidateProperty(propertyValue, validationContext);
            }
        }

        #endregion
    }
}