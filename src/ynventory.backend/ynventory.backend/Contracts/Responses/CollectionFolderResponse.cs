﻿namespace Ynventory.Backend.Contracts.Responses
{
    public class CollectionFolderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CardCount { get; set; }
    }
}
