namespace CCSA_ChatApp.Domain.DTOs.MessageDTOs
{
    public class MessageDTO
    {
        public virtual string TextMessage { get; set; }
        public virtual DateTime MessageCreated { get; set; }
    }
}