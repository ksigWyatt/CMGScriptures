using CMGScripturesAPI.Repos.System;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace CMGScripturesAPI.Repos.Tests {
    public class CmgJsonConverterTests {
        [Fact]
        public void ConvertImageResponseToDto_Valid_ReturnsCorrectNumber() {
            int expected = 36;
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string sampleJsonPath = $"{projectDirectory}\\Samples\\CmgGetResponseSample.json";
            string sampleJson = File.ReadAllText(sampleJsonPath);
            var imageDtos = CmgJsonConverter.ConvertImageResponseToDto(sampleJson);
            Assert.Equal(expected, imageDtos.ToList().Count);
        }
    }
}
