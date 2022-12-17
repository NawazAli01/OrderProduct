using Microsoft.AspNetCore.Mvc;
using Zero.SeedWorks;
using ProductApis.Domain.Aggregates.CustomerAggregate;
using System;
using ProductApis.Domain.Aggregates;
using ProductApis.Domain.Specifications;
using ProductApis.Models;
using ProductApis.Domain.Aggregates.CartAggregate;
using ProductApis.Domain.Aggregates.ProductAggregate;
using Zero.SharedKernel.Types.Result;
using Zero.AspNetCoreServiceProjectExample.Domain;

namespace ProductApis.Controllers
{
    [Route("/cart")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly IRepository<Cart> _cartRepository;
        public CartController(IRepository<Cart> cartRepository)
        {
            this._cartRepository = cartRepository;
        }


        [HttpGet("/api/carts")]
        public async Task<IActionResult> GetAll()
        {
            var carts = await _cartRepository.ListAllAsync();

            return Ok(carts.Select(m => new CartResponseModel
            {
                // left side ones should be similar to response model
                customerId = m.CustomerId,
                productId = m.ProductId,
                name = m.Name,
                quantity = m.Quantity.Value,
                price = m.Price.Value,
            }));
        }



        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<IActionResult> PostAsync(CartRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                bool isValid = true;
                var price = Price.Create(model.Price!);
                if (price.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Price), price.Error.Message);
                    isValid = false;
                }
                var quantity = Quantity.Create(model.Quantity!);
                if (quantity.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Quantity), quantity.Error.Message);
                    isValid = false;
                }
                if (isValid)
                {
                    var cart = new Cart(model.CustomerID, model.ProductID, model.Name, quantity.Value, price.Value);
                    if (cart == null) return NotFound("Either Customer Id or product id does not exist");
                    await _cartRepository.AddAsync(cart);
                    await _cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                    return Ok(cart);
                }
            }
            return ValidationProblem(ModelState);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductResponseModel), StatusCodes.Status200OK)]
        [HttpGet("cart/{Id}")]
        public async Task<IActionResult> GetAsyncById(int Id)
        {

            var cart = await _cartRepository.GetByIdAsync(Id);

            if (cart == null) return NotFound();

            return Ok(new CartResponseModel
            {
                customerId = cart.CustomerId,
                productId = cart.ProductId,
                name = cart.Name,
                quantity = cart.Quantity.Value,
                price = cart.Price.Value,
            });
        }


/*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int Id, CartRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var cart = await _cartRepository.GetByIdAsync(Id);

                if (cart == null || cart.IsDeleted) return NotFound();

                bool isValid = true;

                var quantity = Quantity.Create(model.Quantity);
                if (quantity.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Quantity), quantity.Error.Message);
                    isValid = false;

                }

                var price = Price.Create(model.Price);
                if (price.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Price), price.Error.Message);
                    isValid = false;
                }

                if (isValid)
                {
                    var result = cart.Update(model.Name, quantity.Value, model.Description, price.Value); 

                    if (result.IsSuccess)
                    {
                        await _cartRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                        return NoContent();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Error.Message);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }
*/

    }
}
