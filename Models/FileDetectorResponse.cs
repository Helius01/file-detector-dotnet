using System.Collections.Immutable;
using MimeDetective.Storage;

namespace Models;

public class FileDetectorResponse
{
    public FileDetectorResponse(string extension, string mime, IEnumerable<Category> categories)
    {
        Extension = extension;
        Mime = mime;
        Categories = categories;
    }

    public readonly string Extension;
    public readonly string Mime;
    public readonly IEnumerable<Category> Categories;

}