using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents the DTO class that contains the person details to update
    /// </summary>
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage ="Person Id can't be blank")]
        public Guid PersonId { get; set;  }

        [Required(ErrorMessage = "Person Name can't be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email Name can't be blank")]
        [EmailAddress(ErrorMessage = "Email value should be valid")]
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool? ReceiveNewsLetters { get; set;  }

        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryId { get; set; }
       

        /// <summary>
        /// Converts the current object PersonAddRequest into a new object of Person type
        /// </summary>
        /// <returns>Returns person Object</returns>

        public Person ToPerson()
        {
            return new Person
            {
                PersonId = PersonId,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryId = CountryId,
              
            };
        }


    }
}
