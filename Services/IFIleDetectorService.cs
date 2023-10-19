using Models;

namespace Services;

public interface IFileDetectorService
{
    /// <summary>
    /// Returns file type 
    /// </summary>
    /// <param name="fileStream">file stream</param>
    FileDetectorResponse GetFileType(Stream fileStream);

    /// <summary>
    /// Checks whether the file is an image
    /// </summary>
    /// <param name="fileStream">file stream</param>
    /// <returns>true if the stream type is image else false</returns>
    bool IsImage(Stream fileStream);

    /// <summary>
    /// Checks whether the file is an audio
    /// </summary>
    /// <param name="fileStream">file stream</param>
    /// <returns>true if the stream type is image else false</returns>
    bool IsAudio(Stream fileStream);

    /// <summary>
    /// Compare given file extension with inspected file extension
    /// </summary>
    /// <param name="fileName">full file name (ex : lorem.png)</param>
    /// <param name="fileStream">file stream</param>
    /// <returns>true if the extension of given file equals with inspected file extension else false</returns>
    bool IsValidExtension(string fileName, Stream fileStream);
}