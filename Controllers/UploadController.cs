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
        // var inspector = new ContentInspectorBuilder()
        // {
        //     Definitions = MimeDetective.Definitions.Default.FileTypes.Audio.All()
        // }.Build();

        // using var fileStream = request.File.OpenReadStream();

        // var inspectedResult = inspector.Inspect(fileStream);

        // var resultByExtension = inspectedResult.ByFileExtension();
        // var resultByMimeType = inspectedResult.ByMimeType();

        // var a = resultByMimeType.First().Matches.First().Definition;

        // var cats = a.File.Categories;

        // var def = _fileDetectorService.GetFileType(request.File.OpenReadStream());

        var isImage = _fileDetectorService.IsImage(request.File.OpenReadStream());

        return Ok(isImage);

        // return Ok(new { mimeType = resultByMimeType.First().MimeType, extension = resultByExtension.First().Extension });
    }
}