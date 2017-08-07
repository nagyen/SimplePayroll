# Simple Payroll

## [Live Demo](http://ec2-107-22-40-48.compute-1.amazonaws.com/)

## Docker Image: [nyendluri/simple-payroll:latest](https://hub.docker.com/r/nyendluri/simple-payroll/)

----

## Environment

> The application is written using ASP.NET Core, Entity Framework Core, AngularJS

----

## Features

* User Authentication
* Employee Listing
* Create New Employee
* Edit Employee Details
* Record Pay
* Pay History Listing

----

## Database

* The application uses Entity Framework Core In-Memory database.

## Tables

* Authentication
  - Id (long)
  - AuthKey (Guid)
  - UserId (long)
  - CreateDateTime (DateTime)
* User
  - Id (long)
  - Username (string)
  - FirstName (string)
  - LastName (string)
  - Password (string)
  - CreateDateTime (DateTime)
* Employee
  - Id (long)
  - FirstName (string)
  - LastName (string)
  - State (string)
  - W4Allowances (int)
  - SSN (string)
  - Insurance (decimal)
  - Reitirement401KPercent(decimal)
  - Reitirement401KPreTax (bool)
  - CreateDateTime (DateTime)
* Payment
  - Id (long)
  - EmplId (long)
  - GrossPay(decimal)
  - PaymentPeriodFrom (DateTime)
  - PaymentPeriodTo (DateTime)
  - FedTax (decimal)
  - StateTax (decimal)
  - SocialSecurityTax (decimal)
  - MedicareTax (decimal)
  - Insurance (decimal)
  - Reitirement401K(decimal)
  - NetPay (decimal)
  - CreateDateTime (DateTime)
* TaxPercentage
  - Id (decimal)
  - TaxCode (string)
  - Percent (decimal)

----

## Steps to run locally

[Intall dotnet core](https://www.microsoft.com/net/core?WT.mc_id=Blog_CENews_Announce_CEA#windowsvs2017)

Run either of the following commands from project root

* $ sh run-web.sh
* $ dotnet restore && cd ./web && dotnet run

## Steps to run using docker

[Install Docker](https://docs.docker.com/engine/installation/#desktop)
Run the following commands from project root

* $ docker run --rm -p 5000:5000 nyendluri/greatbank-web:latest

> Note: If port 5000 is not available please map to any other available port

----
