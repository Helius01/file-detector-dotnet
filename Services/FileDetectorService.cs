using MimeDetective;
using MimeDetective.Storage;
using Models;

namespace Services;

public class FileDetectorService : IFileDetectorService
{
    /////////////////   
    /// Attention!///
    /////////////////
    /// If you wanna develop this service by using  ** MimeDetective ** package,
    /// Notice that always pass the resetPosition flag as true on inspecting a file stream.
    /// Else the you'll see some unexpected result that make your mind mess

    /// <summary>
    /// Returns file type
    /// </summary>
    /// <param name="fileStream">file stream</param>
    /// <returns>FileDetectorResponse</returns>
    public FileDetectorResponse GetFileType(Stream fileStream)
    {
        var inspector = GetInspector(DefinitionType.All);

        var inspectorResult = inspector.Inspect(fileStream, true);

        var resultsByMimeType = inspectorResult.ByMimeType();

        var firstMatch = resultsByMimeType.First().Matches.OrderByDescending(x => x.Points).First();

        return new FileDetectorResponse(firstMatch.Definition.File.Extensions.First(), firstMatch.Definition.File.MimeType ?? "", firstMatch.Definition.File.Categories);
    }

    /// <summary>
    /// Checks whether the file is an image
    /// </summary>
    /// <param name="fileStream">file stream</param>
    /// <returns>true if the stream type is image else false</returns>
    public bool IsImage(Stream fileStream)
    {
        var inspector = GetInspector(DefinitionType.Image);

        var inspectedResult = inspector.Inspect(fileStream, true);

        return inspectedResult.Any();
    }

    /// <summary>
    /// Checks whether the file is an audio
    /// </summary>
    /// <param name="fileStream"></param>
    /// <returns>true if the stream type is image else false</returns>
    public bool IsAudio(Stream fileStream)
    {
        var inspector = GetInspector(DefinitionType.Audio);

        var inspectedResult = inspector.Inspect(fileStream, true);

        return inspectedResult.Any();
    }

    /// <summary>
    /// Compare given file extension with inspected file extension
    /// </summary>
    /// <param name="fileName">full file name (ex : lorem.png)</param>
    /// <param name="fileStream">file stream</param>
    /// <returns>true if the extension of given file equals with inspected file extension else false</returns>
    public bool IsValidExtension(string fileName, Stream fileStream)
    {
        var givenExtension = Path.GetExtension(fileName);

        var inspector = GetInspector(DefinitionType.All);

        var inspectedResult = inspector.Inspect(fileStream, true);

        var result = inspectedResult.ByFileExtension();

        if (!result.Any()) return false;

        var trimmedGivenExtension = givenExtension.StartsWith('.') ? givenExtension.Replace('.', ' ').Trim() : givenExtension;

        return result.First().Extension.ToLowerInvariant() == trimmedGivenExtension.ToLowerInvariant();

    }

    //////////////////////
    ////** Privates **////
    //////////////////////

    /// <summary>
    /// Returns definitions by type 
    /// </summary>
    /// <param name="type">Definition Type</param>
    /// <returns>A list of definitions to build inspector</returns>
    private static IEnumerable<Definition> GetDefinitions(DefinitionType type)
    {
        return type switch
        {
            DefinitionType.All => new MimeDetective.Definitions.ExhaustiveBuilder() { UsageType = MimeDetective.Definitions.Licensing.UsageType.PersonalNonCommercial }.Build(),
            DefinitionType.Image => MimeDetective.Definitions.Default.FileTypes.Images.All(),
            DefinitionType.Audio => (IEnumerable<Definition>)MimeDetective.Definitions.Default.FileTypes.Audio.All(),
            _ => throw new ArgumentException($"Invalid definition type ${type}")
        };
    }

    /// <summary>
    /// Returns an inspector object that using to inspect file
    /// </summary>
    /// <param name="type">Definition Type</param>
    /// <returns>An inspector object to inspect files</returns>
    private static ContentInspector GetInspector(DefinitionType type)
    {
        return type switch
        {
            DefinitionType.All => new ContentInspectorBuilder() { Definitions = GetDefinitions(type).ToList() }.Build(),
            DefinitionType.Image => new ContentInspectorBuilder() { Definitions = GetDefinitions(type).ToList() }.Build(),
            DefinitionType.Audio => new ContentInspectorBuilder() { Definitions = GetDefinitions(type).ToList() }.Build(),
            _ => throw new ArgumentException($"Invalid definition type ${type}")
        };
    }
}