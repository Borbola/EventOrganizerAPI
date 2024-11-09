namespace EventOrganizerAPI.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int ParticipantId { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }

        public virtual Location? Location { get; set; }
        public virtual Participant? Participant { get; set; }
    }
}
