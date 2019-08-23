using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace TuesPechkin
{
	/// <summary>
	/// WinAnyCPUEmbeddedDeployment
	/// </summary>
	[Serializable]
	public class WinAnyCPUEmbeddedDeployment : EmbeddedDeployment
	{
		/// <summary>
		/// wkhtmltoxDll byte array
		/// </summary>
		private byte[] wkhtmltoxDll;

		/// <summary>
		/// Initializes a new instance of the <see cref="WinAnyCPUEmbeddedDeployment"/> class.
		/// WinAnyCPUEmbeddedDeployment
		/// </summary>
		/// <param name="physical">Deployment</param>
		public WinAnyCPUEmbeddedDeployment(IDeployment physical)
			: base(physical)
		{
			if (Environment.Is64BitProcess)
			{
				this.wkhtmltoxDll = Wkhtmltox.AnyCPU.Properties.Resources.wkhtmltox_64_dll;
			}
			else
			{
				this.wkhtmltoxDll = Wkhtmltox.AnyCPU.Properties.Resources.wkhtmltox_32_dll;
			}
		}

		/// <summary>
		/// Get path
		/// </summary>
		public override string Path
		{
			get
			{
				return System.IO.Path.Combine(
					base.Path,
					GetType().Assembly.GetName().Version.ToString());
			}
		}

		/// <summary>
		/// GetContents
		/// </summary>
		/// <returns>KeyValuePair of stream</returns>
		protected override IEnumerable<KeyValuePair<string, Stream>> GetContents()
		{
			return new[]
			{
				new KeyValuePair<string, Stream>(
					key: WkhtmltoxBindings.DLLNAME,
					value: new GZipStream(
						new MemoryStream(this.wkhtmltoxDll),
						CompressionMode.Decompress))
			};
		}
	}
}
