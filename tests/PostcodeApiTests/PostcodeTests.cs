using MarkEmbling.PostcodesIO;
using MarkEmbling.PostcodesIO.Results;
using Moq;
using PostcodeApi;

namespace PostcodeApiTests
{
    [TestClass]
    public class PostcodeTests
    {
        private Mock<IPostcodesIOClient> _clientMock;
        private PostcodeService _postcodeService;

        public PostcodeTests()
        {
            _clientMock = new Mock<IPostcodesIOClient>();
            _postcodeService = new PostcodeService(_clientMock.Object);
        }

        [TestMethod]
        public async Task Postcode_lookup_test()
        {
            // Arrange
            string postcode = "AB12 3CD";
            PostcodeResult expected = new PostcodeResult();

            _clientMock.Setup(c => c.LookupAsync(postcode)).ReturnsAsync(expected);

            // Act
            var result = await _postcodeService.LookupAsync(postcode);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task Postcode_Autocomplete_test()
        {
            // Arrange
            string partialPostcode = "AB12";
            var expected = new List<string>(){"AB12 C34", "AB12 D34"};

            _clientMock.Setup(c => c.AutocompleteAsync(partialPostcode, null)).ReturnsAsync(expected);

            // Act
            var result = await _postcodeService.AutocompleteAsync(partialPostcode);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
