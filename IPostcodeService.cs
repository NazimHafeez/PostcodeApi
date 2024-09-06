using MarkEmbling.PostcodesIO.Results;
namespace PostcodeApi;

public interface IPostcodeService
{
    Task<PostcodeResult> LookupAsync(string postcode);
    Task<IEnumerable<string>> AutocompleteAsync(string partialPostcode);
}