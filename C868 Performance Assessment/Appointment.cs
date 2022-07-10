using System;

namespace C868_Performance_Assessment
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string CustomerName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Appointment(int appointmentId, string customerName, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end)
        {
            AppointmentId = appointmentId;
            CustomerName = customerName;
            Title = title;
            Description = description;
            Location = location;
            Contact = contact;
            Type = type;
            Url = url;
            Start = start;
            End = end;
        }
    }
}