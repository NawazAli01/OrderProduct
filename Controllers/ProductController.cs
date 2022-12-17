using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApis.Domain.Aggregates.ProductAggregate;
using Zero.SeedWorks;
using Microsoft.Identity.Client;
using System.Net.Mail;
using System;
using ProductApis.Domain.Aggregates;
using ProductApis.Models;
using Zero.AspNetCoreServiceProjectExample.Domain;

namespace ProductApis.Controllers
{
    [Route("/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _ProductRepository;

        public ProductController(IRepository<Product> ProductRepository)
        {
            this._ProductRepository = ProductRepository;

        }
        [ProducesResponseType(typeof(ProductResponseModel), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var product = await _ProductRepository.ListAllAsync();
            return Ok(product.Select(m => new ProductResponseModel
            {
                //add whatever u added in orderResponseModel
                productId = m.ProductId,
                name = m.Name,
                price = m.Price,
                quantity = m.Quantity,
            }));
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<IActionResult> PostAsync(ProductRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                bool isValid = true;
                var price = Price.Create(model.price!);
                if (price.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.price), price.Error.Message);
                    isValid = false;
                }
                var quantity = Quantity.Create(model.quantity!);
                if (quantity.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.quantity), quantity.Error.Message);
                    isValid = false;
                }
                if (isValid)
                {
                    var product = new Product(model.OrderId, model.name, price.Value, quantity.Value);
                    await _ProductRepository.AddAsync(product);
                    await _ProductRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                    return Ok(product);
                }
            }
            return ValidationProblem(ModelState);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductResponseModel), StatusCodes.Status200OK)]
        [HttpGet("pro/{Id}")]
        public async Task<IActionResult> GetAsyncById(int Id)
        {

            var product = await _ProductRepository.GetByIdAsync(Id);

            if (product == null) return NotFound();

            return Ok(new ProductResponseModel
            {
                productId = product.ProductId,
                name = product.Name,
                price = product.Price,
                quantity = product.Quantity,
            });
        }
    }
}
