using WebEmployeeManagement.Applications.Services;
using WebEmployeeManagement.Infrastructures.Entities;

namespace WebEmployeeManagement.Tests.Applications.Services;

public class DepartmentServiceTests
{
    [Fact]
    public void GetAll_部署一覧をリポジトリから取得する()
    {
        var departments = new List<Department>
        {
            new() { DepartmentId = 1, DepartmentName = "総務" },
            new() { DepartmentId = 2, DepartmentName = "営業" }
        };
        var repository = new FakeDepartmentRepository { Departments = departments };
        var service = new DepartmentService(repository);

        var result = service.GetAll();

        Assert.Equal(2, result.Count);
        Assert.Equal("総務", result[0].DepartmentName);
        Assert.Equal("営業", result[1].DepartmentName);
    }

    [Fact]
    public void Find_指定したIDの部署を返す()
    {
        var repository = new FakeDepartmentRepository
        {
            Departments = new List<Department> { new() { DepartmentId = 10, DepartmentName = "開発" } }
        };
        var service = new DepartmentService(repository);

        var result = service.Find(10);

        Assert.NotNull(result);
        Assert.Equal("開発", result!.DepartmentName);
    }

    [Fact]
    public void HasEmployees_指定部署に社員がいる場合trueを返す()
    {
        var repository = new FakeDepartmentRepository { HasEmployeesResult = true };
        var service = new DepartmentService(repository);

        var result = service.HasEmployees(1);

        Assert.True(result);
    }

    [Fact]
    public void Create_同一IDが既存の場合は失敗を返す()
    {
        var repository = new FakeDepartmentRepository { ExistsByIdResult = true };
        var service = new DepartmentService(repository);

        var result = service.Create(new Department { DepartmentId = 1, DepartmentName = "経理" });

        Assert.False(result.Succeeded);
        Assert.Equal("同一IDの部署が既に存在しています。", result.ErrorMessage);
        Assert.False(repository.AddCalled);
        Assert.False(repository.SaveChangesCalled);
    }

    [Fact]
    public void Create_新規部署作成成功時は成功を返す()
    {
        var repository = new FakeDepartmentRepository();
        var service = new DepartmentService(repository);

        var department = new Department { DepartmentId = 3, DepartmentName = "人事" };
        var result = service.Create(department);

        Assert.True(result.Succeeded);
        Assert.Null(result.ErrorMessage);
        Assert.True(repository.AddCalled);
        Assert.True(repository.SaveChangesCalled);
        Assert.Contains(department, repository.Departments);
    }

    [Fact]
    public void Create_SaveChangesで例外発生時は失敗を返す()
    {
        var repository = new FakeDepartmentRepository { ThrowOnSaveChanges = true };
        var service = new DepartmentService(repository);

        var result = service.Create(new Department { DepartmentId = 4, DepartmentName = "法務" });

        Assert.False(result.Succeeded);
        Assert.Contains("部署の保存中にエラーが発生しました", result.ErrorMessage);
    }

    private class FakeDepartmentRepository : IDepartmentRepository
    {
        public List<Department> Departments { get; set; } = new();
        public bool ExistsByIdResult { get; set; }
        public bool HasEmployeesResult { get; set; }
        public bool ThrowOnSaveChanges { get; set; }
        public bool AddCalled { get; private set; }
        public bool SaveChangesCalled { get; private set; }

        public List<Department> GetAll() => Departments;
        public Department? Find(int departmentId) => Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
        public bool ExistsById(int departmentId) => ExistsByIdResult;
        public bool HasEmployees(int departmentId) => HasEmployeesResult;
        public void Add(Department department)
        {
            AddCalled = true;
            Departments.Add(department);
        }
        public void Remove(Department department) { }
        public void MoveEmployees(int fromDepartmentId, int toDepartmentId) { }
        public void SaveChanges()
        {
            SaveChangesCalled = true;
            if (ThrowOnSaveChanges)
            {
                throw new InvalidOperationException("DBエラー");
            }
        }
    }
}
