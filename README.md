# CommonHtmlConverter
## 👩‍🏫 Usage
- it is a lightweight html converter library written by C# in .net 5.
- it is useful for convert html to image with powerfull html selector "xpath"
- the converter offers a lot of powerful options (convert any web page to image, convert any HTML string to image) it can be used to convert HTML strings that support html5/CSS and any javascript libraries that attach to the HTML page in the CDN server

## 📦NuGet :
Install using nuget https://www.nuget.org/packages/CommonHtmlConverter/
>**Install-Package CommonHtmlConverter**

## 🛠 Requirements to run:
First Install-Package Selenium.WebDriver.ChromeDriver package in your application(console app, web app or any .net applications).
* [Selenium.WebDriver.ChromeDriver](https://www.nuget.org/packages/Selenium.WebDriver.ChromeDriver)
> install this package is so important
> 
# Uses for console application
The first sample is convert google body element to image so you can easily use this code in your program.cs file
```csharp
  class Program
  {
      static void Main(string[] args)
      {
          var consoleAppPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          using var htmlRenderer = new HtmlRenderer(new Uri("https://www.google.com/"), consoleAppPath);
          var image = htmlRenderer.ConvertElementToImage(xpath: "//body");

          using MemoryStream ms = new MemoryStream(image);
          Image i = Image.FromStream(ms);
          i.Save("google.png");
       }
   }
```

# google.png file
[![google image](https://github.com/pejmansahebi/CommonHtmlConverter.Net/blob/master/HtmlConvertor.Common/assets/images/google.png)](https://github.com/pejmansahebi/CommonHtmlConverter.Net/edit/master/README.md)

# Second example
The second example is convert html string variable to images, there are two h1 elements in html string variable so out put is two byte array types that are eventually saved in the image(0).png and image(1).png
```csharp
    class Program
    {
        static void Main(string[] args)
        {
            string html =
                "<html><head><style>.h1Styles{color:red}</style></head><body><h1 class='h1Styles'>common html convertor</h1><h1 class='h1Styles'>pejman sahebi</h1></body></html>";
            var consoleAppPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using var htmlRenderer = new HtmlRenderer(html, consoleAppPath);
            var images = htmlRenderer.ConvertElementsToImages(xpath: "//h1");

            int imageIndex = 1;
            foreach (var image in images)
            {
                using MemoryStream ms = new MemoryStream(image);

                Image i = Image.FromStream(ms);
                i.Save($"image({imageIndex++}).png");
            }
        }
    }
```

