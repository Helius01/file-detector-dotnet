using MimeDetective.Definitions.Licensing;
using Microsoft.AspNetCore.Mvc;
using MimeDetective.Storage;
using MimeDetective;
using System.Collections.Immutable;
using Services;
using Models;

namespace System;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    private readonly IFileDetectorService _fileDetectorService;

    public UploadController(IFileDetectorService fileDetectorService)
    {
        _fileDetectorService = fileDetectorService;
    }

    [HttpPost]
    public IActionResult Upload([FromForm] UploadRequest request)
    {
        var fileStream = request.File.OpenReadStream();

        var isImage = _fileDetectorService.IsImage(fileStream);
        var isAudio = _fileDetectorService.IsAudio(fileStream);
        var fileType = _fileDetectorService.GetFileType(fileStream);
        var hasSameExtension = _fileDetectorService.IsValidExtension(request.File.FileName, fileStream);

        return Ok(new
        {
            isImage = isImage,
            isAudio = isAudio,
            fileType = fileType,
            hasSameExtension
        });
    }
}