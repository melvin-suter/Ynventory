using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Resources;
using Ynventory.Backend.Services.Data;
using Ynventory.Data.Models;

namespace Ynventory.Backend.Controllers
{
    /// <summary>
    /// Provides functions to fetch and manipulate collections
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionsController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        /// <summary>
        /// Creates a new collection. Note that the name must be unique
        /// </summary>
        /// <param name="request">The details of the collection to create</param>
        /// <returns>The newly created collection</returns>
        /// <remarks>
        /// Example request
        ///     
        ///     POST /collections
        ///     {
        ///         "name": "my collection",
        ///         "description": "my super sick collection"
        ///     }
        ///    
        /// </remarks>
        /// <response code="201">The collection was successfuly created</response>
        /// <response code="400">The collection with the given name already exists</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CollectionResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateCollection(CollectionCreateRequest request)
        {
            try
            {
                var collection = await _collectionService.CreateCollection(request);
                return Created($"/api/collection/{collection.Id}", collection);
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Returns a collection for the given id
        /// </summary>
        /// <param name="collectionId">The id of the collection to read</param>
        /// <returns>The collection associated with the given id</returns>
        /// <remarks>
        /// Example request:
        ///
        ///     GET /collections/123
        ///     
        /// </remarks>
        /// <response code="200">The collection was found and returned</response>
        /// <response code="404">The collection was not found</response>
        [HttpGet("{collectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetCollection(int collectionId)
        {
            try
            {
                return Ok(await _collectionService.GetCollection(collectionId));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Returns a list of all collections
        /// </summary>
        /// <returns>A list of all collections</returns>
        /// <response code="200">The list has been created</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionResponse[]))]
        public async Task<IActionResult> GetCollections()
        {
            return Ok(await _collectionService.GetCollections());
        }

        /// <summary>
        /// Updates a collection for the given id.
        /// </summary>
        /// <param name="collectionId">The id of the collection to update</param>
        /// <param name="request">The new values</param>
        /// <returns>The updated collection</returns>
        /// <remarks>
        /// Example Request:
        /// 
        ///     PUT /collections/123
        ///     {
        ///         "id": 123,
        ///         "name": "my updated collection"
        ///         "description": "even more sick now"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">The collection was updated successfuly</response>
        /// <response code="400">The request is invalid</response>
        /// <response code="404">The collection for the given id was not found</response>
        [HttpPut("{collectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type =typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateCollection(int collectionId, CollectionUpdateRequest request)
        {
            var response = CheckIdMismatch(collectionId, request.Id);
            if (response is not null)
            {
                return response;
            }

            try
            {
                return Ok(await _collectionService.UpdateCollection(request));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Deletes a collection for the given id.
        /// </summary>
        /// <param name="collectionId"></param>
        /// <returns></returns>
        /// <remarks>
        /// Example request
        /// 
        ///     DELETE /collections/123
        /// 
        /// </remarks>
        /// <response code="200">The collection was successfully deleted</response>
        /// <response code="404">The collection to the given id does not exist</response>
        [HttpDelete("{collectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteCollection(int collectionId)
        {
            try
            {
                await _collectionService.DeleteCollection(collectionId);
                return Ok();
            } 
            catch(YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Retrieves all cards assigned to folders within a collection
        /// </summary>
        /// <param name="collectionId"></param>
        /// <returns>A collection of cards within the collection</returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     GET /collections/123/cards
        /// 
        /// </remarks>
        /// <response code="200">The cards were successfully retrieved</response>
        /// <response code="404">The collection was not found</response>
        [HttpGet("{collectionId}/cards")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardResponse[]))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetCards(int collectionId)
        {
            try
            {
                return Ok(await _collectionService.GetCards(collectionId));
            } 
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Creates a new collection item within a collection. Note that the name of the item must be unique within the collection and its respective type
        /// </summary>
        /// <param name="collectionId">The collection to create the folder in</param>
        /// <param name="request">The details of the collection item to create</param>
        /// <returns>The newly created collection item</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     POST /collections/123/items 
        ///     {
        ///         "name": "my secret folder",
        ///         "type": "Folder",
        ///         "description": "please don't tell anyone!!"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">The item was successfully created</response>
        /// <response code="400">An item with the given name and type already exists within the collection</response>
        /// <response code="404">The collection was not found</response>
        [HttpPost("{collectionId}/items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionItemResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateItem(int collectionId, CollectionItemCreateRequest request)
        {
            try
            {
                var collectionItem = await _collectionService.CreateItem(collectionId, request);
                return Created($"/api/collections/{collectionId}/items/{collectionItem.Id}", collectionItem);
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Retrieves all items within a collection
        /// </summary>
        /// <param name="collectionId">The collection to get the items from</param>
        /// <returns>A list of items contained within the collection</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     GET /collections/123/items
        ///      
        /// </remarks>
        /// <response code="200">The items were retrieved successfully</response>
        /// <response code="404">The collection was not found</response>
        [HttpGet("{collectionId}/items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionItemResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetItems(int collectionId)
        {
            try
            {
                return Ok(await _collectionService.GetItems(collectionId));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Retrieves an item within a collection
        /// </summary>
        /// <param name="collectionId">The collection to get the item from</param>
        /// <param name="collectionItemId">The id of the item to be retrieved</param>
        /// <returns>The item associated with the given id</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     GET /collections/123/items/11
        /// 
        /// </remarks>
        /// <response code="200">The item was retrieved successfully</response>
        /// <response code="404">Either the collection or item was not found</response>
        [HttpGet("{collectionId}/items/{collectionItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionItemResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetItem(int collectionId, int collectionItemId)
        {
            try
            {
                return Ok(await _collectionService.GetItem(collectionId, collectionItemId));
            }
            catch (YnventoryException ex) 
            { 
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Updates an existing item within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the item</param>
        /// <param name="collectionItemId">The item id to update</param>
        /// <param name="request">The new values</param>
        /// <returns>The updated item</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     PUT /collections/123/items/11
        ///     {
        ///         "id": 11,
        ///         "name": "my even more secret folder",
        ///         "description": "now even more secretive"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">The item was updated successfully</response>
        /// <response code="400">The request is invalid</response>
        /// <response code="404">Either the collection or item was not found</response>
        [HttpPut("{collectionId}/items/{collectionItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionItemResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateItem(int collectionId, int collectionItemId, CollectionItemUpdateRequest request)
        {
            var response = CheckIdMismatch(collectionItemId, request.Id);
            if (response is not null)
            {
                return response;
            }

            try
            {
                return Ok(await _collectionService.UpdateItem(collectionId, request));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Deletes an item within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the item</param>
        /// <param name="collectionItemId">The item id to delete</param>
        /// <returns></returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     DELETE /collections/123/items/11
        /// 
        /// </remarks>
        /// <response code="200">The item was successfully deleted</response>
        /// <response code="404">Either the collection or item was not found</response>
        [HttpDelete("{collectionId}/items/{collectionItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteItem(int collectionId, int collectionItemId)
        {
            try
            {
                await _collectionService.DeleteItem(collectionId, collectionItemId);
                return Ok();
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Adds a card to an item within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the item</param>
        /// <param name="collectionItemId">The item to add the card to</param>
        /// <param name="request">The details of the card to add</param>
        /// <returns>The newly created card</returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     POST /collections/123/items/11/cards
        ///     {
        ///         "cardMetadataId": "f295b713-1d6a-43fd-910d-fb35414bf58a",
        ///         "quantity": 2,
        ///         "cardFinish": "NonFoil"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">The card was successfully created</response>
        /// <response code="400">The cardMetadataId does not exist</response>
        /// <response code="404">Either the collection or item was not found</response>
        /// <response code="502">An error occured calling the scryfall API</response>
        [HttpPost("{collectionId}/items/{collectionItemId}/cards")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status502BadGateway, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateCard(int collectionId, int collectionItemId, CardCreateRequest request)
        {
            try
            {
                var card = await _collectionService.CreateCard(collectionId, collectionItemId, request);
                return Created($"/api/collections/{collectionId}/items/{collectionItemId}/cards/{card.Id}", card);
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Retrieves a list of cards of an item within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the item</param>
        /// <param name="collectionItemId">The item with the cards</param>
        /// <returns>A list of cards contained in the item</returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     GET /collections/123/items/11/cards
        /// 
        /// </remarks>
        /// <response code="200">The list was successfully retrieved</response>
        /// <response code="404">Either the collection or item was not found</response>
        [HttpGet("{collectionId}/items/{collectionItemId}/cards")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetCards(int collectionId, int collectionItemId)
        {
            try
            {
                return Ok(await _collectionService.GetCards(collectionId, collectionItemId));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Retrieves a card of an item within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the item</param>
        /// <param name="collectionItemId">The item with the card</param>
        /// <param name="cardId">The card to retrieve</param>
        /// <returns>The card associated with the id</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     GET /collections/123/items/11/cards/21
        ///     
        /// </remarks>
        /// <response code="200">The card was found</response>
        /// <response code="404">Either the collection, item or card was not found</response>
        [HttpGet("{collectionId}/items/{collectionItemId}/cards/{cardId}")]
        [ActionName("GetCard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetCard(int collectionId, int collectionItemId, int cardId)
        {
            try
            {
                return Ok(await _collectionService.GetCard(collectionId, collectionItemId, cardId));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Updates an existing card of an item within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the item</param>
        /// <param name="collectionItemId">The item with the card</param>
        /// <param name="cardId">The card id to update</param>
        /// <param name="request">The new values</param>
        /// <returns>The updated card</returns>
        /// <remarks>
        /// Example request:    
        /// 
        ///     PUT /collections/123/items/11/cards/21
        ///     {
        ///         "id": 21,
        ///         "cardMetadataId": "f295b713-1d6a-43fd-910d-fb35414bf58a"
        ///         "quantity": 1,
        ///         "cardFinish": 1
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">The card was successfully updated</response>
        /// <response code="400">The request is invalid</response>
        /// <response code="404">Either the collection, item or card was not found</response>
        /// <response code="502">An error occured calling the scryfall API</response>
        [HttpPut("{collectionId}/items/{collectionItemId}/cards/{cardId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status502BadGateway, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateCard(int collectionId, int collectionItemId, int cardId, CardUpdateRequest request)
        {
            var response = CheckIdMismatch(cardId, request.Id);
            if (response is not null)
            {
                return response;
            }

            try
            {
                return Ok(await _collectionService.UpdateCard(collectionId, collectionItemId, request));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Deletes a card of an item within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the item</param>
        /// <param name="collectionItemId">The item with the card</param>
        /// <param name="cardId">The card to delete</param>
        /// <returns></returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     DELETE /collections/123/items/11/cards/21
        /// 
        /// </remarks>
        /// <response code="200">The card was successfully deleted</response>
        /// <response code="404">Either the collection, item or card was not found</response>
        [HttpDelete("{collectionId}/items/{collectionItemId}/cards/{cardId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteCard(int collectionId, int collectionItemId, int cardId)
        {
            try
            {
                await _collectionService.DeleteCard(collectionId, collectionItemId, cardId);
                return Ok();
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Moves a card from one collection item to another
        /// </summary>
        /// <param name="collectionId">The collection with the item</param>
        /// <param name="collectionItemId">The item with the card</param>
        /// <param name="cardId">The card to move</param>
        /// <param name="request">The details of the target</param>
        /// <returns>The moved card</returns>
        /// <remarks>
        /// Example request: 
        /// 
        ///     POST /collections/123/items/11/cards/21/move
        ///     {
        ///         "targetCollectionId": 321,
        ///         "targetCollectionItemId": 21,
        ///         "quantity": 2
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">The card was successfully moved</response>
        /// <response code="400">The request is invalid</response>
        /// <response code="404">One of the respective items was not found</response>
        [HttpPost("{collectionId}/items/{collectionItemId}/cards/{cardId}/move")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> MoveCard(int collectionId, int collectionItemId, int cardId, MoveCardRequest request)
        {
            try
            {
                return Ok(await _collectionService.MoveCard(collectionId, collectionItemId, cardId, request));
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
