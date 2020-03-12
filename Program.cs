using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;

namespace PdfSplit
{
    class Program
    {
        static void Main(string[] _)
        {
            var dir = Directory.GetCurrentDirectory();
            var date = new DirectoryInfo(dir).Name;
            
            string[] files = Directory.GetFiles(dir, "*.pdf");
            foreach (var file in files)
            {
                PdfDocument input = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                string id = Path.GetFileNameWithoutExtension(file);
                Directory.CreateDirectory(id);

                var consent = new PdfDocument();
                AddRange(consent, input, 1, 10);
                consent.Save($"{id}/{id}_consent_{date}.pdf");

                var hipaa1 = new PdfDocument();
                AddRange(hipaa1, input, 11, 13);
                hipaa1.Save($"{id}/{id}_HIPAA1_{date}.pdf");

                var hipaa4 = new PdfDocument();
                AddRange(hipaa4, input, 14, 17);
                hipaa4.Save($"{id}/{id}_HIPAA4_{date}.pdf");

                var ihsAuth = new PdfDocument();
                AddRange(ihsAuth, input, 18, 20);
                ihsAuth.Save($"{id}/{id}_IHSAuth_{date}.pdf");

                var medcon = new PdfDocument();
                AddRange(medcon, input, 21, 24);
                medcon.Save($"{id}/{id}_medcon_{date}.pdf");
            }
        }

        private static void AddRange(PdfDocument output, PdfDocument input, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                output.AddPage(input.Pages[i]);
            }
        }
    }
}
