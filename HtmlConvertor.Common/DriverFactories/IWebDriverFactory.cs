using OpenQA.Selenium;

namespace EasyHtmlConverter.Common.DriverFactories
{
    public interface IWebDriverFactory
    {
        T CreateDriver<T>(string path) where T : class, IWebDriver;
    }
}