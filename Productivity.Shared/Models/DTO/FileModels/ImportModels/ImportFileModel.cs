using Microsoft.AspNetCore.Http;
using Productivity.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivity.Shared.Models.DTO.FileModels.ImportModels
{
    public class ImportFileModel
    {
        [Required(ErrorMessage = "Файл обязателен для выбора")]
        [AllowedExtensions([".xlsx"], ErrorMessage = "Разрешены только файлы формата Excel (.xlsx)")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Файл не может иметь размер более 5 мегабайт")]
        public IFormFile? File { get; set; }
    }
}
