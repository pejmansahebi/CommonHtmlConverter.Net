using System;
using System.Collections.Generic;
using System.Linq;
using HtmlConvertor.Common.DriverFactories;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace HtmlConvertor.Common.Helpers
{
    public class WebDriverHelper
    {
        public static byte[] GetImage(string fileName, string driverPath, string xpath)
        {
            var driver = GetDriver(fileName, driverPath);
            var desktopScreenShot = TakFullPageScreenShot(driver);
            var image = GetDriverImage(driver, xpath, desktopScreenShot);
            driver.Close();
            driver.Quit();
            return image;
        }
        public static byte[] GetImage(Uri uri, string driverPath, string xpath)
        {
            var driver = GetDriver(uri, driverPath);
            var desktopScreenShot = TakFullPageScreenShot(driver);
            var image = GetDriverImage(driver, xpath, desktopScreenShot);
            driver.Close();
            driver.Quit();
            return image;
        }
        public static IEnumerable<byte[]> GetImages(string fileName, string driverPath, string xpath)
        {
            var driver = GetDriver(fileName, driverPath);
            var desktopScreenShot = TakFullPageScreenShot(driver);
            var images = GetDriverImages(driver, xpath, desktopScreenShot).ToList();
            driver.Close();
            driver.Quit();
            return images;
        }
        public static IEnumerable<byte[]> GetImages(Uri uri, string driverPath, string xpath)
        {
            var driver = GetDriver(uri, driverPath);
            var desktopScreenShot = TakFullPageScreenShot(driver);
            var images = GetDriverImages(driver, xpath, desktopScreenShot).ToList();
            driver.Close();
            driver.Quit();
            return images;
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