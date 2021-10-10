using System;
using System.Collections.Generic;
using System.Linq;
using HtmlConvertor.DriverFactories;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace HtmlConvertor.Helpers
{
    public class WebDriverHelper
    {
        public static byte[] GetImage(object webPage, string driverPath, string xpath)
        {
            ChromeDriver driver = webPage switch
            {
                string fileName => GetDriver(fileName, driverPath),
                Uri uri => GetDriver(uri, driverPath),
                _ => throw new ArgumentException(nameof(webPage))
            };
            var desktopScreenShot = TakFullPageScreenShot(driver);
            var image = GetDriverImage(driver, xpath, desktopScreenShot);
            driver.Close();
            driver.Quit();
            return image;
        }

        public static IEnumerable<byte[]> GetImages(object webPage, string driverPath, string xpath)
        {
            ChromeDriver driver = webPage switch
            {
                string fileName => GetDriver(fileName, driverPath),
                Uri uri => GetDriver(uri, driverPath),
                _ => throw new ArgumentException(nameof(webPage))
            };

            var desktopScreenShot = TakFullPageScreenShot(driver);
            var images = GetDriverImages(driver, xpath, desktopScreenShot).ToList();
            driver.Close();
            driver.Quit();
            return images;
        }

        public static byte[] GetPdf(object webPage, string driverPath, Dictionary<string, object> printOptions)
        {
            ChromeDriver driver = webPage switch
            {
                string fileName => GetDriver(fileName, driverPath),
                Uri uri => GetDriver(uri, driverPath),
                _ => throw new ArgumentException(nameof(webPage))
            };

            var printOutput = driver.ExecuteChromeCommandWithResult("Page.printToPDF", printOptions) as Dictionary<string, object>;
            var pdfStr = printOutput["data"] as string ?? string.Empty;
            return Convert.FromBase64String(pdfStr);
        }

        private static byte[] GetDriverImage(ChromeDriver driver, string mustImageXPath, Screenshot screenShot)
        {
            try
            {
                var mustBeImageElement = driver.FindElementByXPath(mustImageXPath);
                var finalCaptchaImage = mustBeImageElement.ConvertWebElementToBitmap(screenShot);
                return finalCaptchaImage.ToByteArray();
            }
            catch
            {
                return null;
            }
        }
        private static IEnumerable<byte[]> GetDriverImages(ChromeDriver driver, string mustImageXPath, Screenshot screenShot)
        {
            try
            {
                var mustBeImageElements = driver.FindElementsByXPath(mustImageXPath);
                return mustBeImageElements
                    .Select(mustBeImageElement => mustBeImageElement.ConvertWebElementToBitmap(screenShot))
                    .Select(finalCaptchaImage => finalCaptchaImage.ToByteArray());
            }
            catch
            {
                return null;
            }
        }

        private static Screenshot TakFullPageScreenShot(IWebDriver webDriver)
        {
            var verticalCombineDecorator = new VerticalCombineDecorator(new ScreenshotMaker());
            var screenShot = new Screenshot(Convert.ToBase64String(webDriver.TakeScreenshot(verticalCombineDecorator)));
            return screenShot;
        }
        private static ChromeDriver GetDriver(string fileName, string driverPath)
        {
            WebDriverFactoryBase factory = new WebDriverFactory();
            var chromeWebDriverFactory = factory.GetDriver("chrome");
            var driver = chromeWebDriverFactory.CreateDriver<ChromeDriver>(driverPath);
            driver.Navigate().GoToUrl(fileName);
            return driver;
        }
        private static ChromeDriver GetDriver(Uri uri, string driverPath)
        {
            WebDriverFactoryBase factory = new WebDriverFactory();
            var chromeWebDriverFactory = factory.GetDriver("chrome");
            var driver = chromeWebDriverFactory.CreateDriver<ChromeDriver>(driverPath);
            driver.Navigate().GoToUrl(uri);
            return driver;
        }
    }
}