using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Constraints
{
    public class MaxCollectionLengthAttribute : ValidationAttribute
    {
        public readonly int _max;
        public MaxCollectionLengthAttribute(int max)
        {
            _max = max;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IEnumerable list)
            {
                int count = 0;
                var enumerator = list.GetEnumerator();
                while (enumerator.MoveNext()) count++;

                if (count > _max)
                {
                    return new ValidationResult(ErrorMessage ?? $"Cannot exceed {_max} items.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
