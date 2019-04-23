using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class PayrollDatabase: IPayrollDatabase
    {
        private readonly Dictionary<int, Employee> itsEmployees;
        private readonly Dictionary<int, Employee> unionMembers;

        public PayrollDatabase()
        {
            itsEmployees = new Dictionary<int, Employee>();
            unionMembers = new Dictionary<int, Employee>();
        }

        public Employee GetEmployee(int empId)
        {
            if (itsEmployees.ContainsKey(empId))
                return itsEmployees[empId];

            return null;
        }

        public void Clear()
        {
            itsEmployees.Clear();
        }

        public void DeleteEmployee(int id)
        {
            itsEmployees.Remove(id);
        }

        public void AddUnionMember(int id, Employee e)
        {
            unionMembers.Add(id, e);
        }

        public Employee GetUnionMember(int id)
        {
            if (unionMembers.ContainsKey(id))
                return unionMembers[id];

            return null;
        }

        public void RemoveUnionMember(int memberId)
        {
            unionMembers.Remove(memberId);
        }

        public List<int> GetAllEmployeeIds()
        {
            var employees = new List<int>();
            foreach (var item in itsEmployees)
            {
                employees.Add(item.Key);
            }

            return employees;
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            foreach(var item in itsEmployees)
            {
                employees.Add(item.Value);
            }

            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            itsEmployees.Add(employee.EmpId, employee);
        }
    }

    public interface IPayrollDatabase
    {
        void AddEmployee(Employee employee);
        Employee GetEmployee(int id);
        void DeleteEmployee(int id);
        void AddUnionMember(int id, Employee e);
        Employee GetUnionMember(int id);
        void RemoveUnionMember(int memberId);
        List<int> GetAllEmployeeIds();
        List<Employee> GetAllEmployees();
    }
}
