﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PhotoSauce.MagicScaler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookWarm.Data.FileManager
{
	public class FileManager:IFileManager
	{
		private string _imagePath;

		public FileManager(IConfiguration config) // it injects appsettings.json
		{
			_imagePath = config["Path:Images"];
		}

		public FileStream ImageStream(string image)
		{
			return new FileStream(Path.Combine(_imagePath,image),FileMode.Open,FileAccess.Read);
		}

		public bool RemoveImage(string image)
		{
			try
			{
				var file = Path.Combine(_imagePath, image);
				if (File.Exists(file))
				{
					File.Delete(file);
				}
				return true;
			}
			catch (Exception e )
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		public async Task<string> SaveImage(IFormFile image)
		{
			try
			{
				//check if directory wwwroot//content//static exists, if not, create

				var save_path = Path.Combine(_imagePath);

				if (!Directory.Exists(save_path))
				{
					Directory.CreateDirectory(save_path);
				}

				//Internet Explorer Error C:/User/Foo/Image.jpg so this is not only Image name but full dir
				//var fileName = image.FileName;
				var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
				var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";

				using (var fileStream = new FileStream(Path.Combine(save_path, fileName), FileMode.Create)) //File Stream is tunel to a folder
				{
					//await image.CopyToAsync(fileStream);
					MagicImageProcessor.ProcessImage(image.OpenReadStream(), fileStream, ImageOptions());
				}

				return fileName;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return "Error";
			}
		}
		private ProcessImageSettings ImageOptions() => new ProcessImageSettings
		{
			Width = 800,
			Height = 500,
			ResizeMode = CropScaleMode.Crop,
			SaveFormat = FileFormat.Jpeg,
			JpegQuality = 100,
			JpegSubsampleMode = ChromaSubsampleMode.Subsample420
		}; 
	}
}
