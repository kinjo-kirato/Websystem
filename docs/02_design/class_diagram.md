# クラス図（主要ドメイン）

> 形式：クラス名 / ・メソッド名（引数）:返り値

## IEmployeeService
社員に関するユースケース（一覧取得、1件取得、登録、更新、削除）を定義するサービスインターフェース。

・GetAllEmployeesAsync():Task<List<Employee>>
  - 社員一覧を取得する。
・GetEmployeeByIdAsync(id:int):Task<Employee?>
  - 指定した社員IDの社員を1件取得する（未存在の場合は `null`）。
・AddEmployeeAsync(employee:Employee):Task
  - 社員を新規登録する。
・UpdateEmployeeAsync(employee:Employee):Task
  - 既存社員情報を更新する。
・DeleteEmployeeAsync(id:int):Task
  - 指定した社員IDの社員を削除する。

## EmployeeService
`IEmployeeService` の実装クラス。業務処理の窓口として、社員関連処理を `IEmployeeRepository` に委譲する。

・EmployeeService(employeeRepository:IEmployeeRepository)
  - リポジトリ依存を受け取り、サービスを初期化する。
・GetAllEmployeesAsync():Task<List<Employee>>
  - 社員一覧取得をリポジトリに依頼して返す。
・GetEmployeeByIdAsync(id:int):Task<Employee?>
  - 指定IDの社員取得をリポジトリに依頼して返す。
・AddEmployeeAsync(employee:Employee):Task
  - 社員登録をリポジトリに依頼する。
・UpdateEmployeeAsync(employee:Employee):Task
  - 社員更新をリポジトリに依頼する。
・DeleteEmployeeAsync(id:int):Task
  - 社員削除をリポジトリに依頼する。

## IEmployeeRepository
社員データ永続化（DBアクセス）の契約を定義するリポジトリインターフェース。

・GetAllAsync():Task<List<Employee>>
  - 社員データを全件取得する。
・FindByIdAsync(id:int):Task<Employee?>
  - 指定IDの社員データを1件取得する。
・AddAsync(employee:Employee):Task
  - 社員データを新規追加する。
・UpdateAsync(employee:Employee):Task
  - 社員データを更新する。
・DeleteAsync(id:int):Task
  - 指定IDの社員データを削除する。

## EmployeeRepository
`IEmployeeRepository` の実装クラス。実際のDB（PostgreSQL）に対して社員データのCRUDを行う。

・EmployeeRepository(configuration:IConfiguration)
  - 接続文字列など設定を受け取り初期化する。
・GetAllAsync():Task<List<Employee>>
  - DBから社員一覧を取得する。
・FindByIdAsync(id:int):Task<Employee?>
  - DBから指定IDの社員を取得する。
・AddAsync(employee:Employee):Task
  - DBに社員を新規追加する。
・UpdateAsync(employee:Employee):Task
  - DB上の社員情報を更新する。
・DeleteAsync(id:int):Task
  - DB上の社員を削除する。

## IDepartmentService
部署に関するユースケース（一覧取得、検索、所属社員有無確認、作成）を定義するサービスインターフェース。

・GetAll():List<Department>
  - 部署一覧を取得する。
・Find(departmentId:int):Department?
  - 指定部署IDの部署を取得する（未存在の場合は `null`）。
・HasEmployees(departmentId:int):bool
  - 指定部署に社員が所属しているかを返す。
・Create(department:Department):DepartmentSaveResult
  - 部署を新規作成し、成否とエラーメッセージを返す。

## DepartmentService
`IDepartmentService` の実装クラス。部署関連の業務ロジック（重複チェックなど）を担当する。

・DepartmentService(departmentRepository:IDepartmentRepository)
  - リポジトリ依存を受け取り、サービスを初期化する。
・GetAll():List<Department>
  - 部署一覧を取得して返す。
・Find(departmentId:int):Department?
  - 指定IDの部署を取得して返す。
・HasEmployees(departmentId:int):bool
  - 指定部署の社員所属有無を返す。
・Create(department:Department):DepartmentSaveResult
  - 部署ID重複を確認し、問題なければ追加・保存して結果を返す。

## IDepartmentRepository
部署データ永続化（DBアクセス）の契約を定義するリポジトリインターフェース。

・GetAll():List<Department>
  - 部署データを全件取得する。
・Find(departmentId:int):Department?
  - 指定IDの部署データを1件取得する。
・ExistsById(departmentId:int):bool
  - 指定部署IDが既に存在するか確認する。
・HasEmployees(departmentId:int):bool
  - 指定部署に所属社員が存在するか確認する。
・Add(department:Department):void
  - 部署データを追加する。
・Remove(department:Department):void
  - 部署データを削除する。
・MoveEmployees(fromDepartmentId:int,toDepartmentId:int):void
  - ある部署の社員を別部署へ付け替える。
・SaveChanges():void
  - 追加・更新・削除内容を永続化する。

## DepartmentRepository
`IDepartmentRepository` の実装クラス。実際のDB（PostgreSQL）に対して部署関連処理を実行する。

・DepartmentRepository(configuration:IConfiguration)
  - 接続文字列など設定を受け取り初期化する。
・GetAll():List<Department>
  - DBから部署一覧を取得する。
・Find(departmentId:int):Department?
  - DBから指定部署を取得する。
・ExistsById(departmentId:int):bool
  - DB上で指定部署IDの存在確認を行う。
・HasEmployees(departmentId:int):bool
  - DB上で指定部署の社員所属有無を確認する。
・Add(department:Department):void
  - DBへ部署を追加する。
・Remove(department:Department):void
  - DBから部署を削除する。
・MoveEmployees(fromDepartmentId:int,toDepartmentId:int):void
  - DB上で社員の所属部署を移動する。
・SaveChanges():void
  - DBへ変更内容を確定反映する。

## DepartmentSaveResult
部署保存（主に作成）処理の結果を保持するレコード。

・DepartmentSaveResult(succeeded:bool,errorMessage:string?):DepartmentSaveResult
  - 保存処理の成功可否と、失敗時メッセージを保持する。

## DepartmentDeleteResult
部署削除処理の結果を保持するレコード。

・DepartmentDeleteResult(succeeded:bool,errorMessage:string?):DepartmentDeleteResult
  - 削除処理の成功可否と、失敗時メッセージを保持する。

## Employee
社員1件分のデータ（社員ID、氏名、部署ID など）を保持するエンティティ。

・(メソッドなし)

## Department
部署1件分のデータ（部署ID、部署名、所属社員リスト）を保持するエンティティ。

・(メソッドなし)

## User
ユーザー情報を保持するエンティティ。

・(メソッドなし)

## 関連
- `EmployeeService` は `IEmployeeRepository` に依存
- `EmployeeRepository` は `IEmployeeRepository` を実装
- `DepartmentService` は `IDepartmentRepository` に依存
- `DepartmentRepository` は `IDepartmentRepository` を実装
- `Employee` は `Department` に所属（`DepartmentId`）
- `Department` は `Employees` を保持
