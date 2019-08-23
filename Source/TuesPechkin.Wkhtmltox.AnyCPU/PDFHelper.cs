using System.Web.Hosting;

namespace TuesPechkin.Wkhtmltox.AnyCPU
{
	public class PDFHelper
	{
		public class Factory
		{
			/// <summary>
			/// The static readonly locker
			/// </summary>
			private static readonly object Locker = new object();

			/// <summary>
			/// Pdf converter
			/// </summary>
			private static IConverter converter;

			/// <summary>
			/// Singleton converter, for multi-threaded application
			/// </summary>
			/// <returns>Pdf converter</returns>
			public static IConverter GetConverter()
			{
				lock (Locker)
				{
					if (converter != null)
					{
						return converter;
					}

					var tempFolderDeployment = new TempFolderDeployment();
					var winAnyCpuEmbeddedDeployment = new WinAnyCPUEmbeddedDeployment(tempFolderDeployment);
					IToolset toolSet;
					if (HostingEnvironment.IsHosted)
					{
						toolSet = new RemotingToolset<PdfToolset>(winAnyCpuEmbeddedDeployment);
					}
					else
					{
						toolSet = new PdfToolset(winAnyCpuEmbeddedDeployment);
					}

					converter = new ThreadSafeConverter(toolSet);
				}

				return converter;
			}
		}
	}
}
