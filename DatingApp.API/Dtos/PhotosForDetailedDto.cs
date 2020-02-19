using System;

namespace DatingApp.API.Dtos
{
    public class PhotosForDetailedDto
    {
        // Photo ID
        public int Id { get; set; }
        // URL to access photo
        public string Url { get; set; }
        // Description of the photo
        public string Description { get; set; }
        // Date photo was added
        public DateTime DateAdded { get; set; }
        // Main Profile Page
        public bool IsMain { get; set; }
    }
}