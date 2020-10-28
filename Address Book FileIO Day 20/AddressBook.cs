using Address_Book_Problem_Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;

namespace AddressBook
{
    public class AddressBook
    {
        public Dictionary<string, ContactPersonInformation> addressBookMapper = new Dictionary<string, ContactPersonInformation>();
        public Dictionary<string, List<ContactDetails>> cityDetailsDictionary = new Dictionary<string, List<ContactDetails>>();
        public Dictionary<string, List<ContactDetails>> stateDetailsDictionary = new Dictionary<string, List<ContactDetails>>();
        public HashSet<string> cityList = new HashSet<string>();
        public HashSet<string> stateList = new HashSet<string>();
        /// <summary>
        /// Adding new Address Book
        /// </summary>
        public void AddAdressBook()
        {
            Console.WriteLine("\nEnter Name for the New Address Book");
            string name = Console.ReadLine();
            //checking address book, if name entered is existing
            if (addressBookMapper.ContainsKey(name))
            {
                Console.WriteLine("Address Book Already exist with this name");
            }
            else
            {
                //Adding the name and contact person information class in dictionary
                ContactPersonInformation contactPersonInformation = new ContactPersonInformation();
                addressBookMapper.Add(name, contactPersonInformation);
            }
        }

        /// <summary>
        /// Adding contacts in Address Book
        /// </summary>
        public void AddContactsInAddressBook()
        {
            Console.WriteLine("\nEnter Name of address book to add new contact");
            string name = Console.ReadLine();
            //checking, if the name is existing in the dictionary
            if (!addressBookMapper.ContainsKey(name))
            {
                Console.WriteLine("No address book found with this name");
                Console.WriteLine("Please Enter Valid Name from following names:");
                //displaying the available dictionary names
                foreach (KeyValuePair<string, ContactPersonInformation> tempPair in addressBookMapper)
                {
                    Console.WriteLine(tempPair.Key);
                }
            }
            else
            {
                //calling the methods to add contact details in dictionary and displaying it
                ContactPersonInformation contactPersonInformation = addressBookMapper[name];
                contactPersonInformation.AddingContactDetails();
                contactPersonInformation.DisplayContactDetails();
            }
        }
        /// <summary>
        /// Editing contact person details from address book
        /// </summary>
        public void EditDetailsOfAddressBook()
        {
            Console.WriteLine("\nEnter Name of address book to modify contact details");
            string name = Console.ReadLine();
            //checking if the name exist in dictionary
            if (!addressBookMapper.ContainsKey(name))
            {
                Console.WriteLine("No address book found with this name");
                Console.WriteLine("Please Enter Valid Name from following names:");
                //displaying the names that are available in dictionary
                foreach (KeyValuePair<string, ContactPersonInformation> tempPair in addressBookMapper)
                {
                    Console.WriteLine(tempPair.Key);
                }
            }
            else
            {
                // calling the methods from contact person information to display and edit contact person details in address book 
                ContactPersonInformation contactPersonInformation = addressBookMapper[name];
                contactPersonInformation.EditingContactDetails();
                contactPersonInformation.DisplayContactDetails();
            }
        }
        /// <summary>
        /// Deleting contact person details from address book
        /// </summary>

