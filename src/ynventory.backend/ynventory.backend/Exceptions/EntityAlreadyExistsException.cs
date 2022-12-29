using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class EntityAlreadyExistsException : YnventoryException
    {
        public EntityAlreadyExistsException(string entityName, object entityIdentifier) : base(ErrorCodes.Data.EntityAlreadyExists, "EntityAlreadyExists", entityName, entityIdentifier) 
        { 
            EntityName = entityName;
            EntityIdentifier = entityIdentifier;
        }
        
        public string EntityName { get; set; }
        public object EntityIdentifier { get; set; }

        public override IDictionary<string, object?>? Data => new Dictionary<string, object?>
        {
            ["entityName"] = EntityName,
            ["entityIdentifier"] = EntityIdentifier
        };
    }
}
