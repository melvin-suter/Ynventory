using Ynventory.Backend.Resources;

namespace Ynventory.Backend.Exceptions
{
    public class ScryfallCardNotFoundException : YnventoryException
    {
        public ScryfallCardNotFoundException(Guid cardId) : base(ErrorCodes.Scryfall.CardNotFound, "Scryfall_CardNotFound", cardId)
        {
            CardId = cardId;
        }

        public Guid CardId { get; }

        public override IDictionary<string, object?>? Data => new Dictionary<string, object?>
        {
            ["cardId"] = CardId
        };
    }
}
