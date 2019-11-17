using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Service.Abstraction;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;

namespace BookStore.Service.GoogleBookApi
{
    public class GoogleBookService : IBookService<BookResponse>
    {
        private readonly AuthenticationService _authenticationService;

        public GoogleBookService()
        {
            _authenticationService = new AuthenticationService();
        }

        public BookResponse GetBookDetail(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BookResponse> GetBookDetailAsync(string id)
        {
            BooksService googleApiBooksService = await _authenticationService.Authenticate();

            var book = new BookResponse();
            var bookselve = await googleApiBooksService.Mylibrary.Bookshelves.List().ExecuteAsync();
            if (bookselve.Items == null)
            {
                return await Task.FromResult(book);
            }

            var booksShelfItem = bookselve.Items.First();
            if (booksShelfItem.VolumeCount <= 0)
            {
                return await Task.FromResult(book);
            }

            MylibraryResource.BookshelvesResource.VolumesResource.ListRequest request = googleApiBooksService.Mylibrary.Bookshelves.Volumes.List(booksShelfItem.Id.ToString());
            Volumes inBookshelf = await request.ExecuteAsync();
            if (inBookshelf.Items == null)
            {
                return await Task.FromResult(book);
            }

            Volume bookDetail = inBookshelf.Items.FirstOrDefault(b => b.Id.Equals(id));
            if (bookDetail != null)
            {
                book = new BookResponse
                {
                    Id = bookDetail.Id,
                    Title = bookDetail.VolumeInfo?.Title,
                    Description = bookDetail.VolumeInfo?.Description,
                    Thumbnail = bookDetail?.VolumeInfo?.ImageLinks.Thumbnail,
                    Author = string.Join(",", bookDetail.VolumeInfo?.Authors ?? new List<string>()),
                    PublishDate = bookDetail.VolumeInfo?.PublishedDate
                };
            }

            return await Task.FromResult(book);
        }

        public IEnumerable<BookResponse> SearchBooks(string searchTerm)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<BookResponse>> SearchBooksAsync(string searchTerm)
        {
            BooksService googleApiBooksService = await _authenticationService.Authenticate();

            var books = Enumerable.Empty<BookResponse>();
            var bookselve = await googleApiBooksService.Mylibrary.Bookshelves.List().ExecuteAsync();
            if (bookselve.Items == null)
            {
                return await Task.FromResult(books);
            }

            var booksShelfItem = bookselve.Items.First();
            if (booksShelfItem.VolumeCount <= 0)
            {
                return await Task.FromResult(books);
            }

            MylibraryResource.BookshelvesResource.VolumesResource.ListRequest request = googleApiBooksService.Mylibrary.Bookshelves.Volumes.List(booksShelfItem.Id.ToString());
            Volumes inBookshelf = await request.ExecuteAsync();
            if (inBookshelf.Items == null)
            {
                return await Task.FromResult(books);
            }

            IEnumerable<Volume> volumes = searchTerm == "all" ? inBookshelf.Items : 
                                        inBookshelf.Items.Where(b => b.VolumeInfo.Title.Contains(searchTerm) ||
                                                                b.VolumeInfo.Authors.Contains(searchTerm));

            books = volumes.Select(b => new BookResponse
            {
                Id = b?.Id,
                Title = b?.VolumeInfo?.Title,
                Description = b?.VolumeInfo?.Description,
                SmallThumbnail = b?.VolumeInfo?.ImageLinks.SmallThumbnail,
                Author = string.Join(",", b?.VolumeInfo?.Authors ?? new List<string>()),
                PublishDate = b?.VolumeInfo?.PublishedDate
            });

            return await Task.FromResult(books);
        }
    }
}