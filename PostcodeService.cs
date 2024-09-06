using MarkEmbling.PostcodesIO;
using MarkEmbling.PostcodesIO.Results;
namespace PostcodeApi;

public class PostcodeService : IPostcodeService
{
    private readonly IPostcodesIOClient _client;

    public PostcodeService(IPostcodesIOClient client)
    {
        _client = client;
    }

    public async Task<PostcodeResult> LookupAsync(string postcode)
    {
        return await _client.LookupAsync(postcode);
    }

    public async Task<IEnumerable<string>> AutocompleteAsync(string partialPostcode)
    {
        return await _client.AutocompleteAsync(partialPostcode);
    }
}