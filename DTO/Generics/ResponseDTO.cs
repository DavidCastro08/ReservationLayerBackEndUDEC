namespace ReservationLinKtic.DTO.Generics
{
    public class ResponseDTO
    {
        public bool isValid { get; set; }
        public string? error { get; set; }
        public string? message { get; set; }
    }
}
