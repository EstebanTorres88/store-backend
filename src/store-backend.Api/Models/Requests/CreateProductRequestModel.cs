using System.ComponentModel.DataAnnotations;

public class CreateProductRequestModel
{
    [Required]
    public Guid? ProductResourceId { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }
}
