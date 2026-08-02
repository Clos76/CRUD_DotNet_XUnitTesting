using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents DTO Class that is used as return type of most methods of Person Service
    /// </summary>
    public class PersonResponse
    {
        public Guid PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Country { get; set;  }
        public string? Address { get; set; }
        public bool? ReceiveNewsLetters { get; set; }
        public double? Age { get; set;  }

        /// <summary>
        /// Compares the current object data with the parameter object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True or Fasle, indication whether all person details are matched with the specified parameter object</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person = (PersonResponse)obj; ;
            return PersonId == person.PersonId && PersonName == person.PersonName && Email == person.Email && 
                DateOfBirth == person.DateOfBirth && Gender == person.Gender && CountryId == person.CountryId && Country == person.Country
                && Address == person.Address && ReceiveNewsLetters == person.ReceiveNewsLetters && Age == person.Age;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Person Id: {PersonId}, Person Name = {PersonName}, Person Email = {Email}, Person DOB = {DateOfBirth?.ToString("dd MM yyyy")}," +
                $" Gender: {Gender}, Age: {Age}, Country: {Country}";
        }

        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                PersonId = PersonId,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
                Address = Address,
                CountryId = CountryId,
                ReceiveNewsLetters = ReceiveNewsLetters
            };
        }
    }

    /// <summary>
    /// An extension method to convert an object of Person class into PersonReponse class
    /// 
    /// <param name="person">The Person object to convert</param>
    /// <returns>Returns the converted PersonResponse object</returns>
    /// </summary>
    public static class PersonExtension
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            //person => PersonResponse
            return new PersonResponse()
            {
                PersonId = person.PersonId,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Address = person.Address,
                CountryId = person.CountryId,
                Gender = person.Gender,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now
                - person.DateOfBirth.Value).TotalDays / 365.25) : null
            };
        }
    }

   
}
