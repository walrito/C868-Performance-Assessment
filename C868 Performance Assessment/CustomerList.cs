namespace C868_Performance_Assessment
{
    public class CustomerList
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public CustomerList(int customerId, string customerName)
        {
            CustomerId = customerId;
            CustomerName = customerName;
        }
    }
}