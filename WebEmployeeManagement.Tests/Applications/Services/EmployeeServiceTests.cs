using WebEmployeeManagement.Applications.Interfaces;
using WebEmployeeManagement.Applications.Services;
using WebEmployeeManagement.Infrastructures.Entities;

namespace WebEmployeeManagement.Tests.Applications.Services;

public class EmployeeServiceTests
{
    [Fact]
    public async Task GetAllEmployeesAsync_リポジトリの結果を返す()
    {
        var repository = new FakeEmployeeRepository
        {
            Employees = new List<Employee>
            {
                new() { EmployeeId = 1, LastName = "山田", FirstName = "太郎" },
                new() { EmployeeId = 2, LastName = "佐藤", FirstName = "花子" }
            }
        };
        var service = new EmployeeService(repository);

        var result = await service.GetAllEmployeesAsync();

        Assert.Equal(2, result.Count);
        Assert.Equal("山田", result[0].LastName);
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_指定IDの社員を返す()
    {
        var employee = new Employee { EmployeeId = 5, LastName = "鈴木", FirstName = "一郎" };
        var repository = new FakeEmployeeRepository { FindByIdResult = employee };
        var service = new EmployeeService(repository);

        var result = await service.GetEmployeeByIdAsync(5);

        Assert.NotNull(result);
        Assert.Equal(5, result!.EmployeeId);
    }

    [Fact]
    public async Task AddEmployeeAsync_リポジトリのAddAsyncを呼び出す()
    {
        var repository = new FakeEmployeeRepository();
        var service = new EmployeeService(repository);
        var employee = new Employee { EmployeeId = 10, LastName = "中村", FirstName = "次郎" };

        await service.AddEmployeeAsync(employee);

        Assert.True(repository.AddCalled);
        Assert.Contains(employee, repository.Employees);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_リポジトリのUpdateAsyncを呼び出す()
    {
        var repository = new FakeEmployeeRepository();
        var service = new EmployeeService(repository);
        var employee = new Employee { EmployeeId = 11, LastName = "高橋", FirstName = "三郎" };

        await service.UpdateEmployeeAsync(employee);

        Assert.True(repository.UpdateCalled);
        Assert.Equal(employee, repository.UpdatedEmployee);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_リポジトリのDeleteAsyncを呼び出す()
    {
        var repository = new FakeEmployeeRepository();
        var service = new EmployeeService(repository);

        await service.DeleteEmployeeAsync(99);

        Assert.True(repository.DeleteCalled);
        Assert.Equal(99, repository.DeletedId);
    }

    private class FakeEmployeeRepository : IEmployeeRepository
    {
        public List<Employee> Employees { get; set; } = new();
        public Employee? FindByIdResult { get; set; }
        public bool AddCalled { get; private set; }
        public bool UpdateCalled { get; private set; }
        public bool DeleteCalled { get; private set; }
        public Employee? UpdatedEmployee { get; private set; }
        public int DeletedId { get; private set; }

        public Task<List<Employee>> GetAllAsync() => Task.FromResult(Employees);
        public Task<Employee?> FindByIdAsync(int id) => Task.FromResult(FindByIdResult);

        public Task AddAsync(Employee employee)
        {
            AddCalled = true;
            Employees.Add(employee);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Employee employee)
        {
            UpdateCalled = true;
            UpdatedEmployee = employee;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            DeleteCalled = true;
            DeletedId = id;
            return Task.CompletedTask;
        }
    }
}
