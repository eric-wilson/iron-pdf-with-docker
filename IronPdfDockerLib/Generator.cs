using System;
using IronPdf;

namespace IronPdfDockerLib
{
    public class Generator
    {
        public Generator()
        {
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
    }
}
