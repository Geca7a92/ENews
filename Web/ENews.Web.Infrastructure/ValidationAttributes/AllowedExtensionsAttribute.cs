﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace ENews.Web.Infrastructure.ValidationAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            if (value is IEnumerable)
            {
                var files = value as IEnumerable<IFormFile>;
                return this.CheckExtension(files);
            }
            else if (value is IFormFile)
            {
                var file = value as IFormFile;
                return this.CheckExtension(file);
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        private ValidationResult CheckExtension(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!this.extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult($"This file: {file.FileName} extension is not allowed!");
            }

            return ValidationResult.Success;
        }

        private ValidationResult CheckExtension(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!this.extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"This file: {file.FileName} extension is not allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}