using TestTrickLord.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ImageGallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            string imageFolder = Path.Combine(_env.WebRootPath, "Images");
            List<ImageModel> images = new List<ImageModel>();

            if (Directory.Exists(imageFolder))
            {
                var files = Directory.GetFiles(imageFolder, "*.*") // Lấy tất cả file ảnh
                                     .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png"))
                                     .OrderBy(f => f) // Sắp xếp theo tên file
                                     .ToList();

                foreach (var file in files)
                {
                    images.Add(new ImageModel
                    {
                        ImageUrl = "/Images/" + Path.GetFileName(file)
                    });
                }
            }

            return View(images);
        }

    }
}
