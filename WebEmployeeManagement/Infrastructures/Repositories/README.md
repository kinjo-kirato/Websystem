# Repositories Layer

このフォルダはレイヤードアーキテクチャにおける **Infrastructure層のRepository実装** を配置します。

- `DepartmentRepository.cs`
  - `IDepartmentRepository` のインフラ実装。
  - データ取得・登録・更新処理をController/Serviceから分離する役割を持ちます。

- `EmployeeRepository.cs`
  - `IEmployeeRepository` のインフラ実装。
  - 社員データの取得・追加・更新・削除処理を担当します。
