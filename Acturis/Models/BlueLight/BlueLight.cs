using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acturis.Models.BlueLight
{
    public class Bluelight
    {
        public string Id { get; set; }
        public Contact Contact { get; set; }
        public SubType SubType { get; set; }
        public Class Class { get; set; }
        public string MembershipNumber { get; set; }
        public CurrentMembership CurrentMembership { get; set; }
        public Grade Grade { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string CmsId { get; set; }
        public List<Products> Products { get; set; }


    }
    public class CurrentMembership
    {
        public string Id { get; set; }
       
    }


    public class Contact
    {
        public string Id { get; set; }

        [JsonProperty("EmployeeReferenceNumber")]
        public string EmployeeReferenceNumber { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress1 { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomePhone { get; set; }

        [JsonProperty("HomeMobile")]
        public string HomeMobile { get; set; }
        [JsonProperty("WorkPhone")]
        public string WorkPhone { get; set; }
        [JsonProperty("WorkMobile")]
        public string WorkMobile { get; set; }
        public Title Title { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address2Postcode { get; set; }

    }

    public class SubType
    {
        public string Label { get; set; }
    }

    public class Class
    {

        public string Name { get; set; }

    }
    public class Grade
    {
        public string Name { get; set; }
    }

    public class Title
    {
        public int Value { get; set; }
        public string Label { get; set; }
    }


    public class PaymentMethod
    {
        public int Value { get; set; }
        public string Label { get; set; }
    }

    public class Invoice
    {
        public string Id { get; set; }

    }

    public class Product
    {

        public string Name { get; set; }

    }

    public class Products
    {

        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
        public double Quantity { get; set; }

    }


}
