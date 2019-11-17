using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Service.Abstraction
{
    public interface IBookService<T> where T : BookResponse
    {
        Task<IEnumerable<BookResponse>> SearchBooksAsync(string searchTerm);
        Task<BookResponse> GetBookDetailAsync(string id);

       IEnumerable<BookResponse> SearchBooks(string searchTerm);
       BookResponse GetBookDetail(string id);
    }
}
