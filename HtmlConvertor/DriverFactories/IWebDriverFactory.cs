using OpenQA.Selenium;

namespace HtmlConvertor.DriverFactories
{
    public interface IWebDriverFactory
    {
        T CreateDriver<T>(string path) where T : class, IWebDriver;
    }
}