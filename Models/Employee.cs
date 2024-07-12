namespace ITInventoryAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Device>? Devices { get; set; }

        public Employee()
        {
            Name = string.Empty;
            Email = string.Empty;
        }

    }
}