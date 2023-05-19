using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleCode.Data;

[Table("bookmarks", Schema = "bl")]
public class Bookmark
{
    public int Id { get; set; }
    //public string? UserName { get; set; }

    //[StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string? Name { get; set; }

    //[Required]
    //[RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Enter a valid zipcode in 55555 or 55555-5555 format")]
    public string? Url { get; set; }


}
