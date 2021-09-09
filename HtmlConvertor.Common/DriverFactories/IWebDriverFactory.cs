using OpenQA.Selenium;

namespace CommonHtmlConverter.DriverFactories
{
    public interface IWebDriverFactory
    {
        T CreateDriver<T>(string path) where T : class, IWebDriver;
    }
}