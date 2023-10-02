## powerplant-coding-challenge

Solution consists of two projects - Api and Test. I made conscious decision not to split Api project to separate ones with domain model, web controllers and core logic, since at this level of complexity it makes sense to keep it all in one place. If solution would grow, it would obviously make sense to do proper decomposition. 

### Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download) 
- [Docker](https://www.docker.com/get-started) (optional)

### Build and Run

To build and run the solution, please perform the following steps:

#### Clone the repo
```bash
git clone https://github.com/dmitrybozhenok/powerplant-coding-challenge
```
#### Navigate to the solution
```bash
cd powerplant-coding-challenge
```
#### Build the project
```bash
dotnet build
```
#### Run the API
```bash
dotnet run --project ProductionPlan.Api
```
API then can be explored via Swagger at https://localhost:8888/swagger/index.html
