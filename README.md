# Alerts API

Small MVP of an alerts API

# Run dev

dotnet run

# Swagger

http://localhost:5233/api/swagger/

# Architechture

Classic mvc pattern with controller -> service -> repository. Keeping the domain model "Alerts" to the domain, use request and response models as exposed data models. Most logic is kept in the service layer, uses an in-memory solution as a simple data layer. I've built the repo layer as aync since that is how I would have built the actuallt solution, even though it's not really needed here. Implemented a naive "Result" class which i use to signal different scenarios in the service layer (conflict, notFound, inValid etc)

# TODO

- Tests, unit & e2e
- Atomic db handling
- Model change tracking (who published, cancelled etc)
- Logging
- Metrics
- Authentication, Authorization, Rate limiting
- DB unique constraint (area, status == "published")
- Validate request models
- Caching
