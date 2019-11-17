using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Service.Abstraction;

namespace BookStore.Service.TestingThirdParty
{
    public class SampleBookService : IBookService<BookResponse>
    {
        public BookResponse GetBookDetail(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BookResponse> GetBookDetailAsync(string id)
        {
            return new BookResponse();
        }

        public IEnumerable<BookResponse> SearchBooks(string searchTerm)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<BookResponse>> SearchBooksAsync(string searchTerm)
        {
            return await Task.FromResult(new List<BookResponse>());
        }
    }
}