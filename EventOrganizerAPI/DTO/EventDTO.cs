namespace EventOrganizerAPI.DTO
{
    public class EventDTO
    {
        public int LocationId { get; set; }
        public int ParticipantId { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
    }
}
