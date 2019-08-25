using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TuesPechkin.Wkhtmltox.AnyCPU;

namespace TuesPechkin.WebApp.Controllers
{
    public class PdfController : Controller
    {
		public FileContentResult Get()
		{
			byte[] buf = null;
			var converter = PDFHelper.Factory.GetConverter();
			try
			{
				buf = converter.Convert(this.Document);

				if (buf == null)
				{
					throw new Exception();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return new FileContentResult(buf, "application/pdf");
		}

		private HtmlToPdfDocument Document = new HtmlToPdfDocument
		{
			Objects =
				{
					new ObjectSettings { PageUrl = "www.google.com" }
				}
		};
	}
}
