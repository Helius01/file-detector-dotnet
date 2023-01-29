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
    /// <param name="fileStream"></param>
    /// <returns>true if the stream type is image else false</returns>
    bool IsAudio(Stream fileStream);
}