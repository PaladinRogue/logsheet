using Common.Api.Resources;
using Common.Api.Validation.Attributes;

namespace Logsheet.Api.Logsheets
{
    public class CreateLogsheetResource : IResource
    {
        [Required]
        public string CharacterName { get; set; }
    }
}