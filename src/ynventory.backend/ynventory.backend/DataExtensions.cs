using Ynventory.Backend.Contracts.Responses;
using Ynventory.Data.Models;

namespace Ynventory.Backend
{
    public static class DataExtensions
    {
        public static CardMetadataResponse ToResponse(this CardMetadata metadata)
        {
            return new CardMetadataResponse
            {
                Id = metadata.Id,
                Name = metadata.Name,
                Lang = metadata.Lang,
                Layout = metadata.Layout,
                ImageUrlSmall = metadata.ImageUrlSmall,
                ImageUrlLarge = metadata.ImageUrlLarge,
                Type = metadata.Type,
                ManaCost = metadata.ManaCost,
                OracleText = metadata.OracleText,
                Power = metadata.Power,
                Toughness = metadata.Toughness,
                ManaCostTotal = metadata.ManaCostTotal,
                Colors = metadata.Colors.Select(x => x.Color).ToArray(),
                ColorIdentity = metadata.ColorIdentity.Select(x => x.ColorIdentity).ToArray(),
                Keywords = metadata.Keywords.Select(x => x.Keyword).ToArray(),
                Legalities = metadata.Legalities.ToDictionary(x => x.PlayFormat, x => x.Legality),
            };

        }
    }
}
