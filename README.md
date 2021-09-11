# CommonHtmlConverter
## 👩‍🏫 Usage
- it is a lightweight html converter library written by C# in .net 5.
- it is useful for convert html to image with powerfull html selector "xpath"

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





