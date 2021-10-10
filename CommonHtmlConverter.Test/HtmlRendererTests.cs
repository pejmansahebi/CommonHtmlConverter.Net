using System;
using System.IO;
using CommonHtmlConverter.Test.TestData;
using HtmlConvertor.Converters;
using Xunit;

namespace CommonHtmlConverter.Test
{
    public class HtmlRendererTests
    {
        #region Variables
        private const string HtmlStr = "<html>" +
                                       "<body>" +
                                       "<h1>pejman</h1>" +
                                       "</body>" +
                                       "</html>";

        private const string ExistPath = @"C:\Windows";
        private const string ValidUriStr = "https://www.google.com/";
        #endregion

        #region Tests
        [Theory]
        [ClassData(typeof(CheckStringTestData))]
        public void HtmlRendererFileWithEmptyOrNullOrWhiteSpaceHtmlShouldThrowException(string html)
        {
            ArgumentException exception = Assert.Throws<ArgumentNullException>(() =>
            {
                HtmlRendererCreator(html, @"C:\Drivers");
            });
            Assert.Equal(nameof(html), exception.ParamName);
        }

        [Theory]
        [ClassData(typeof(CheckStringTestData))]
        public void HtmlRendererFileWithEmptyOrNullOrWhiteSpaceChromeDriverPathShouldThrowException(string path)
        {
            ArgumentException exception = Assert.Throws<ArgumentNullException>(() =>
            {
                HtmlRendererCreator(HtmlStr, path);
            });
            Assert.Equal(nameof(path), exception.ParamName);
        }

        [Theory]
        [InlineData("InvalidPath")]
        [InlineData("C://InvalidPath")]
        public void HtmlRendererFile_Should_ThrowException_When_IfPathIsInvalid(string path)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                HtmlRendererCreator(HtmlStr, path);
            });
            Assert.Equal("path doesn't exist in your system or server", exception.Message);
        }

        [Fact]
        public void HtmlRendererFile_Should_ThrowException_IfTempFileNotCreated()
        {
            using var htmlRenderer = new HtmlRenderer(HtmlStr, ExistPath);
            Assert.True(File.Exists(htmlRenderer.FileName));
        }
        [Fact]
        public void HtmlRendererFile_ShouldRemoveTempFile_AfterDisposeObject()
        {
            using var htmlRenderer = new HtmlRenderer(HtmlStr, ExistPath);
            htmlRenderer.Dispose();
            Assert.False(File.Exists(htmlRenderer.FileName));
        }

        [Theory]
        [ClassData(typeof(CheckStringTestData))]
        public void HtmlRendererFileUriWithEmptyOrNullOrWhiteSpaceChromeDriverPathShouldThrowException(string path)
        {
            ArgumentException exception = Assert.Throws<ArgumentNullException>(() =>
            {
                HtmlRendererWithUriCreator(new Uri(ValidUriStr), path);
            });
            Assert.Equal(nameof(path), exception.ParamName);
        }


        [Theory]
        [InlineData("InvalidPath")]
        [InlineData("C://InvalidPath")]
        public void HtmlRendererUri_Should_ThrowException_When_IfPathIsInvalid(string path)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                HtmlRendererWithUriCreator(new Uri(ValidUriStr), path);
            });
            Assert.Equal("path doesn't exist in your system or server", exception.Message);
        }

        #endregion

        #region Helper methods
        private void HtmlRendererCreator(string html, string chromeDriverPath)
        {
            using (var _ = new HtmlRenderer(html, chromeDriverPath))
            {
            }
        }

        private void HtmlRendererWithUriCreator(Uri uri, string chromeDriverPath)
        {
            using (var _ = new HtmlRenderer(uri, chromeDriverPath))
            {
            }
        }
        #endregion
    }
}