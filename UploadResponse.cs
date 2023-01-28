namespace System;

public class UploadResponse
{
    public string? FileName { get; set; }
    public string? FileExtension { get; set; }
    public bool IsImage { get; set; }
    public bool IsMp3 { get; set; }
}