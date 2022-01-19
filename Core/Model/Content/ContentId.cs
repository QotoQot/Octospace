using CSharpFunctionalExtensions;
using Microsoft.Toolkit.Diagnostics;
using System;
namespace Core.Model.Content
{
    public class Id : SimpleValueObject<string>
    {
        public Id(string value) : base(value)
        {
            Guard.IsNotNullOrEmpty(value, nameof(value));
        }
    }
}
