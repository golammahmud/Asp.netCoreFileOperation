namespace RazorFileUploads.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        
        public string? FileName { get; set; }

        public byte[]? Data { get; set; }


        public DateTime? Created_at { get; set; } = DateTime.UtcNow;




    }
}
