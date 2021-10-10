using System;

namespace HtmlConvertor.DriverFactories
{
    public class WebDriverFactory : WebDriverFactoryBase
    {
        public override IWebDriverFactory GetDriver(string browser)
        {
            switch (browser)
            {
                case "chrome":
                    return new ChromeWebDriver();
                case "firefox":
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}