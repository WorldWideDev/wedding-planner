using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class NewWeddingForm
    {
        [Display(Name="Wedder One Name")]
        [Required(ErrorMessage="Field is required")]
        [MinLength(3, ErrorMessage="Name fields must be at least 3 characters")]
        public string WedderOne { get; set; }

        [Display(Name="Wedder Two Name")]
        [Required(ErrorMessage="Field is required")]
        [MinLength(3, ErrorMessage="Name fields must be at least 3 characters")]
        public string WedderTwo { get; set; }

        [Display(Name = "Wedding Date")]
        // [DataType(DataType.DateTime)]
        [FutureDateAttribute(ErrorMessage="Date must be in the future")]
        public DateTime Date { get; set; }

        [DisplayAttribute(Name="Street")]
        [RequiredAttribute(ErrorMessage="Field is required")]
        public string Street { get; set; }

        [DisplayAttribute(Name="City")]
        [RequiredAttribute(ErrorMessage="Field is required")]
        public string City { get; set; }

        [DisplayAttribute(Name="State")]
        [RequiredAttribute(ErrorMessage="Field is required")]
        public string State { get; set; }

        [DisplayAttribute(Name="Zip")]
        [RequiredAttribute(ErrorMessage="Field is required")]
        public int Zip { get; set; }

    }

    public class ResponseForm
    {
        public int UserId { get; set; }
        public int WeddingId { get; set; }
        public string Result { get; set; }
    }
}