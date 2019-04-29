using System;
using IronPdf;

namespace IronPdfDockerLib
{
    public class Generator
    {
        public Generator()
        {

            var tmp = System.IO.Path.GetTempPath();
            IronPdf.Installation.TempFolderPath = tmp;

            Log($"Temp Path set to: {tmp}");

            //if (string.IsNullOrWhiteSpace(License.LicenseKey))
            {
                // set the license key / get it from appsettings or environment variables
                //License.LicenseKey = "";

            }
        }

        public string BuildReport()
        {
            var htmlToPdf = new HtmlToPdf();
            var organization = "Acme Company";

            var path = System.IO.Path.GetTempPath();
            path = System.IO.Path.Combine(path, $"test-file{DateTime.UtcNow.Ticks}.pdf");

            htmlToPdf.PrintOptions.FitToPaperWidth = true;
            htmlToPdf.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Landscape;

            htmlToPdf.PrintOptions.Header.LeftText = "Audit Report";
            htmlToPdf.PrintOptions.Header.RightText = DateTime.UtcNow.ToString();
            htmlToPdf.PrintOptions.Header.DrawDividerLine = true;

            htmlToPdf.PrintOptions.Footer.LeftText = $"Confidential {organization}";
            htmlToPdf.PrintOptions.Footer.DrawDividerLine = true;
            htmlToPdf.PrintOptions.Footer.RightText = $"\u00A9 {organization}";


            Log("Getting ready to render html");


            try
            {
                var pdf = htmlToPdf.RenderHtmlAsPdf(GetHtml());

                Log("Rendered ready to render html");

                Log($"file path {path}");

                pdf.SaveAs(path);

                return $"Success: File generated: {path}";

            }
            catch (Exception ex)
            {
                Log("FAILED TO CREATE PDF");
                Log(ex.ToString());


                TestRead();
                TestWrite();


                return $"Failed to produce file: {path}.  Error: {ex.Message}";
            }

            

           

        }

        private string GetHtml()
        {
            var html = @"
            <html>
            <body>
            This is a test.

            </body>
            </html>
            ";


            ;

            return html;

        }

        private void Log(string message)
        {
            Console.WriteLine($"[Pdf Generator]: {message}");
        }




        public void TestRead()
        {
            var dir = IronPdf.Installation.TempFolderPath;

            Log($"Testing dir {dir}");

            if(System.IO.Directory.Exists(dir))
            {
                var files = System.IO.Directory.GetFiles(dir);
                var first = "";
                foreach(var f in files)
                {
                    if (string.IsNullOrWhiteSpace(first)) { first = f; }
                    Log($"Found Files {f}");
                }


                try
                {
                    // try to read a file
                    var text = System.IO.File.ReadAllText(first);

                    var max = 100;
                    if (text.Length < max) max = text.Length;

                    Log($"Read file {first} ok {text.Substring(0, max)}");

                } catch (Exception ex)
                {
                    Log($"Error reading file {ex.Message}");
                }
            }
            else
            {

                Log($"Directory Not Found During test {dir}");
            }
        }


        public void TestWrite()
        {
            var dir = IronPdf.Installation.TempFolderPath;

            Log($"Testing write on dir {dir}");

            if (System.IO.Directory.Exists(dir))
            {
                var text = "This is a test write";


                try
                {
                    var fileName = System.IO.Path.Combine(dir, $"test-{System.DateTime.UtcNow.Ticks}.txt");
                    System.IO.File.WriteAllText(fileName, text);

                    Log($"Write sucess {fileName}");
                }
                catch (Exception ex)
                {
                    Log($"Write Failed {ex.Message}");
                }
                

            }
            else
            {

                Log($"Directory Not Found During test {dir}");
            }
        }
    }
}
