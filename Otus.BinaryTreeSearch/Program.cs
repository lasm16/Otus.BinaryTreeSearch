﻿using System.Xml.Linq;

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
            ShowSalariesAsc(binaryTree, 0, false, false);
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
            if (node > binaryTree.Length)
            {
                Console.WriteLine("Такой сотрудник не найден!");
                return;
            }
            var employee = binaryTree[node];
            if (employee.Salary == salary)
            {
                Console.WriteLine($"Искомый сотрудник: {employee.Name}");
                return;
            }
            else if (employee.Salary > salary)
            {
                var leftEmployee = 2 * node + 1;
                FindEmployee(binaryTree, salary, leftEmployee);
            }
            else
            {
                var rightEmployee = 2 * node + 2;
                FindEmployee(binaryTree, salary, rightEmployee);
            }
        }

        private static void ShowSalariesAsc(Employee[] binaryTree, int node, bool leftChecked, bool rightChecked)
        {
            var left = 2 * node + 1;
            var right = 2 * node + 2;
            var currentEmployee = binaryTree[node];
            if (currentEmployee == null)
            {
                var previous = ((node - 1) / 2 - 1) / 2;
                var prevEmployee = binaryTree[previous];
                Console.WriteLine($"Имя: {prevEmployee.Name}, зарплата: {prevEmployee.Salary}");
                return;
                //как-то выйти надо нормально из рекурсии...
            }
            if (leftChecked && rightChecked)
            {
                var previous = (node - 1) / 2;
                var prevEmployee = binaryTree[previous];
                Console.WriteLine($"Имя: {prevEmployee.Name}, зарплата: {prevEmployee.Salary}");
                right = 2 * previous + 2;
                ShowSalariesAsc(binaryTree, right, false, false);
            }
            if (right > binaryTree.Length && rightChecked)
            {
                Console.WriteLine($"Имя: {currentEmployee.Name}, зарплата: {currentEmployee.Salary}");
                var previous = ((node - 1) / 2 - 1) / 2;
                ShowSalariesAsc(binaryTree, previous, true, true);
            }
            if (left > binaryTree.Length && currentEmployee != null)
            {
                Console.WriteLine($"Имя: {currentEmployee.Name}, зарплата: {currentEmployee.Salary}");
                var previous = (node - 1) / 2;
                var prevEmployee = binaryTree[previous];
                right = 2 * previous + 2;
                Console.WriteLine($"Имя: {prevEmployee.Name}, зарплата: {prevEmployee.Salary}");
                ShowSalariesAsc(binaryTree, right, false, true);
            }
            if (left > binaryTree.Length && currentEmployee == null)
            {
                return;
            }
            var leftEmployee = binaryTree[left];

            if (leftEmployee != null)
            {
                ShowSalariesAsc(binaryTree, left, false, false);
            }
            else
            {
                Console.WriteLine($"Имя: {currentEmployee.Name}, зарплата: {currentEmployee.Salary}");
                ShowSalariesAsc(binaryTree, right, false, false);
            }
        }

        private static Employee[] BuildBinaryTree(List<Employee> employees)
        {
            // какой задть размер массива, чтобы был оптимальный?
            var array = new Employee[employees.Count * 2];

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
            var currentEmployee = array[node];
            if (currentEmployee.Salary > employee.Salary && array[left] == null)
            {
                array[left] = employee;
            }
            else if (currentEmployee.Salary <= employee.Salary && array[right] == null)
            {
                array[right] = employee;
            }
            else if (currentEmployee.Salary > employee.Salary)
            {
                AddEmployeeToArray(array, employee, 2 * node + 1);
            }
            else
            {
                AddEmployeeToArray(array, employee, 2 * node + 2);
            }
        }
    }
}
