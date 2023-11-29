Project Name: DemoAPITest

Description: A set of API tests built using C# Specflow to test a couple of API calls under https://reqres.in/.

PROJECT STRUCTURE/DIRECTORIES
=============================

Data - contains the object classes that represent the different request and response for each API calls tested.<br>
Features - contains the feature files for the selected API calls. These were broken down into 3 different files, namely, for the Users, Register, and Login APIs.<br>
Steps - contains all the step definitions for all used steps in the feature files.<br>
Utility - contains classes necessary for performing the API calls, utility functions for string processing, and JSON file to handle string constants.
<br>
<br>


HOW TO RUN THE TESTS
====================

UI - Make sure you have Test Explorer in view. To enable it, go to View -> Test Explorer, or simply press Ctrl+E, T. You can usually find this view to the left. Once in view, you can specify which tests to run by selecting them from the menu. To run the selected tests, simply click on the Run button. Alternatively, you can press Ctrl+R, T. Tests that pass will then be marked by a green tick icon. If it fails, a rex X icon will be displayed instead. The details of the test whether it passes or fails will then be displayed at the bottom of the Test Explorer view.

Console - The tests can also be run via console. To access this, go to View -> Terminal, or press Ctrl+`. This view usually shows at the bottom of the IDE. It can take some time for the console to load. Once ready, just input the command "dotnet test". After the tests are run, it should display a summary of which tests have passed or failed.


RUNTIME
=======
API tests are usually very quick. For this project, average run time is just 5 seconds.