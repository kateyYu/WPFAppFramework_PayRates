/*
Group-4
    Kurian, Eldho,
    Mittal, Tanya,
    Pou, Aileen,
    Yu, Katey,
 */
using System.ComponentModel;

namespace PayRates
{
    public class VM
    {
        public BindingList<Employee> Employees { get; set; }

        public VM()
        {
            Db.GetInstance().CreateDatabase();
            Employees = new BindingList<Employee>(Db.GetInstance().Read());
        }
    }
}
