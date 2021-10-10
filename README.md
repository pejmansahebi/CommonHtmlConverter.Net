# Html Convertor
## 👩‍🏫 Usage
- it is a lightweight html converter library written by C#.
- it is useful for convert html to image or pdf with powerfull html selector "xpath"
- the converter offers a lot of powerful options (convert any web page or HTML string to image or pdf) it can be used to convert HTML strings that support html5/CSS and any javascript libraries that attach to the HTML page in the CDN server

## 📦NuGet :
Install using nuget https://www.nuget.org/packages/HtmlConvertor/
>**Install-Package HtmlConvertor**

## 🛠 Requirements to run:
First Install-Package Selenium.WebDriver.ChromeDriver package in your application(console app, web app or any .net applications).
* [Selenium.WebDriver.ChromeDriver](https://www.nuget.org/packages/Selenium.WebDriver.ChromeDriver)
> install this package is so important

## Convert web page html to image example
The first sample is convert google body element to image so you can easily use this code in your program.cs file
```csharp
  class Program
  {
      static void Main(string[] args)
      {
          var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          using var htmlRenderer = new HtmlRenderer(new Uri("https://www.google.com/"), driverPath);
          var image = htmlRenderer.ConvertElementToImage(xpath: "//body");

          using MemoryStream ms = new MemoryStream(image);
          Image i = Image.FromStream(ms);
          i.Save("google.png");
       }
   }
```

## google.png file
[![google image](https://github.com/pejmansahebi/CommonHtmlConverter.Net/blob/master/HtmlConvertor/assets/images/google.png)](https://github.com/pejmansahebi/CommonHtmlConverter.Net/edit/master/README.md)

## Convert html string to images example
The second example is convert html string variable to images, there are two h1 elements in html string variable so out put is two byte array types that are eventually saved in the image(0).png and image(1).png
```csharp
    class Program
    {
        static void Main(string[] args)
        {
            string html =
                "<html><head><style>.h1Styles{color:red}</style></head><body><h1 class='h1Styles'>common html convertor</h1><h1 class='h1Styles'>pejman sahebi</h1></body></html>";
            var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using var htmlRenderer = new HtmlRenderer(html, driverPath);
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

## Convert web page to pdf
```csharp
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var printOptions = new Dictionary<string, object>
            {
                { "paperWidth", 210 / 25.4 },
                { "paperHeight", 297 / 25.4 },
                { "scale", 0.9 },
                { "pages", "all" }
            };
            using var htmlRenderer = new HtmlRenderer(new Uri("https://en.wikipedia.org/wiki/Freedom"), path);
            byte[] pdf = htmlRenderer.ConvertToPdf(printOptions);
            File.WriteAllBytes("test.pdf", pdf);
        }
    }
```
> The options available for the Page.printToPDF call are documented here: (https://chromedevtools.github.io/devtools-protocol/tot/Page/#method-printToPDF)
