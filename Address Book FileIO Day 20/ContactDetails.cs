using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    public class ContactDetails
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city{ get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public double phoneNo { get; set; }
        public string eMail { get; set; }

        /// <summary>
        /// Calling Constructor for initializing variables 
        /// variables are called and initialized from contact person information class
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        /// <param name="phoneNo"></param>
        /// <param name="eMail"></param>
        public ContactDetails(string firstName, string lastName, string address, string city, string state, int zip, double phoneNo, string eMail)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phoneNo = phoneNo;
            this.eMail = eMail;
        }
        
    }
}
