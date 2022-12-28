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
        [ProducesDefaultResponseType(typeof(CollectionResponse))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> CreateCollection(CollectionCreateRequest request)
        {
            try
            {
                var collection = await _collectionService.CreateCollection(request);
                return CreatedAtAction("GetCollection", new { id = collection.Id }, collection);
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
        [ProducesDefaultResponseType(typeof(CollectionResponse))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
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
        [ProducesDefaultResponseType(typeof(CollectionResponse[]))]
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
        [ProducesDefaultResponseType(typeof(CollectionResponse))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
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
        [ProducesDefaultResponseType(typeof(void))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
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
        /// Creates a new folder within a collection. Note that the name of the folder must be unique within the collection
        /// </summary>
        /// <param name="collectionId">The collection to create the folder in</param>
        /// <param name="request">The details of the folder to create</param>
        /// <returns>The newly created folder</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     POST /collections/123/folders 
        ///     {
        ///         "name": "my secret folder",
        ///         "description": "please don't tell anyone!!"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">The folder was successfully created</response>
        /// <response code="400">A folder with the given name already exists within the collection</response>
        /// <response code="404">The collection was not found</response>
        [HttpPost("{collectionId}/folders")]
        [ProducesDefaultResponseType(typeof(CollectionFolderResponse))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> CreateFolder(int collectionId, CollectionFolderCreateRequest request)
        {
            try
            {
                var folder = await _collectionService.CreateFolder(collectionId, request);
                return CreatedAtAction("GetFolder", new { collectionId, folderId = folder.Id }, folder);
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Retrieves all folders within a collection
        /// </summary>
        /// <param name="collectionId">The collection to get the folders from</param>
        /// <returns>A list of folders contained within the collection</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     GET /collections/123/folders
        /// 
        /// </remarks>
        /// <response code="200">The folders were retrieved successfully</response>
        /// <response code="404">The collection was not found</response>
        [HttpGet("{collectionId}/folders")]
        [ProducesDefaultResponseType(typeof(CollectionFolderResponse[]))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> GetFolders(int collectionId)
        {
            try
            {
                return Ok(await _collectionService.GetFolders(collectionId));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Retrieves a folder within a collection
        /// </summary>
        /// <param name="collectionId">The collection to get the folder from</param>
        /// <param name="folderId">The id of the folder to be retrieved</param>
        /// <returns>The folder associated with the given id</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     GET /collections/123/folders/11
        /// 
        /// </remarks>
        /// <response code="200">The folder was retrieved successfully</response>
        /// <response code="404">Either the collection or folder was not found</response>
        [HttpGet("{collectionId}/folders/{folderId}")]
        [ProducesDefaultResponseType(typeof(CollectionFolderResponse))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> GetFolder(int collectionId, int folderId)
        {
            try
            {
                return Ok(await _collectionService.GetFolder(collectionId, folderId));
            }
            catch (YnventoryException ex) 
            { 
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Updates an existing folder within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the folder</param>
        /// <param name="folderId">The folder id to update</param>
        /// <param name="request">The new values</param>
        /// <returns>The updated folder</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     PUT /collections/123/folders/11
        ///     {
        ///         "id": 11,
        ///         "name": "my even more secret folder",
        ///         "description": "now even more secretive"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">The folder was updated successfully</response>
        /// <response code="400">The request is invalid</response>
        /// <response code="404">Either the collection or folder was not found</response>
        [HttpPut("{collectionId}/folders/{folderId}")]
        [ProducesDefaultResponseType(typeof(CollectionFolderResponse))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> GetFolder(int collectionId, int folderId, CollectionFolderUpdateRequest request)
        {
            var response = CheckIdMismatch(folderId, request.Id);
            if (response is not null)
            {
                return response;
            }

            try
            {
                return Ok(await _collectionService.UpdateFolder(collectionId, request));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Deletes a folder within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the folder</param>
        /// <param name="folderId">The folder id to delete</param>
        /// <returns></returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     DELETE /collections/123/folders/11
        /// 
        /// </remarks>
        /// <response code="200">The folder was successfully deleted</response>
        /// <response code="404">Either the collection or folder was not found</response>
        [HttpDelete("{collectionId}/folders/{folderId}")]
        [ProducesDefaultResponseType(typeof(void))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteFolder(int collectionId, int folderId)
        {
            try
            {
                await _collectionService.DeleteFolder(collectionId, folderId);
                return Ok();
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Adds a card to a folder within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the folder</param>
        /// <param name="folderId">The folder to add the card to</param>
        /// <param name="request">The details of the card to add</param>
        /// <returns>The newly created card</returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     POST /collections/123/folders/11/cards
        ///     {
        ///         "cardMetadataId": "f295b713-1d6a-43fd-910d-fb35414bf58a",
        ///         "quantity": 2,
        ///         "cardFinish": 0
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">The card was successfully created</response>
        /// <response code="400">The cardMetadataId does not exist</response>
        /// <response code="404">Either the collection or folder was not found</response>
        /// <response code="502">An error occured calling the scryfall API</response>
        [HttpPost("{collectionId}/folders/{folderId}/cards")]
        [ProducesDefaultResponseType(typeof(CollectionFolderCardResponse))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> CreateCard(int collectionId, int folderId, CollectionFolderCardCreateRequest request)
        {
            try
            {
                var card = await _collectionService.CreateCard(collectionId, folderId, request);
                return CreatedAtAction("GetCard", new { collectionId, folderId, cardId = card.Id });
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Retrieves a list of cards of a folder within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the folder</param>
        /// <param name="folderId">The folder with the cards</param>
        /// <returns>A list of cards contained in the folder</returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     GET /collections/123/folders/11/cards
        /// 
        /// </remarks>
        /// <response code="200">The list was successfully retrieved</response>
        /// <response code="404">Either the collection or folder was not found</response>
        [HttpGet("{collectionId}/folders/{folderId}/cards")]
        [ProducesDefaultResponseType(typeof(CollectionFolderCardResponse[]))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> GetCards(int collectionId, int folderId)
        {
            try
            {
                return Ok(await _collectionService.GetCards(collectionId, folderId));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Retrieves a card of a folder within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the folder</param>
        /// <param name="folderId">The folder with the card</param>
        /// <param name="cardId">The card to retrieve</param>
        /// <returns>The card associated with the id</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     GET /collections/123/folders/11/cards/21
        ///     
        /// </remarks>
        /// <response code="200">The card was found</response>
        /// <response code="404">Either the collection, folder or card was not found</response>
        [HttpGet("{collectionId}/folders/{folderId}/cards/{cardId}")]
        [ProducesDefaultResponseType(typeof(CollectionFolderCardResponse))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> GetCard(int collectionId, int folderId, int cardId)
        {
            try
            {
                return Ok(await _collectionService.GetCard(collectionId, folderId, cardId));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Updates an existing card of a folder within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the folder</param>
        /// <param name="folderId">The folder with the card</param>
        /// <param name="cardId">The card id to update</param>
        /// <param name="request">The new values</param>
        /// <returns>The updated card</returns>
        /// <remarks>
        /// Example request:    
        /// 
        ///     PUT /collections/123/folders/11/cards/21
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
        /// <response code="404">Either the collection, folder or card was not found</response>
        /// <response code="502">An error occured calling the scryfall API</response>
        [HttpPut("{collectionId}/folders/{folderId}/cards/{cardId}")]
        [ProducesDefaultResponseType(typeof(CollectionFolderCardResponse))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateCard(int collectionId, int folderId, int cardId, CollectionFolderCardUpdateRequest request)
        {
            var response = CheckIdMismatch(cardId, request.Id);
            if (response is not null)
            {
                return response;
            }

            try
            {
                return Ok(await _collectionService.UpdateCard(collectionId, folderId, request));
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        /// <summary>
        /// Deletes a card of a folder within a collection
        /// </summary>
        /// <param name="collectionId">The collection with the folder</param>
        /// <param name="folderId">The folder with the card</param>
        /// <param name="cardId">The card to delete</param>
        /// <returns></returns>
        /// <remarks>
        /// Example request:
        ///     
        ///     DELETE /collections/123/folders/11/cards/21
        /// 
        /// </remarks>
        /// <response code="200">The card was successfully deleted</response>
        /// <response code="404">Either the collection, folder or card was not found</response>
        [HttpDelete("{collectionId}/folders/{folderId}/cards/{cardId}")]
        [ProducesDefaultResponseType(typeof(void))]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteCard(int collectionId, int folderId, int cardId)
        {
            try
            {
                await _collectionService.DeleteCard(collectionId, folderId, cardId);
                return Ok();
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }

        private static IActionResult? CheckIdMismatch(int expected, int actual)
        {
            if (!actual.Equals(actual))
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
