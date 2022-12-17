using Microsoft.AspNetCore.Mvc;
using Zero.SeedWorks;
using ProductApis.Domain.Aggregates.CustomerAggregate;
using System;
using ProductApis.Domain.Specifications;
using ProductApis.Models;
using Zero.AspNetCoreServiceProjectExample.Domain;

namespace ProductApis.Controllers
{
    [Route("/customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IRepository<Customer> _customerRepository;
        public CustomerController(IRepository<Customer> customerRepository)
        {
            this._customerRepository = customerRepository;

        }

        [HttpGet("/api/customers")]
        public async Task<IActionResult> GetAllActivePerson()
        {
            var customers = await _customerRepository.ListAsync(new ActiveCustomerSpecification());

            return Ok(customers.Select(m => new CustomerResponseModel
            {
                customerId = m.CustomerId,
                name = m.Name,
                emailAddress = m.EmailAddress?.Value,
                mobileNumber = m.MobileNumber?.Value,
            }));
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostAsync(CustomerRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                bool isValid = true;

                var name = Name.Create(model.Name!);
                if (name.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Name), name.Error.Message);
                    isValid = false;
                }

                var mobilenumber = MobileNumber.Create(model.MobileNumber, true);
                if (mobilenumber.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.MobileNumber), mobilenumber.Error.Message);
                    isValid = false;
                }

                var emailAddress = EmailAddress.Create(model.EmailAddress, true);
                if (emailAddress.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.EmailAddress), emailAddress.Error.Message);
                    isValid = false;
                }

                if (isValid)
                {
                    var customer = new Customer(name.Value, mobilenumber.Value, emailAddress.Value);

                    await _customerRepository.AddAsync(customer);

                    await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                    return Ok(customer);
                }
            }
            return ValidationProblem(ModelState);
        }


        [ProducesResponseType(typeof(CustomerRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, CustomerRequestModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var customer = await _customerRepository.GetByIdAsync(id);

                if (customer == null || customer.IsDeleted) return NotFound();

                bool isValid = true;

                var name = Name.Create(model.Name!);
                if (name.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.Name), name.Error.Message);
                    isValid = false;
                }

                var mobileNumber = MobileNumber.Create(model.MobileNumber, true);
                if (mobileNumber.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.MobileNumber), mobileNumber.Error.Message);
                    isValid = false;

                }

                var emailAddress = EmailAddress.Create(model.EmailAddress, true);
                if (emailAddress.IsFailure)
                {
                    ModelState.AddModelError(nameof(model.EmailAddress), emailAddress.Error.Message);
                    isValid = false;
                }

                if (isValid)
                {
                    var result = customer.Update(name.Value, mobileNumber.Value, emailAddress.Value);

                    if (result.IsSuccess)
                    {
                        await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
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


        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null) return NotFound();

            customer.Delete();
            await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return NoContent();
        }
    }
}




