Project Name: DemoAPITest

Description: A set of API tests built using C# Specflow to test a couple of API calls under https://reqres.in/.

Project Structure/Directories:

Data - contains the object classes that represent the different request and response for each API calls tested.
Features - contains the feature files for the selected API calls. These were broken down into 3 different files, namely, for the Users, Register, and Login APIs.
Steps - contains all the step definitions for all used steps in the feature files.
Utility - contains classes necessary for performing the API calls, utility functions for string processing, and JSON file to handle string constants.