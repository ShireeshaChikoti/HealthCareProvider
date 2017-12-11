Solution contains 2 projects
1. ASP.Web API named "HealthCareProviderWebAPI"
2. Class Library Project "DataAccess"

HealthCareProviderWebAPI:
-------------------------
Under this project, we have "providersController" which contains the GET method to get providers based on search criteria
Example Endpoint: 
http://localhost:54682/api/providers?max_discharges=18&min_discharges=16&max_average_covered_charges=8600&min_average_covered_charges=8000&min_average_medicare_payments=4000&max_average_medicare_payments=6000&state=AL

DataAccess:
------------
This is a class library project for access to the csv datasource of providers data
This project contains 2 classes namely
1. SearchCriteria: This is an entity class for search criteria. This class contains properties for all query strings and a default constructor to set default values for the properties.

2. ProviderDetail: This class contains methods to retrieve data from csv datasource. It contains mechanism to cache providers data to prevent unnecessary server calls to frequently accessed data.