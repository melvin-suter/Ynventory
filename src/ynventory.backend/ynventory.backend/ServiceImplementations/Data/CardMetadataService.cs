using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Data;
using Ynventory.Data;
using Ynventory.Data.Models;

namespace Ynventory.Backend.ServiceImplementations.Data
{
    public class CardMetadataService : ICardMetadataService
    {
        private readonly IScryfallClient _client;
        private readonly YnventoryDbContext _context;

        public CardMetadataService(IScryfallClient client, YnventoryDbContext context)
        {
            _client = client;
            _context = context;
        }

        public async Task<CardMetadataResponse> GetCardMetadata(Guid cardMetadataId)
        {
            var metadata = await _context.CardMetadata.FindAsync(cardMetadataId);
            if (metadata is null)
            {
                var scryfallMetadata = await _client.GetMetadata(cardMetadataId);

                metadata = new CardMetadata
                {
                    Id = scryfallMetadata.Id,
                    Name = scryfallMetadata.Name,
                    Lang = scryfallMetadata.Lang,
                    Layout = scryfallMetadata.Layout,
                    ImageUrlSmall = scryfallMetadata.ImageUrlSmall,
                    ImageUrlLarge = scryfallMetadata.ImageUrlLarge,
                    Type = scryfallMetadata.Type,
                    ManaCost = scryfallMetadata.ManaCost,
                    OracleText = scryfallMetadata.OracleText,
                    Power = scryfallMetadata.Power,
                    Toughness = scryfallMetadata.Toughness,
                    ManaCostTotal = scryfallMetadata.ManaCostTotal,
                    Colors = scryfallMetadata.Colors.Select(x => new CardColor
                    {
                        Color = x,
                        CardMetadataId = scryfallMetadata.Id
                    }).ToList(),
                    ColorIdentity = scryfallMetadata.ColorIdentity.Select(x => new CardColorIdentity
                    {
                        ColorIdentity = x,
                        CardMetadataId = scryfallMetadata.Id
                    }).ToList(),
                    Keywords = scryfallMetadata.Keywords.Select(x => new CardKeyword
                    {
                        Keyword = x,
                        CardMetadataId = scryfallMetadata.Id
                    }).ToList()
                };

                _context.CardMetadata.Add(metadata);
                await _context.SaveChangesAsync();
            }

            return ToResponse(metadata);
        }

        public async Task<CardMetadataResponse> UpdateCardMetadata(Guid cardMetadataId)
        {
            var metadata = await _context.CardMetadata.FindAsync(cardMetadataId);
            if (metadata is null)
            {
                throw new CardMetadataNotFoundException(cardMetadataId);
            }

            var scryfallCard = await _client.GetMetadata(cardMetadataId);

            metadata.Lang = scryfallCard.Lang;
            metadata.Layout = scryfallCard.Layout;
            metadata.ImageUrlSmall = scryfallCard.ImageUrlSmall;
            metadata.ImageUrlLarge = scryfallCard.ImageUrlLarge;
            metadata.Type = scryfallCard.Type;
            metadata.ManaCost = scryfallCard.ManaCost;
            metadata.OracleText = scryfallCard.OracleText;
            metadata.Power = scryfallCard.Power;
            metadata.Toughness = scryfallCard.Toughness;
            metadata.ManaCostTotal = scryfallCard.ManaCostTotal;
            metadata.Colors.Clear();
            scryfallCard.Colors.ToList().ForEach(x => metadata.Colors.Add(new CardColor
            {
                Color = x,
                CardMetadataId = cardMetadataId,
            }));
            scryfallCard.ColorIdentity.ToList().ForEach(x => metadata.ColorIdentity.Add(new CardColorIdentity
            {
                ColorIdentity = x,
                CardMetadataId = cardMetadataId,
            }));
            scryfallCard.Keywords.ToList().ForEach(x => metadata.Keywords.Add(new CardKeyword
            {
                Keyword = x,
                CardMetadataId = cardMetadataId,
            }));

            await _context.SaveChangesAsync();

            return ToResponse(metadata);
        }

        private static CardMetadataResponse ToResponse(CardMetadata metadata)
        {
            return new CardMetadataResponse
            {
                Id = metadata.Id,
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
            };
        }
    }
}
