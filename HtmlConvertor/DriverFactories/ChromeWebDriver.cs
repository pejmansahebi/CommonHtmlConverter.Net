using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HtmlConvertor.DriverFactories
{
    public class ChromeWebDriver : IWebDriverFactory
    {
        public T CreateDriver<T>(string path) where T : class, IWebDriver
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");
            options.AddArgument("start-maximized");
            var driver = new ChromeDriver(path, options);
            return driver as T;
        }
    }
}