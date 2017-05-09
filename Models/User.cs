using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Response> Responses { get; set; }

    }

    public class Response
    {
        public int Id { get; set; }
        public bool IsGoing { get; set; }
        public virtual int UserId { get; set; }
        public virtual int WeddingId { get; set; }
        public virtual User User { get; set; }
        public virtual Wedding Wedding { get; set; }
    }

    public class Wedding
    {
        public int Id { get; set; }
        public string WedderOne { get; set; }
        public string WedderTwo { get; set; }
        public DateTime Date { get; set; }
        public virtual User Host { get; set; }
        public virtual Address EventLocation { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public int WeddingId { get; set; }
        public virtual Wedding Wedding { get; set; }   
    }

}