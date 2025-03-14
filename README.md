
# RFQ Application: Multi-Framework Implementation

This repository showcases the implementation of a  application across multiple popular web development frameworks. The aim is to provide a comparative view of how the same application logic can be realized using different technologies, highlighting the unique strengths and patterns of each framework. 

## Project Goals

* **Comparative Analysis:** Illustrate the implementation of a consistent application across various tech stacks.
* **Skill Diversification:** Explore and gain proficiency in diverse web development frameworks.
* **Best Practices:** Apply framework-specific best practices and architectural patterns.
* **Performance Evaluation:** Conduct basic benchmarking to compare the performance of different implementations.

## Frameworks and Technologies Used

* **ASP.NET Web API (C#):** A robust framework for building web APIs using C#.
* **Express.js (Node.js):** A minimalist web framework for Node.js.
* **Ruby on Rails (Ruby):** A full-stack web application framework written in Ruby.

## Database Schema

The application uses the following database structure, adapted for SQLite:

### Users Table

* **Id** (INTEGER, PRIMARY KEY, AUTOINCREMENT)
* **Username** (TEXT, UNIQUE, NOT NULL)
* **PasswordHash** (TEXT, NOT NULL)
* **Role** (TEXT, NOT NULL, CHECK (Role IN ('employee', 'bidder')))

### RFQs Table

* **Id** (INTEGER, PRIMARY KEY, AUTOINCREMENT)
* **Title** (TEXT, NOT NULL)
* **Description** (TEXT, NOT NULL)
* **CreatedById** (INTEGER, FOREIGN KEY REFERENCES Users(Id))
* **CreatedAt** (DATETIME, NOT NULL)

### Quotations Table

* **Id** (INTEGER, PRIMARY KEY, AUTOINCREMENT)
* **RfqId** (INTEGER, FOREIGN KEY REFERENCES RFQs(Id))
* **BidderId** (INTEGER, FOREIGN KEY REFERENCES Users(Id))
* **Price** (REAL, NOT NULL)
* **Details** (TEXT)
* **SubmittedAt** (DATETIME, NOT NULL)

## API Routes

### Authentication

* **POST /api/auth/register** (Register a new user)
* **POST /api/auth/login** (Login and get a JWT)

### RFQs (protected by JWT, employee role)

* **POST /api/rfqs** (Create a new RFQ)
* **GET /api/rfqs** (Get all RFQs)
* **GET /api/rfqs/{id}** (Get a specific RFQ)
* **PUT /api/rfqs/{id}** (Update an RFQ)
* **DELETE /api/rfqs/{id}** (Delete an RFQ)

### Quotations (protected by JWT, bidder role)

* **POST /api/quotations** (Submit a quotation for an RFQ)
* **GET /api/quotations/{rfqId}** (Get all quotations for an RFQ)

## JWT Authentication

All protected routes require a valid JWT in the `Authorization` header. The JWT payload includes the user's `Id` and `Role`.

## Folder Structure

```
multi-framework-rfq/
├── RFQasp/         # ASP.NET Web API implementation
├── RFQexpress/     # Express.js implementation
├── RFQrails/       # Ruby on Rails implementation
├── README.md       # This README file
```

## Getting Started

To run each application, navigate to its respective directory and follow the instructions in that folder's `README.md`.

## Benchmarking

To evaluate the performance of each implementation, basic benchmarking was conducted using tools like `ab` (ApacheBench) or `Postman`'s collection runner. The following metrics were measured:

* **Requests per second (RPS):** Measures the throughput of the API.
* **Average response time:** Indicates the average time taken to process a request.
* **Memory usage:** Tracks the memory consumption of the application.

**Note:** Detailed benchmarking results and methodology will be added in a separate `BENCHMARKS.md` file.

## Contributing

While this repository primarily serves as a personal learning and demonstration project, contributions and feedback are always welcome. Feel free to open issues or submit pull requests with suggestions for improvements.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
