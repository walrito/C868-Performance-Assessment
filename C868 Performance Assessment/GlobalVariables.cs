using System.Collections.Generic;

namespace C868_Performance_Assessment
{
    public static class GlobalVariables
    {
        public static List<Appointment> appointmentList = new List<Appointment>();

        public static string DbConn { get; set; }
        public static bool LoggedIn { get; set; }
        public static int UserId { get; set; }
        public static string UserName { get; set; }
    }
}