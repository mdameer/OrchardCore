using System;
using GraphQL.Types;
using OrchardCore.Apis.GraphQL.Queries.Types;

namespace OrchardCore.Apis.GraphQL.Queries;

public class OrderByInputObjectGraphType : OrderByInputObjectGraphType<object>, IFilterInputObjectGraphType
{
}

public class OrderByInputObjectGraphType<TSourceType> : InputObjectGraphType<TSourceType>, IFilterInputObjectGraphType
{
    public virtual void AddScalarFilterFields(Type graphType, string fieldName, string description)
    {
        AddField(new FieldType
        {
            Type = typeof(OrderByDirectionGraphType),
            Name = fieldName,
            Description = description
        });
    }
}