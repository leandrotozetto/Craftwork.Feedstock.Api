﻿namespace Craftwork.Feedstock.Api.Domain.Core.Validations.Interfaces
{
    public interface IRule
    {
        void Validate(string fieldName, dynamic fieldValue);
    }
}
