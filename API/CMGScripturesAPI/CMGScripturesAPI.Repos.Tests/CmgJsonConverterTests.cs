using CMGScripturesAPI.Repos.DTOs;
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
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            string sampleJsonPath = $"{projectDirectory}\\Samples\\CmgGetResponseSample.json";
            string sampleJson = File.ReadAllText(sampleJsonPath);
            var imageDtos = CmgJsonConverter.ExtractImageObjectsFromResponse(sampleJson);
            Assert.Equal(expected, imageDtos.ToList().Count);
        }
        [Fact]
        public void ConvertImageResponseToDto_Valid_FirstResultMatches() {
            CmgImageDto expectedDto = new CmgImageDto() {
                Id = "92e39968-9b3b-c7b9-6e11-704b8b37915c",
                Added = 1569938294,
                Width = 5000,
                Height = 2637,
                Filename = "Stardust Shine HQ 10.jpg",
                Creator = new CreatorDto {
                    Id = "cmg",
                    Name = "Church Motion Graphics",
                    Website = @"https:\/\/www.churchmotiongraphics.com",
                    Thumbnail = @"https:\/\/cmgcreate-1.imgix.net\/producers\/cmg01.jpg?h=160&q=60&s=94c5c37b8fe408d747d2b2927ccb48da"
                }
            };
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            string sampleJsonPath = $"{projectDirectory}\\Samples\\CmgGetResponseSample.json";
            string sampleJson = File.ReadAllText(sampleJsonPath);
            var imageDtos = CmgJsonConverter.ExtractImageObjectsFromResponse(sampleJson);
            var firstImageDto = imageDtos.ToList()[0];
            Assert.Equal(expectedDto.Id, firstImageDto.Id);
            Assert.Equal(expectedDto.Creator.Id, firstImageDto.Creator.Id);
        }
        [Fact]
        public void ConvertImageResponseToDto_Valid_MetaObjectIsCorrect() {
            MetaDto expectedMeta = new MetaDto() {
                RequestTotal = 36,
                RequestStart = 0,
                RequestEnd = 36,
                Total = 3668
            };
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            string sampleJsonPath = $"{projectDirectory}\\Samples\\CmgGetResponseSample.json";
            string sampleJson = File.ReadAllText(sampleJsonPath);

            var metaResult = CmgJsonConverter.ExtractMetaObjectFromResponse(sampleJson);
            Assert.Equal(expectedMeta.Total, metaResult.Total);
        }
    }
}
