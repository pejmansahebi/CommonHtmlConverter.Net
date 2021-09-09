using System;
using System.IO;
using System.Linq;
using System.Reflection;
using EasyHtmlConverter.Common.Converters;
using Xunit;

namespace EasyHtmlConverter.Tests
{
    public class HtmlRendererImageTests
    {
        private const string HtmlStr = "<html>" +
                                       "<body>" +
                                       "<h1 class='must-be-image'>pejman</h1>" +
                                       "</body>" +
                                       "</html>";

        [Fact]
        public void HtmlRendererFile_Should_ConvertElementOfHtml_To_ImageByteArray()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using var htmlRenderer = new HtmlRenderer(HtmlStr, path);
            var image = htmlRenderer.ConvertElementToImage("//*[contains(@class,'must-be-image')]");
            Assert.True(image is { Length: > 0 });
        }

        [Fact]
        public void HtmlRendererFile_Should_ConvertElementsOfHtml_To_ImageByteArray()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using var htmlRenderer = new HtmlRenderer(HtmlStr, path);
            var images = htmlRenderer.ConvertElementsToImages("//*[contains(@class,'must-be-image')]")
                .ToList();
            Assert.True(images.Any() && images.All(p => p is { Length: > 0 }));
        }

        [Fact]
        public void HtmlRendererUri_Should_ConvertElementOfHtml_To_ImageByteArray()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using var htmlRenderer = new HtmlRenderer(new Uri("https://www.google.com/"), path);
            byte[] image = htmlRenderer.ConvertElementToImage(xpath:"//body");
            Assert.True(image is { Length: > 0 });
        }

        [Fact]
        public void HtmlRendererUri_Should_ConvertElementsOfHtml_To_ImageByteArray()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using var htmlRenderer = new HtmlRenderer(new Uri("https://www.google.com/"), path);
            var images = htmlRenderer.ConvertElementsToImages(xpath:"//input[@type='text']")
                .ToList();
            Assert.True(images.Any() && images.All(p => p is { Length: > 0 }));
        }
    }
}