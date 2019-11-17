using BookStore.Service.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.WebApi.Models
{
    public class BookModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string PublishDate { get; set; }
    }

    public class BookListModel
    {
        public class BookListDetailModel : BookModel
        {
            public string SmallThumbnail { get; set; }
        }

        public List<BookListDetailModel> Books { get; set; }

        public List<BookListDetailModel> TransformToModel(IEnumerable<BookResponse> bookResponses)
        {
            var books = bookResponses.Select(b => new BookListDetailModel
            {
                Id = b?.Id,
                Title = b?.Title,
                Description = b?.Description,
                Author = b?.Author,
                PublishDate = b?.PublishDate,
                SmallThumbnail = b?.SmallThumbnail
            }).ToList();

            return books;
        }
    }
}