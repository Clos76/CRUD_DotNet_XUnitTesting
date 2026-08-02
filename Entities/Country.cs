namespace Entities
{
    /// <summary>
    /// Domain Model for Country - not as argument or return type. 
    /// </summary>
    public class Country
    {
        public Guid CountryId { get; set;  }
        public string? CountryName { get; set;  }
    }
}
