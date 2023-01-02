namespace Ynventory.Backend.Exceptions
{
    public class CardMoveIllegalException : EntityIllegalStateException
    {
        public CardMoveIllegalException(int cardId, int requestedQuantity, int actualQuantity)
            : base("Card", cardId, "CardMoveIllegal", cardId, requestedQuantity, actualQuantity)
        {
            RequestedQuantity = requestedQuantity;
            ActualQuantity = actualQuantity;
        }

        public int RequestedQuantity { get; }
        public int ActualQuantity { get; }

        public override IDictionary<string, object?>? Data => new Dictionary<string, object?>(base.Data!)
        {
            ["requestedQuantity"] = RequestedQuantity,
            ["actualQuantity"] = ActualQuantity,
        };
    }
}
