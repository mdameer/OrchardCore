using System.Linq;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OrchardCore.ContentManagement.GraphQL.Options;
using OrchardCore.ContentManagement.Metadata.Models;

namespace OrchardCore.ContentManagement.GraphQL.Queries.Types;

public class DynamicContentTypeOrderByInputBuilder : DynamicContentTypeBuilder
{
    public DynamicContentTypeOrderByInputBuilder(IHttpContextAccessor httpContextAccessor,
        IOptions<GraphQLContentOptions> contentOptionsAccessor,
        IStringLocalizer<DynamicContentTypeWhereInputBuilder> localizer)
        : base(httpContextAccessor, contentOptionsAccessor, localizer) { }

    public override void Build(ISchema schema, FieldType contentQuery, ContentTypeDefinition contentTypeDefinition, ContentItemType contentItemType)
    {
        var orderByInputType = (ContentItemOrderByInput)contentQuery.Arguments?.FirstOrDefault(x => x.Name == "orderBy")?.ResolvedType;

        if (orderByInputType != null)
        {
            BuildInternal(schema, contentTypeDefinition, orderByInputType);
        }
    }
}
