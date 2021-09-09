namespace EasyHtmlConverter.Common.DriverFactories
{
    public abstract class WebDriverFactoryBase
    {
        public abstract IWebDriverFactory GetDriver(string browser);
    }
}