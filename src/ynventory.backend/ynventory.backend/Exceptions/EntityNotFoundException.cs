using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class EntityNotFoundException : YnventoryException
    {
        public EntityNotFoundException(string entityName, object entityId) : base(ErrorCodes.Data.EntityNotFound, "EntityNotFound", entityName, entityId)
        {
            EntityName = entityName;
            EntityId= entityId;
        }

        public string EntityName { get; set; }
        public object EntityId { get; set; }

        public override IDictionary<string, object?>? Data => new Dictionary<string, object?>()
        {
            ["entityName"] = EntityName,
            ["entityId"] = EntityId
        };
    }
}