        public void DeleteContactsOfAddressBook()
        {
            Console.WriteLine("\nEnter Name of address book to delete contact details");
            string name = Console.ReadLine();
            if (!addressBookMapper.ContainsKey(name))
            {
                Console.WriteLine("No address book found with this name");
                Console.WriteLine("Please Enter Valid Name from following names:");
                foreach (KeyValuePair<string, ContactPersonInformation> tempPair in addressBookMapper)
                {
                    Console.WriteLine(tempPair.Key);
                }
            }
            else
            {
                //Calling method to delete contact details from address book
                ContactPersonInformation contactPersonInformation = addressBookMapper[name];
                contactPersonInformation.DeletingContactDetails();
                contactPersonInformation.DisplayContactDetails();
            }
        }
        /// <summary>
        /// Displaying available adddress books and their contact details
        /// </summary>
        public void DisplayingAddressBooks()
        {
            Console.WriteLine("***********************************************************");
            foreach(KeyValuePair<string,ContactPersonInformation> dictionaryPair in addressBookMapper)
            {
                Console.WriteLine("the name of address book is " + dictionaryPair.Key);
                ContactPersonInformation contactPersonInformation = dictionaryPair.Value;
                contactPersonInformation.DisplayContactDetails();
            }
        }
        /// <summary>
        /// Writing data into text file for each address book
        /// </summary>
        public void WritingAddressBookInTextFile()
        {
            //calling of addressbooks using foreach loop
            foreach (KeyValuePair<string, ContactPersonInformation> dictionaryPair in addressBookMapper)
            { 
                ContactPersonInformation contactPersonInformation = dictionaryPair.Value;
                contactPersonInformation.AddingContactDetailsinTextFile(dictionaryPair.Key);
            }
            Console.WriteLine("Data Appended in text file");
        }
        /// <summary>
        /// Reading all the data from text file created for writing
        /// </summary>
        public void ReadingContactDetailsFromTextFile()
        {
            //path from which data is read
            string path = @"C:\Users\vishu\source\repos\Address Book FileIO Day 20\Address Book FileIO Day 20\DataFile.txt";
            Console.WriteLine("Showing all the data Writtern into the file by calling Stream Reader");
            //if file exists, streamreader function from system.IO is called
            //sr.ReadLine() will read each line from the text  file and display to console until line is null.
            if(File.Exists(path))
            {
                using(StreamReader sr= File.OpenText(path))
                {
                    string line = "";
                    while((line= sr.ReadLine())!=null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
        }
        /// <summary>
        /// writing the contact details of address book into csv file
        /// calling method in contact person information which writes contact list of each address book in seperate csv files
        /// </summary>
        public void WritingAddressBookInCsvFile()
        {
            //calling of addressbooks using foreach loop
            foreach (KeyValuePair<string, ContactPersonInformation> dictionaryPair in addressBookMapper)
            {
                ContactPersonInformation contactPersonInformation = dictionaryPair.Value;
                contactPersonInformation.AddingContactDetailsInCsvFile(dictionaryPair.Key);
            }
            Console.WriteLine("Data Appended in csv file successfully");
        }
        /// <summary>
        /// reading the contact details from csv file
        /// </summary>
        public void ReadingContactDetailsFromCSVFile()
        {
            Console.WriteLine("**Reading data from csv file**");
            //iterating over each address book
            foreach (KeyValuePair<string,ContactPersonInformation> addressBookName in addressBookMapper)
            {
                //defining path for each address book
                string path = @"C:\Users\vishu\source\repos\Address Book FileIO Day 20\Address Book FileIO Day 20\" + addressBookName.Key + ".csv";
                if (File.Exists(path))
                {
                    Console.WriteLine("The Name of the address book is:" + addressBookName.Key);
                    //reading the path of file
                    StreamReader reader = new StreamReader(path);
                    //instatiating csvreader object to read csv file
                    var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                    //getting the list of contact details from file using GetRecords Method and type casting it to ContactDetails
                    List<ContactDetails> list = csv.GetRecords<ContactDetails>().ToList();
                    foreach (ContactDetails contactPerson in list)
                    {
                        Console.WriteLine($"First Name : {contactPerson.firstName} || Last Name: {contactPerson.lastName} || Address: {contactPerson.address} || City: {contactPerson.city} || State: {contactPerson.state}|| zip: {contactPerson.zip} || Phone No: {contactPerson.phoneNo} || eMail: {contactPerson.eMail}");
                    }
                    //closing the csv reader for garbage collection
                    reader.Close();
                }
            }
        }
        /// <summary>
        /// Removing complete address book
        /// </summary>
        public void DeletingAddressBook()
        {
            Console.WriteLine("\nEnter Name of address book to delete ");
            string name = Console.ReadLine();
            if (!addressBookMapper.ContainsKey(name))
            {
                Console.WriteLine("No address book found with this name");
                Console.WriteLine("Please Enter Valid Name from following names:");
                foreach (KeyValuePair<string, ContactPersonInformation> tempPair in addressBookMapper)
                {
                    Console.WriteLine(tempPair.Key);
                }
            }
            else
            {
                //Removes address book from the Dictionary
                addressBookMapper.Remove(name);
            }
        }
        /// <summary>
        /// Searching by city for address book and contact details
        /// </summary>
        public void SearchingByCity()
        {
            Console.WriteLine("Please enter the city");
            string searchCity = Console.ReadLine();

            //foreach loop to print name of address book and pass address book value to contact person information class
            foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
            {
                Console.WriteLine("Name of the address book: " + keyValuePair.Key);
                ContactPersonInformation contactPersonInformation = keyValuePair.Value;
               
                contactPersonInformation.SearchingContactDetailsByCity(searchCity);
            }
            Console.WriteLine("Do you want to enter city again, press y for yes");
            string checkInput = Console.ReadLine();
            if (checkInput.ToLower() == "y")
            {
                SearchingByCity();
            }

        }
        /// <summary>
        /// Searching by state to get address book and contact details
        /// </summary>
        public void SearchingByState()
        {
            //used to find custom exception, if state do not exist

            Console.WriteLine("Please enter the state");
            string searchState = Console.ReadLine();
            //foreach loop is used to print key for dictionary and pass the values of dictionary to contact person information class
            foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
            {
                Console.WriteLine("Name of the address book: " + keyValuePair.Key);
                ContactPersonInformation contactPersonInformation = keyValuePair.Value;
                contactPersonInformation.SearchingContactDetailsByState(searchState);
            }
            Console.WriteLine("Do you want to enter state again, press y for yes");
            string checkInput = Console.ReadLine();
            if (checkInput.ToLower() == "y")
            {
                //Details of state are entered again.
                SearchingByState();
            }

        }
        /// <summary>
        /// used to display list of city
        /// </summary>
        public void ViewingCityDictionary()
        {
            foreach(KeyValuePair<string,List<ContactDetails>> cityDetails in cityDetailsDictionary)
            {
                Console.WriteLine(cityDetails.Key+":");
                //index is used to maintain count of each city
                int index = 0;
                foreach(ContactDetails contactPerson in cityDetails.Value)
                {
                    Console.WriteLine($"First Name : {contactPerson.firstName} || Last Name: {contactPerson.lastName} || Address: {contactPerson.address} || City: {contactPerson.city} || State: {contactPerson.state}|| zip: {contactPerson.zip} || Phone No: {contactPerson.phoneNo} || eMail: {contactPerson.eMail}");
                    index++;
                }
                //displays name of city and count of contact details
                Console.WriteLine($"Total no of contact details in {cityDetails.Key} are {index}");
                Console.WriteLine("");

            }
        }
        /// <summary>
        /// used to display list of state
        /// </summary>
        public void ViewingStateDictionary()
        {
            foreach (KeyValuePair<string, List<ContactDetails>> stateDetails in stateDetailsDictionary)
            {
                Console.WriteLine(stateDetails.Key);
                //index is used to maintain count of each state
                int index = 0;
                foreach (ContactDetails contactPerson in stateDetails.Value)
                {
                    Console.WriteLine($"First Name : {contactPerson.firstName} || Last Name: {contactPerson.lastName} || Address: {contactPerson.address} || City: {contactPerson.city} || State: {contactPerson.state}|| zip: {contactPerson.zip} || Phone No: {contactPerson.phoneNo} || eMail: {contactPerson.eMail}");
                    index++;

                }
                //displays total count for each state
                Console.WriteLine($"Total no of contact details in {stateDetails.Key} are {index}");
                Console.WriteLine("");
            }
        }
        /// <summary>
        /// Getting city names is used to get all the names of city in all address books
        /// </summary>
        public void GettingCityNames()
        {
            //calling each address book
            foreach(KeyValuePair<string,ContactPersonInformation> keyValuePair in addressBookMapper)
            {
                ContactPersonInformation contactPersonInformation = keyValuePair.Value;
                //calling method Getting CityList from contactperson information
                //getting city list returns a hashset of cities in particular address book
                //the cities are then added in new hashmap called citylist defined in this class
                foreach(string city in contactPersonInformation.GettingCityList())
                {
                    cityList.Add(city);
                }
            }
        }
        /// <summary>
        /// calling method to create a city dictionary
        /// </summary>
        public void CreatingCityDictionary()
        {
            //foreach loop is used to iterate over city names in hashset citylist
            foreach (string cityName in cityList)
            {
                //list is defined of contact details for every new city
                List<ContactDetails> cityDetailsList = new List<ContactDetails>();
                //foreach loop is called to call each address book seperately
                //each address book is passed to a method addding contact details by city in contact person information
                //method returns a list of contact person details for particular city in particular address book
                //foreach loop iterates the method again by passing the same city and city details list, where more values are added for same city
                foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
                {
                    ContactPersonInformation contactPersonInformation = keyValuePair.Value;
                    cityDetailsList= contactPersonInformation.AddingContactDetailsByCity(cityName,cityDetailsList);
                }
                //after iterating over all address books, city and city details list are added in dictionary
                cityDetailsDictionary.Add(cityName, cityDetailsList);
            }
        }
        /// <summary>
        /// Getting city names is used to get all the names of city in all address books
        /// </summary>
        public void GettingStateNames()
        {
            //calling each address book
            foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
            {
                ContactPersonInformation contactPersonInformation = keyValuePair.Value;
                //calling method Getting StateList from contactperson information
                //getting state list returns a hashset of cities in particular address book
                //the cities are then added in new hashmap called statelist defined in this class
                foreach (string state in contactPersonInformation.GettingStateList())
                {
                    stateList.Add(state);
                }
            }
        }
        /// <summary>
        /// calling method to create a state dictionary
        /// </summary>
        public void CreatingStateDictionary()
        {
            //foreach loop is used to iterate over state names in hashset citylist
            foreach (string stateName in stateList)
            {
                //list is defined of contact details for every new city
                List<ContactDetails> stateDetailsList = new List<ContactDetails>();
                //foreach loop is called to call each address book seperately
                //each address book is passed to a method addding contact details by state in contact person information
                //method returns a list of contact person details for particular state in particular address book
                //foreach loop iterates the method again by passing the same state and state details list, where more values are added for same state
                foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
                {
                    ContactPersonInformation contactPersonInformation = keyValuePair.Value;
                    stateDetailsList = contactPersonInformation.AddingContactDetailsByState(stateName, stateDetailsList);
                }
                //after iterating over all address books, city and city details list are added in dictionary
                stateDetailsDictionary.Add(stateName, stateDetailsList);
            }
        }
    }
}
