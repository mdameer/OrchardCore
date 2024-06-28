using System;
using GraphQL.Types;

namespace OrchardCore.Apis.GraphQL.Queries;

public interface IFilterInputObjectGraphType : IInputObjectGraphType
{
    void AddScalarFilterFields(Type graphType, string fieldName, string description);
}

public static class IFilterInputObjectGraphTypeExtensions
{
    public static void AddScalarFilterFields(this IFilterInputObjectGraphType filterInput, Type graphType, string fieldName)
    {
        filterInput.AddScalarFilterFields(graphType, fieldName, string.Empty);
    }

    public static void AddScalarFilterFields<TGraphType>(this IFilterInputObjectGraphType filterInput, string fieldName)
    {
        filterInput.AddScalarFilterFields(typeof(TGraphType), fieldName, string.Empty);
    }

    public static void AddScalarFilterFields<TGraphType>(this IFilterInputObjectGraphType filterInput, string fieldName, string description)
    {
        filterInput.AddScalarFilterFields(typeof(TGraphType), fieldName, description);
    }
}
