using System.ComponentModel.DataAnnotations.Schema;

namespace contactsCRUD.Models
{
    public class Contacts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string JobTitle { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
