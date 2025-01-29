namespace CarStoreApi.Models.Dto
{
    public class RegisterationResponseDTO
    {

        public UserDTO User { get; set; }
        public List<string> errors { get; set; } 


    }
}
