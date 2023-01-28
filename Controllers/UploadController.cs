using MimeDetective.Definitions.Licensing;
using Microsoft.AspNetCore.Mvc;
using MimeDetective.Storage;
using MimeDetective;

namespace System;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    [HttpPost]
    public IActionResult Upload([FromForm] UploadRequest request)
    {
        var inspector = new ContentInspectorBuilder()
        {
            Definitions = MimeDetective.Definitions.Default.All()
        }.Build();

        using var fileStream = request.File.OpenReadStream();

        var inspectedResult = inspector.Inspect(fileStream);

        var resultByExtension = inspectedResult.ByFileExtension();
        var resultByMimeType = inspectedResult.ByMimeType();



        return Ok(new { mimeType = resultByMimeType.First().MimeType, extension = resultByExtension.First().Extension });
    }
}