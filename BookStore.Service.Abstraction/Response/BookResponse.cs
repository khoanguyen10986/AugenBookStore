using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Service.Abstraction
{
    public class BookResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SmallThumbnail { get; set; }
        public string Thumbnail { get; set; }
        public string Author { get; set; }
        public string PublishDate { get; set; }
    }
}
