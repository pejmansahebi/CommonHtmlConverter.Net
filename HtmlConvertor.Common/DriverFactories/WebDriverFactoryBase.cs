namespace CommonHtmlConverter.DriverFactories
{
    public abstract class WebDriverFactoryBase
    {
        public abstract IWebDriverFactory GetDriver(string browser);
    }
}