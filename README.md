# PlumGuide

## Project structure
- Rover.Domain: domain classes to manage the rover.
- Rover.Domain.Tests: Unit tests of the project Rover.Domain
- Rover.Infra: Infra layer (persistance, ...)
- Rover.App: Application layer. In this case, the Rover API

## Debug
- Checkout the repository
- Build the solution
- Run the Rover.App project (startup project)
- Go to http://localhost:5000/swagger/index.html
- Or, Import the Postman collection (Rover.postman_collection.json)
- Trigger the requests (provided samples)
- Add your own requests and command the rover 

## Remarks
- Error management is not done
- Logging is not done
- Unit tests for the projects Rover.App and Rover.Infra are not done. 
- There is no persistance layer (EF Core or any other data source). Everything is kept in memory. 
- The Grid configuration is fixed to 100*100. Could be changed easly if needed. 
- The obstacles are hard coded in the memory implemetation (Domain.Infra). Here also, it can be changed if needed. 
- There are three registred rovers Pluto, Pluto2, and Pluto3. 
