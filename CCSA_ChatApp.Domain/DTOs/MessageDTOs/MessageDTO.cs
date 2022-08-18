namespace CCSA_ChatApp.Domain.DTOs.MessageDTOs
{
    public class MessageDTO
    {
        public Guid MessageId { get; set; }
        public string TextMessage { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}