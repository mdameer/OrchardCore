using GraphQL.Types;

namespace OrchardCore.Apis.GraphQL.Queries.Types;

public class OrderByDirectionGraphType : EnumerationGraphType
{
    public OrderByDirectionGraphType()
    {
        Name = "OrderByDirection";
        Description = "the order by direction";
        Add("ASC", OrderByDirection.Ascending, "orders content items in ascending order");
        Add("DESC", OrderByDirection.Descending, "orders content items in descending order");
    }
}

public enum OrderByDirection
{
    Ascending,
    Descending
}
