using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Common;
using BookStore.Service.Abstraction;
using BookStore.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService<BookResponse> _bookService;
        private readonly IDeliveryServiceGenerator _deliveryServiceGenerator;

        public BookController(IBookService<BookResponse> bookService, IDeliveryServiceGenerator deliveryServiceGenerator)
        {
            _bookService = bookService;
            _deliveryServiceGenerator = deliveryServiceGenerator;
        }

        [HttpGet("filter/{searchTerm}")]
        public async Task<IActionResult> Filter(string searchTerm)
        {
            IEnumerable<BookResponse> books = await _bookService.SearchBooksAsync(searchTerm);
            List<BookListModel.BookListDetailModel> bookModels = new BookListModel().TransformToModel(books);
            return Ok(bookModels);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            BookResponse book = await _bookService.GetBookDetailAsync(id);
            BookDetailModel bookDetailModel = new BookDetailModel().TransformToModel(book);
            return Ok(bookDetailModel);
        }

        [HttpPost("buybook")]
        public IActionResult BuyBook([FromBody] BuyBookModel model)
        {
            double.TryParse(model.DeliveryCost, out double cost);
            var serviceType = (DeliveryType)Enum.Parse(typeof(DeliveryType), model.DeliveryService);
            IDelivery delivery = _deliveryServiceGenerator.ExecuteCreation(serviceType, cost);
            var orderInfo = delivery.GetInfo();
            return Ok(orderInfo);
        }
    }
}