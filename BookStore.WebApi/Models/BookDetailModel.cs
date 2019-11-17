using BookStore.Service.Abstraction;

namespace BookStore.WebApi.Models
{
    public class BookDetailModel : BookModel
    {
        public string Thumbnail { get; set; }

        public BookDetailModel TransformToModel(BookResponse bookResponse)
        {
            var bookDetail = new BookDetailModel
            {
                Id = bookResponse?.Id,
                Title = bookResponse?.Title,
                Description = bookResponse?.Description,
                Author = bookResponse?.Author,
                PublishDate = bookResponse?.PublishDate,
                Thumbnail = bookResponse?.Thumbnail
            };
            return bookDetail;
        }
    }
}
