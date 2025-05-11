﻿
using System.Xml.Linq;
using FC.Codeflix.Catalog.Domain.Exceptions;

namespace FC.Codeflix.Catalog.Domain.Validation;

public class DomainValidation
{
    public static void NotNull(object? target,string fieldName)
    {
        if (target == null)
            throw new EntityValidationException($"{fieldName} should not be null");
    }

    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (String.IsNullOrEmpty(target) || String.IsNullOrWhiteSpace(target))
            throw new EntityValidationException($"{fieldName} should not be null or empty");
    }

    public static void MinLength(string target,int minLength,string fieldName)
    {
        if(target.Length < minLength)
            throw new EntityValidationException($"{fieldName} cannot be less than {minLength} characters long");
    }

    public static void MaxLength(string target, int maxLength, string fieldName)
    {
        if (target.Length > maxLength)
            throw new EntityValidationException($"{fieldName} cannot have more than {maxLength} characters long");
    }
}
