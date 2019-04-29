# Test App for IronPdf

This solution demostrates the the error of writing a pdf (`Error: Can not parse a PDF from an empty byte array`) from inside of a docker container.

The error appears to be an issue with IronPdf's ablity to unpack its supporting libraries

The default action is in the /tmp directory.  IronPdf successfully writes to a file called writetest.txt with the value of OK, so it appears it has the functionality to read and write to that directory

> The errors do not occur if you run the applications directly outside of the container.



## Solution Structure

This solution has 3 projects

1. IronPdfDockerClient
1. IronPdfDockerLib
1. IronPdfDockerWebApi


## IronPdfDockerClient

A sample console application that runs as soon as it's container starts.  The client uses the IronPdfDockerLib to create a simple html string attempts to generate a Pdf.


## IronPdfDockerLib

This represents a sample of a lib project for the business layer.  It uses IronPdf `HtmlToPdf`.  The error occurs when executing the `RenderHtmlAsPdf(...)` 

```csharp
Line 43  htmlToPdf.RenderHtmlAsPdf(GetHtml())
```

When the container starts it will attempt to create a pdf.


## IronPdfDockerWebApi

Contains a rest API contains the default controller of `/api/values`, you can use this endpoint or `/api/values/1` to execute the same code in the console app.



## Running the container.

### Build and run the container

A `docker-compose` file sets up the three projects.  The web project will run on port 6008.

1. From the Solution Directory execute `docker-compose up ` or  `docker-compose up --build`
1. View the output from the containers display, you will see the console app failing
1. Open a web browser http://localhost:6008/api/values









