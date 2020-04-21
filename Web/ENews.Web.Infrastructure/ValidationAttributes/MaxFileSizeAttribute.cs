using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ENews.Web.Infrastructure.ValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            if (value is IEnumerable)
            {
                var files = value as IEnumerable<IFormFile>;
                return this.CheckSize(files);
            }
            else if (value is IFormFile)
            {
                var file = value as IFormFile;
                return this.CheckSize(file);
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        private ValidationResult CheckSize(IFormFile file)
        {
            if (file.Length > this.maxFileSize)
            {
                return new ValidationResult($"{file.FileName} is exceeding maximum allowed file size is 1 mb.");
            }

            return ValidationResult.Success;
        }

        private ValidationResult CheckSize(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > this.maxFileSize)
                {
                    return new ValidationResult($"{file.FileName} is exceeding maximum allowed file size is 1 mb.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
