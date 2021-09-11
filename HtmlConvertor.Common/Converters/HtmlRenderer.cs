using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonHtmlConverter.Enums;
using CommonHtmlConverter.Helpers;

namespace CommonHtmlConverter.Converters
{
    public class HtmlRenderer : IDisposable
    {
        public readonly string FileName;
        private readonly WebDriverNavigationType _webDriverNavigationType;
        private readonly string _chromeDriverPath;
        public readonly Uri Uri;

        public HtmlRenderer(string html, string chromeDriverPath)
        {
            if (string.IsNullOrWhiteSpace(html))
                throw new ArgumentNullException(nameof(html));
            if (!PathHelper.IsPathExist(chromeDriverPath))
            {
                throw new ArgumentException("path doesn't exist in your system or server");
            }

            _webDriverNavigationType = WebDriverNavigationType.File;
            _chromeDriverPath = chromeDriverPath;
            string tempPath = Path.GetTempFileName();
            FileName = tempPath + ".html";
            File.WriteAllText(FileName, html, Encoding.UTF8);
        }

        public HtmlRenderer(Uri uri, string chromeDriverPath)
        {
            if (!PathHelper.IsPathExist(chromeDriverPath))
            {
                throw new ArgumentException("path doesn't exist in your system or server");
            }
            _webDriverNavigationType = WebDriverNavigationType.Uri;
            HttpHelper.CheckUri(uri);
            _chromeDriverPath = chromeDriverPath;
            Uri = uri;
        }

        public byte[] ConvertElementToImage(string xpath)
        {
            return _webDriverNavigationType switch
            {
                WebDriverNavigationType.File => WebDriverHelper.GetImage(FileName, _chromeDriverPath, xpath),
                WebDriverNavigationType.Uri => WebDriverHelper.GetImage(Uri, _chromeDriverPath, xpath),
                _ => throw new ArgumentException(nameof(WebDriverNavigationType))
            };
        }

        public IEnumerable<byte[]> ConvertElementsToImages(string xpath)
        {
            return _webDriverNavigationType switch
            {
                WebDriverNavigationType.File => WebDriverHelper.GetImages(FileName, _chromeDriverPath, xpath),
                WebDriverNavigationType.Uri => WebDriverHelper.GetImages(Uri, _chromeDriverPath, xpath),
                _ => throw new ArgumentException(nameof(WebDriverNavigationType))
            };
        }
        public byte[] ConvertToPdf(Dictionary<string, object> printOptions)
        {
            return _webDriverNavigationType switch
            {
                WebDriverNavigationType.File => WebDriverHelper.GetPdf(FileName, _chromeDriverPath, printOptions),
                WebDriverNavigationType.Uri => WebDriverHelper.GetPdf(Uri, _chromeDriverPath, printOptions),
                _ => throw new ArgumentException(nameof(WebDriverNavigationType))
            };
        }
        public void Dispose()
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                File.Delete(FileName);
            }
        }


    }
}