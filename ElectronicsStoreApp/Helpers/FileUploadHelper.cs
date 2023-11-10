namespace ElectronicsStoreApp.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string> UploadAsync(IFormFile formFile)
        {
            if(formFile!=null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
                using var fs = new FileStream(@$"wwwroot/uploads/{fileName}", FileMode.Create);
                await formFile.CopyToAsync(fs);
                return @$"/uploads/{fileName}";
            }
            throw new Exception("File was not upload");
        }
    }
}
