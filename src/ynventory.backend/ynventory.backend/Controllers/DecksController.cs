using Microsoft.AspNetCore.Mvc;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Resources;
using Ynventory.Backend.Services.Data;

namespace Ynventory.Backend.Controllers
{
    /// <summary>
    /// Provides functions to fetch and manipulate decks
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        private readonly IDeckService _deckService;

        public DecksController(IDeckService deckService)
        {
            _deckService = deckService;
        }

        /// <summary>
        /// Creates a new deck
        /// </summary>
        /// <param name="request">The details of the deck to create</param>
        /// <returns>The newly created deck</returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     POST /decks
        ///     {
        ///         "name": "super deck",
        ///         "description": "my super deck"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">The deck was successfully created</response>
        /// <response code="400">A deck with the given name already exists</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DeckResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateDeck(DeckCreateRequest request)
        {
            try
            {
                var deck = await _deckService.CreateDeck(request);
                return Created($"/api/decks/{deck.Id}", deck);
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Returns all decks
        /// </summary>
        /// <returns>A list of all decks</returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     GET /decks
        ///     
        /// </remarks>
        /// <response code="200">All decks have been read</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeckResponse[]))]
        public async Task<IActionResult> GetDecks()
        {
            return Ok(await _deckService.GetDecks());
        }

        /// <summary>
        /// Returns a deck to the given id
        /// </summary>
        /// <param name="deckId">The deck id</param>
        /// <returns>The deck associated with the id</returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     GET /decks/21
        ///     
        /// </remarks>
        /// <response code="200">The deck has been found</response>
        /// <response code="404">The deck does not exist</response>
        [HttpGet("{deckId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeckResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetDeck(int deckId)
        {
            try
            {
                return Ok(await _deckService.GetDeck(deckId));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Updates a deck to the given id with the given details
        /// </summary>
        /// <param name="deckId">The deck to update</param>
        /// <param name="request">The new values</param>
        /// <returns>The updated deck</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     PUT /decks/21
        ///     {
        ///         "id": 21,
        ///         "name": "even better deck",
        ///         "description": "this will surely win!"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">The deck has been succesfully updated</response>
        /// <response code="400">The request is invalid</response>
        /// <response code="404">The deck does not exist</response>
        [HttpPut("{deckId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeckResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateDeck(int deckId, DeckUpdateRequest request)
        {
            var result = CheckIdMismatch(deckId, request.Id);
            if (result is not null)
            {
                return result;
            }

            try
            {
                return Ok(await _deckService.UpdateDeck(request));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Deletes a deck to the given id
        /// </summary>
        /// <param name="deckId">The id of the deck to delete</param>
        /// <returns></returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     DELETE /decks/21    
        /// 
        /// </remarks>
        /// <response code="200">The deck has been deleted</response>
        /// <response code="404">The deck does not exist</response>
        [HttpDelete("{deckId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteDeck(int deckId)
        {
            try
            {
                await _deckService.DeleteDeck(deckId);
                return Ok();
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Adds a card to a deck
        /// </summary>
        /// <param name="deckId">The deck</param>
        /// <param name="request">The details of the card</param>
        /// <returns>The created card</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     POST /decks/21/cards
        ///     {
        ///         "cardMetadataId": "f295b713-1d6a-43fd-910d-fb35414bf58a",
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">The card has been added to the deck</response>
        /// <response code="400">The cardMetadataId does not exist</response>
        /// <response code="404">The deck was not found</response>
        /// <response code="502">There was an error calling the scryfall API</response>
        [HttpPost("{deckId}/cards")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DeckCardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status502BadGateway, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateCard(int deckId, DeckCardCreateRequest request)
        {
            try
            {
                var card = await _deckService.CreateCard(deckId, request);
                return Created($"/api/decks/{deckId}/cards/{card.Id}", card);
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Returns a list of all cards contained within a deck
        /// </summary>
        /// <param name="deckId">The deck</param>
        /// <returns>A list of all cards contained within the deck</returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     GET /decks/21/cards
        ///     
        /// </remarks>
        /// <response code="200">The list was returned</response>
        /// <response code="404">The deck does not exist</response>
        [HttpGet("{deckId}/cards")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeckCardResponse[]))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetCards(int deckId)
        {
            try
            {
                return Ok(await _deckService.GetCards(deckId));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Returns a card contained within a deck
        /// </summary>
        /// <param name="deckId">The deck</param>
        /// <param name="cardId">The card</param>
        /// <returns>The card associated with the id</returns>
        /// <remarks>
        /// 
        ///     GET /decks/21/cards/22
        /// 
        /// </remarks>
        /// <response code="200">The card was returned</response>
        /// <response code="404">The deck or card was not found</response>
        [HttpGet("{deckId}/cards/{cardId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeckCardResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetCard(int deckId, int cardId)
        {
            try
            {
                return Ok(await _deckService.GetCard(deckId, cardId));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Updates a card contained within a deck
        /// </summary>
        /// <param name="deckId">The deck</param>
        /// <param name="cardId">The card to update</param>
        /// <param name="request">The new values</param>
        /// <returns>The updated card</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     PUT /decks/21/cards/22
        ///     {
        ///         "id": 22,
        ///         "cardMetadataId": "f295b713-1d6a-43fd-910d-fb35414bf58a"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">The card was successfully updated</response>
        /// <response code="400">The request is invalid</response>
        /// <response code="404">The deck or card was not found</response>
        /// <response code="502">Tehre was an error calling the scryfall API</response>
        [HttpPut("{deckId}/cards/{cardId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeckCardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status502BadGateway, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateCard(int deckId, int cardId, DeckCardUpdateRequest request)
        {
            var result = CheckIdMismatch(cardId, request.Id);
            if (result is not null)
            {
                return result;
            }

            try
            {
                return Ok(await _deckService.UpdateCard(deckId, request));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Deletes a card in a  deck
        /// </summary>
        /// <param name="deckId">The deck</param>
        /// <param name="cardId">The card to delete</param>
        /// <returns></returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     DELETE /decks/21/cards/22
        ///     
        /// </remarks>
        /// <response code="200">The card was deleted</response>
        /// <response code="404">The deck or card was not found</response>
        [HttpDelete("{deckId}/cards/{cardId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCard(int deckId, int cardId)
        {
            try
            {
                await _deckService.DeleteCard(deckId, cardId);
                return Ok();
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        private static IActionResult? CheckIdMismatch(int expected, int actual)
        {
            if (actual != expected)
            {
                var response = new ErrorResponse(ErrorCodes.InvalidRequest, ErrorCodes.StatusCode(ErrorCodes.InvalidRequest), "Id mismatch", new Dictionary<string, object?>
                {
                    ["idExpected"] = expected,
                    ["idActual"] = actual
                });

                return response.ToResult();
            }

            return null;
        }
    }
}
