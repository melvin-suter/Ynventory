namespace Ynventory.Backend.Exceptions
{
    public class CardMetadataNotFoundException : EntityNotFoundException
    {
        public CardMetadataNotFoundException(Guid cardMetadataId) : base("CardMetadata", cardMetadataId) 
        {
        }
    }
}