Project-X
============
### Applying Migrations instruction
  1. Open the project in PowerShell/cmd/other cli
  2. ` cd ./src/Infrastructure `
  3. ` dotnet ef migrations add <Migration Name> --startup-project ../Web --context DbContext -o ./Infrastructure/Data/Migrations `
  4. ` dotnet ef database update --startup-project ../Web `
