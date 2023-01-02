using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class EntityIllegalStateException : YnventoryException
    {
        public EntityIllegalStateException(string entityName, object entityId, string errorDetailKey, params object?[] errorDetailParams) 
            : this(entityName, entityId, FormatErrorDetail(errorDetailKey, errorDetailParams))
        {
        }

        private EntityIllegalStateException(string entityName, object entityId, string errorDetail)
            : base(ErrorCodes.Data.EntityIllegalState, "EntityIllegalState", entityName, entityId, errorDetail)
        {
            EntityName = entityName;
            EntityId = entityId;
            ErrorDetail = errorDetail;
        }

        private static string FormatErrorDetail(string key, params object?[] errorDetailParams)
        {
            return ResourcesReader.Strings.GetString(key, errorDetailParams);
        }

        public string EntityName { get; set; }
        public object EntityId { get; set; }
        public string ErrorDetail { get; set; }

        public override IDictionary<string, object?>? Data => new Dictionary<string, object?>
        {
            ["entityName"] = EntityName,
            ["entityId"] = EntityId,
            ["errorDetail"] = ErrorDetail
        };
    }
}
