using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCrud.Models
{
    public class ViewClass
    {
        public long id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Phone_number { get; set; }
        public Nullable<long> Appartment_Number { get; set; }
        public string Street_Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }
}