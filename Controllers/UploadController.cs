using FileTypeChecker;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Mvc;

namespace System;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    [HttpPost]
    public IActionResult Upload([FromForm] UploadRequest request)
    {
        using var fileStream = request.File.OpenReadStream();

        var isRecognizableType = FileTypeValidator.IsTypeRecognizable(fileStream);

        if (!isRecognizableType)
        {
            return BadRequest(new { message = "Invalid file type" });
        }

        var fileType = FileTypeValidator.GetFileType(fileStream);

        var response = new UploadResponse
        {
            FileExtension = fileType.Extension,
            FileName = fileType.Name,
            IsImage = fileStream.IsImage(),
            IsMp3 = fileStream.Is<Mp3>()
        };

        return Ok(response);
    }
}