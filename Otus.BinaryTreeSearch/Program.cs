﻿




using System.Xml.Linq;

namespace Otus.BinaryTreeSearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // заглушка, чтобы не вводить руками самому сотрудников
            var employees = new List<Employee>
            {
                new("7", 7),
                new("1", 1),
                new("10", 10),
                new("4", 4),
                new("6", 6),
                new("9", 9),
                new("2", 2)
            };
            LaunchProgram(employees);
            while (true)
            {
                Console.WriteLine("Введите 0, чтобы начать сначала или 1, чтобы найти сотрудника по его зарплате:");
                _ = int.TryParse(Console.ReadLine(), out var input);
                if (input == 0)
                {
                    LaunchProgram(employees);
                }
                if (input == 1)
                {
                    var binaryTree = BuildBinaryTree(employees);
                    SearhSalary(binaryTree);
                }
            }
        }

        private static void LaunchProgram(List<Employee> employees)
        {
            //чтобы не ошибиться с размером массива, сначала все сотрудники помещаются в List
            //employees = [];
            //while (true)
            //{
            //    Console.WriteLine("Введите имя сотрудника:");
            //    var name = Console.ReadLine();
            //    if (name!.Equals(string.Empty))
            //    {
            //        break;
            //    }
            //    Console.WriteLine("Введите зарплату сотрудника:");
            //    var isSuccess = int.TryParse(Console.ReadLine(), out var salary);
            //    if (isSuccess == false)
            //    {
            //        Console.WriteLine("Зарплата должна быть целым числом!");
            //        continue;
            //    }
            //    var employee = new Employee(name, salary);
            //    employees.Add(employee);
            //}
            var binaryTree = BuildBinaryTree(employees);
            ShowSalariesAsc(binaryTree);
            SearhSalary(binaryTree);
        }

        private static void SearhSalary(Employee[] binaryTree)
        {
            Console.WriteLine("Введите искомый размер зарплаты: ");
            _ = int.TryParse(Console.ReadLine(), out var salaryToFind);
            FindEmployee(binaryTree, salaryToFind, 0);
        }

        private static void FindEmployee(Employee[] binaryTree, int salary, int node)
        {
            var employee = binaryTree[0];
            var height = Math.Log2(binaryTree.Length + 1) - 1;
            if (node == 0 && employee.Salary == salary)
            {
                Console.WriteLine($"Искомый сотрудник: {employee.Name}");
                return;
            }
            else if (node > height)
            {
                Console.WriteLine("Такой сотрудник не найден!");
                return;
            }
            else
            {
                var leftEmployee = binaryTree[2 * node + 1];
                var rightEmployee = binaryTree[2 * node + 2];
                if (leftEmployee.Salary == salary)
                {
                    Console.WriteLine($"Искомый сотрудник: {leftEmployee.Name}");
                    return;
                }
                else if (rightEmployee.Salary == salary)
                {
                    Console.WriteLine($"Искомый сотрудник: {rightEmployee.Name}");
                    return;
                }
                FindEmployee(binaryTree, salary, node + 1);
            }
        }

        private static void ShowSalariesAsc(Employee[] binaryTree)
        {
            var newArray = new Employee[binaryTree.Length];

        }

        private static void FindTopSalary(Employee[] array, Employee employee, int node, int index)
        {

        }

        private static Employee[] BuildBinaryTree(List<Employee> employees)
        {
            var array = new Employee[employees.Count];

            foreach (var employee in employees)
            {
                if (employee != employees.FirstOrDefault())
                {
                    AddEmployeeToArray(array, employee, 0);
                }
                else
                {
                    array[0] = employee;
                }
            }
            return array;
        }

        private static void AddEmployeeToArray(Employee[] array, Employee employee, int node)
        {
            var left = 2 * node + 1;
            var right = 2 * node + 2;
            if (array[left] == null)
            {
                array[left] = employee;
            }
            else if (array[right] == null)
            {
                array[right] = employee;
            }
            else
            {
                AddEmployeeToArray(array, employee, node + 1);
            }
        }
    }
}
