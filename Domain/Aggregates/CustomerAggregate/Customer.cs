using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zero.SeedWorks;
//using ProductApis.Domain.Aggregates.ProductAggregate;
using Zero.AspNetCoreServiceProjectExample.Domain;
using System;
using Zero.SharedKernel.Types.Result;
using ProductApis.Domain.Errors;


namespace ProductApis.Domain.Aggregates.CustomerAggregate
{
    public class Customer : Entity, IAggregateRoot
    {
        public int CustomerId { get; private set; }   
        public Name Name { get; private set; }
        public MobileNumber? MobileNumber { get; private set; }
        public EmailAddress? EmailAddress { get; private set; }
        public bool IsDeleted { get; private set; }
        public Customer(Name name, MobileNumber? mobilenumber, EmailAddress? emailaddress) {

            Name = name ?? throw new ArgumentNullException(nameof(name));
            MobileNumber = mobilenumber;
            EmailAddress = emailaddress;
            IsDeleted = false;
        }
        private Customer()
        {

        }
       
        public Result Update(Name name, MobileNumber? mobileNumber, EmailAddress? emailAddress)
        {
            if (IsDeleted) return Result.Failure(new DeletedCustomerError("Deleted person can not be updated."));

            Name = name;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;

            return Result.Success();
        }

        public void Delete()
        {
            IsDeleted = true;
        }

    }
}
